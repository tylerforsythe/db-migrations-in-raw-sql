# DB Migrations in Raw SQL

This application is intended for projects with multiple developers making database changes. Each developer can keep their database current simply by running the tool after updating to the latest revision of code. It will track what scripts have been run and which haven't, reducing the mental effort on each developer to nil.

It can be used on virtually any project that needs SQL update scripts to be run. However, it does not currently support rollback.

**Can also be configured to execute silently for use with deployment scripts.**