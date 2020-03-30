namespace DB_Migrations_in_Raw_SQL
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtShortName = new System.Windows.Forms.TextBox();
            this.btnCreateNewFile = new System.Windows.Forms.Button();
            this.btnRunMigrations = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBarMigrations = new System.Windows.Forms.ProgressBar();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblWebConfigPath = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtShortName
            // 
            this.txtShortName.Location = new System.Drawing.Point(47, 210);
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(219, 20);
            this.txtShortName.TabIndex = 1;
            // 
            // btnCreateNewFile
            // 
            this.btnCreateNewFile.Location = new System.Drawing.Point(61, 161);
            this.btnCreateNewFile.Name = "btnCreateNewFile";
            this.btnCreateNewFile.Size = new System.Drawing.Size(170, 23);
            this.btnCreateNewFile.TabIndex = 2;
            this.btnCreateNewFile.Text = "Create New Migration File";
            this.btnCreateNewFile.UseVisualStyleBackColor = true;
            this.btnCreateNewFile.Click += new System.EventHandler(this.btnCreateNewFile_Click);
            // 
            // btnRunMigrations
            // 
            this.btnRunMigrations.Location = new System.Drawing.Point(69, 83);
            this.btnRunMigrations.Name = "btnRunMigrations";
            this.btnRunMigrations.Size = new System.Drawing.Size(170, 23);
            this.btnRunMigrations.TabIndex = 3;
            this.btnRunMigrations.Text = "Run Migrations";
            this.btnRunMigrations.UseVisualStyleBackColor = true;
            this.btnRunMigrations.Click += new System.EventHandler(this.btnRunMigrations_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Short Descriptive Name:";
            // 
            // progressBarMigrations
            // 
            this.progressBarMigrations.Location = new System.Drawing.Point(37, 119);
            this.progressBarMigrations.Name = "progressBarMigrations";
            this.progressBarMigrations.Size = new System.Drawing.Size(219, 23);
            this.progressBarMigrations.TabIndex = 6;
            // 
            // txtLog
            // 
            this.txtLog.AcceptsReturn = true;
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(47, 284);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(567, 361);
            this.txtLog.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(47, 97);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(219, 84);
            this.textBox3.TabIndex = 8;
            this.textBox3.TabStop = false;
            this.textBox3.Text = resources.GetString("textBox3.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "DB Migrations in Raw SQL";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(37, 22);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(219, 46);
            this.textBox1.TabIndex = 20;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "To get your local database up to date with all the latest scripts, click this but" +
    "ton and the app will do the rest.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Log:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreateNewFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 195);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Migration File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRunMigrations);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.progressBarMigrations);
            this.groupBox2.Location = new System.Drawing.Point(323, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 195);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Run Migrations";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(539, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblWebConfigPath
            // 
            this.lblWebConfigPath.AutoSize = true;
            this.lblWebConfigPath.Location = new System.Drawing.Point(13, 49);
            this.lblWebConfigPath.Name = "lblWebConfigPath";
            this.lblWebConfigPath.Size = new System.Drawing.Size(94, 13);
            this.lblWebConfigPath.TabIndex = 15;
            this.lblWebConfigPath.Text = "Web.Config Path: ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 657);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblWebConfigPath);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtShortName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "MainForm";
            this.Text = "DB Migrations in Raw SQL";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtShortName;
        private System.Windows.Forms.Button btnCreateNewFile;
        private System.Windows.Forms.Button btnRunMigrations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBarMigrations;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblWebConfigPath;
    }
}

