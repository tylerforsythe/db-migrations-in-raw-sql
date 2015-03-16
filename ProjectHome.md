DB Migrations in Raw SQL
This application is intended for projects with multiple developers making database changes. Each developer can keep their database current simply by running the tool after updating to the latest revision of code. It will track what scripts have been run and which haven't, reducing the mental effort on each developer to nil.

It's primarily designed for use within S#arpLite projects, but it can be configured to work in any context. Please feel free to submit improvements (I'm sure it needs them!).