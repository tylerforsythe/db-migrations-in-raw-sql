USE DbMigrationsInRawSqlTestDb;

INSERT INTO TestLog (Msg, NumberTest) VALUES ('pre-go', 1)
INSERT INTO TestLog (Msg, NumberTest) VALUES ('pre-go2', 2)
go

INSERT INTO TestLog (Msg, NumberTest) VALUES ('value 1', 12.3)
INSERT INTO TestLog (Msg, NumberTest) VALUES (null, null)
go