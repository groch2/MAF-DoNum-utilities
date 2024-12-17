use [Contrat];
declare @row_start as int = 2700
declare @row_end as int = @row_start + 10
;with [Chantier] ([RowNumber], [ChantierID]) as (
select ROW_NUMBER() OVER(ORDER BY [ChantierID] ASC), [ChantierID]
from [Geco].[Chantier])
select [ChantierID] from [Chantier]
WHERE [RowNumber] BETWEEN @row_start AND @row_end
