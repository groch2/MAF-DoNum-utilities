use [GEDMAF]
go

select *
from INFORMATION_SCHEMA.TABLES
where TABLE_NAME like '%redacteur%'
