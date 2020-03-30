using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;

namespace DB_Migrations_in_Raw_SQL
{
    using System.Configuration;

    public partial class MainForm : Form
    {
        private string WebConfigPath = string.Empty;

        private bool ValidDB = false;

        private string connectionString = string.Empty;

        private bool ForceClose = false;

        public MainForm(string[] args) {
            InitializeComponent();

            InitializeApplication(args);

            if (args != null && args.Length > 0) {
                foreach (string arg in args) {
                    if (arg == "runmigrations") {
                        RunMigrations(true, true);
                    }
                }
            }
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            if (ForceClose) {
                Close();
                Application.Exit();
            }
        }

        private void InitializeApplication(string[] args)
        {
            this.connectionString = this.GetConnectionString(args);
        }

        private string GetConnectionString(string[] args)
        {
            if (ConfigurationManager.ConnectionStrings["connectionString"] != null) {
                var conn = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                if (!string.IsNullOrEmpty(conn)) {
                    this.AddLogLine("Found app.config connection string");
                    ValidDB = true;
                    return conn;
                }
            }

            WebConfigPath = FindWebConfig(args);

            if (string.IsNullOrEmpty(WebConfigPath))
            {
                lblWebConfigPath.Text = "Web.config file not found!";
                ValidDB = false;
                return string.Empty;
            }

            this.lblWebConfigPath.Text = "Web.config file path: " + this.WebConfigPath;
            this.ValidDB = true;

            return this.GetConnectionStringFromWebConfigAtPath(WebConfigPath);
        }

        private string FindWebConfig(string[] args) {
            //first try to extract the path from args
            if (args != null && args.Length > 0) {
                foreach (string checkPath in args) {;
                    if (!string.IsNullOrEmpty(checkPath)) {
                        if (File.Exists(checkPath)) {
                            return checkPath;
                        }

                        var full = Path.GetFullPath(checkPath);
                        if (File.Exists(full)) {
                            return full;
                        }
                    }
                }
            }

            //try to locate a web.config as if we are in a S#arp Lite project
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            AddLogLine("This path: " + path);

            path = SeekUpUntilPathOrLimit(path, 10);

            if (!string.IsNullOrEmpty(path)) {
                AddLogLine("Web directory found: " + path);
                path = Path.Combine(path, "Web.config");
                if (File.Exists(path))
                    return path;
            }

            return string.Empty;
        }

        private string SeekUpUntilPathOrLimit(string currentPath, int limitCount) {
            if (limitCount <= 0)
                return string.Empty;

            var files = Directory.GetFiles(currentPath, "Web.config");
            if (files.Length > 0) {
                return currentPath;
            }

            AddLogLine("Up to: " + currentPath);
            string[] webPaths = Directory.GetDirectories(currentPath, "*.Web");
            if (webPaths.Length > 0) {
                return webPaths[0];
            } 
            
            if (webPaths.Length <= 0) {
                webPaths = Directory.GetDirectories(currentPath, "*.Web.Mvc");

                if (webPaths.Length > 0)
                    return webPaths[0];
            }

            currentPath = Path.GetDirectoryName(currentPath);
            if (string.IsNullOrEmpty(currentPath))//we ran out of directories--abort!
                return string.Empty;

            return SeekUpUntilPathOrLimit(currentPath, --limitCount);
        }

        private void AddLogLine(string text) {
            txtLog.Text += Environment.NewLine + text;

            //log.Info(text); // Here's where we can turn on outputting to a text file if desired.
        }

        private void btnExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnRunMigrations_Click(object sender, EventArgs e) {
            RunMigrations(false, false);
        }

