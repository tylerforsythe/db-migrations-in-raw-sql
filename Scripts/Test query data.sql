
select *
from sysobjects
where name = 'DatabaseVersion'
and type = 'U'


drop table DatabaseVersion


CREATE TABLE DatabaseVersion (
Id INT NOT NULL PRIMARY KEY IDENTITY (1,1),
ScriptName VARCHAR (250) NOT NULL,
RunTimestamp DATETIME NOT NULL
)
go


select * from DatabaseVersion 


SELECT 1
FROM DatabaseVersion
WHERE ScriptName = {0}

