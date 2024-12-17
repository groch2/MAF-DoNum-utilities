use Contrat;
select c.COLUMN_NAME
from [INFORMATION_SCHEMA].[COLUMNS] c
where c.TABLE_SCHEMA = 'Donum'
and c.TABLE_NAME = 'DocumentsMNKNonTranfere'
order by c.COLUMN_NAME