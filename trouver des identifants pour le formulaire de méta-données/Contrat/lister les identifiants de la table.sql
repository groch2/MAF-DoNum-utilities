use [Contrat];
declare @row_start as int = 2700
declare @row_end as int = @row_start + 10
;with [Contrat] ([RowNumber], [ContratID], [NumeroContrat]) as (
select ROW_NUMBER() OVER(ORDER BY [ContratID] ASC), [ContratID], [NumeroContrat]
from [Geco].[Contrat]
where [NumeroContrat] is not null)
select [ContratID], [NumeroContrat] from [Contrat]
WHERE [RowNumber] BETWEEN @row_start AND @row_end
