use Contrat;
select c.COLUMN_NAME
from INFORMATION_SCHEMA.COLUMNS c
where c.TABLE_CATALOG = 'Contrat'
and c.TABLE_SCHEMA = 'Geco'
and c.TABLE_NAME = 'Contrat'
order by c.COLUMN_NAME