        private void RunMigrations(bool silent, bool exitWhenDone) {
            if (!ValidDB) {
                txtLog.Text = "Invalid web.config path!";
                return;
            }

            DatabaseVersion dbVersion = new DatabaseVersion();

            // make connection to database
            dbVersion.ConnectToDatabase(connectionString);

            // does DatabaseVersion table exist?
            if (!dbVersion.DoesTableExist()) //if not, create it
                dbVersion.CreateTable();

            // read all database scripts from file system
            List<string> scriptsFromFileSystem = ReadScriptListFromFileSystem();

            // form new list of all scripts form filesystem that do not appear in DB table
            List<string> scriptsToRun = dbVersion.WhichScriptsHaveNotBeenRun(scriptsFromFileSystem);

            // order scripts sequentially
            scriptsToRun.Sort();

            if (scriptsToRun.Count == 0) {
                if (!silent) {
                    MessageBox.Show("Your database is up to date!", "DB Up To Date", MessageBoxButtons.OK);
                }
                dbVersion.Dispose();
                if (exitWhenDone)
                    ForceClose = true;
                else
                    return;
            }

            // confirm list with user and ask before executing!
            bool shouldContinue = false;
            if (silent) {
                shouldContinue = true;
            }
            else {
                StringBuilder sb = new StringBuilder(300);
                sb.AppendLine("Would you like to run these scripts?");
                foreach (string scriptName in scriptsToRun)
                    sb.AppendLine(scriptName);
                DialogResult result = MessageBox.Show(sb.ToString(), "", MessageBoxButtons.YesNo);
                shouldContinue = result == System.Windows.Forms.DialogResult.Yes;
            }

            if (shouldContinue) {
                progressBarMigrations.Value = 0;
                progressBarMigrations.Maximum = scriptsToRun.Count;

                // execute scripts until complete or error
                foreach (string scriptPath in scriptsToRun) {
                    ScriptRunResult fileSuccess = null;
                    bool success = true;
                    Exception whatWeGot = null;
                    txtLog.Text += string.Format("Executing {0}{1}", Path.GetFileName(scriptPath), Environment.NewLine);
                    try {
                        fileSuccess = dbVersion.ExecuteScriptFileAtPath(scriptPath);
                    }
                    catch (Exception ex) {
                        success = false;
                        whatWeGot = ex;
                    }

                    if (fileSuccess == null)
                        success = false;

                    if (success)
                        success = fileSuccess.WasSuccessful;

                    if (success) {
                        txtLog.Text += string.Format("Success!{0}{0}", Environment.NewLine);
                    }
                    else {
                        txtLog.Text += $"{Environment.NewLine}{Environment.NewLine}FAILED ON: {Path.GetFileName(scriptPath)}{Environment.NewLine}{Environment.NewLine}";
                        if (fileSuccess != null)
                            txtLog.Text += $"{fileSuccess.ResultString}";
                        else
                            txtLog.Text += "No failure message returned!";
                        dbVersion.Dispose();
                        return;
                    }
                    progressBarMigrations.Value += 1;
                }
            }
            else {
                txtLog.Text += string.Format("Chose not to run scripts.{0}", Environment.NewLine);
            }

            dbVersion.Dispose();

            if (exitWhenDone)
                ForceClose = true;
        }

        private List<string> ReadScriptListFromFileSystem() {
            string thisPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] files = Directory.GetFiles(thisPath, "*.sql");

            // for testing
            if (files.Length == 0)
                files = Directory.GetFiles(Path.Combine(thisPath, "TestScripts"), "*.sql");

            return files.ToList<string>();
        }

        private string GetConnectionStringFromWebConfigAtPath(string configFilePath) {
            XmlDocument Doc = new XmlDocument();
            Doc.Load(configFilePath);
            XmlElement Root = Doc.DocumentElement;

            string xpath = "connectionStrings/add";
            XmlNode node = Root.SelectSingleNode(xpath);
            return node.Attributes["connectionString"].Value;
        }

        private void btnCreateNewFile_Click(object sender, EventArgs e) {
            string fileName = GetNewFileName();
            FileStream newFile = File.Create(fileName);
            newFile.Close();
            txtLog.Text = string.Format("Created {0}", fileName);
        }

        private string GetNewFileName() {
            return string.Format("{0}_{1}.sql", GetFullTimestampString(), txtShortName.Text.Replace(" ", "_"));
        }

        private string GetFullTimestampString() {
            DateTime n = DateTime.Now;
            return string.Format("{0}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}", n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second);
        }
    }
}
