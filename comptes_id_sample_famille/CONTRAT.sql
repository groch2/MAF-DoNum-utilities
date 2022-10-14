use [MAF_NSI]
go

with Compte ([RowNumber], [CompteID]) as (
select ROW_NUMBER() OVER(ORDER BY [CompteID] ASC), [CompteID]
from [Personnes].[Compte]
where [TypeCompteID] in
(1 
,2 
,3 
,4 
,8 
,9 
,16
,19
,20
,21
,30
,34
,36
,40
,51
,64
,41
,43
,48
,37
,35
,25
,26
,27
,22
,23
,24
,28
,29
,31
,32
,33
,52
,53
,54
,55
,56
,63))
select [CompteID] from Compte
WHERE [RowNumber] BETWEEN 1040 AND 1140
