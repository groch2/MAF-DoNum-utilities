use [MAF_NSI]
go

with Compte ([RowNumber], [CompteID]) as (
select ROW_NUMBER() OVER(ORDER BY [CompteID] ASC), [CompteID]
from [Personnes].[Compte]
where [TypeCompteID] in
(5
,6
,18
,46))
select [CompteID] from Compte
WHERE [RowNumber] BETWEEN 1040 AND 1140