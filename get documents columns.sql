use [GEDMAF]
go

select COLUMN_NAME, C.DATA_TYPE, C.IS_NULLABLE
from INFORMATION_SCHEMA.COLUMNS C
where TABLE_SCHEMA = 'GED' and TABLE_NAME = 'Document'
order by COLUMN_NAME