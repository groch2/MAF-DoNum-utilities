USE [GEDMAF]
GO

select [LIB_DOC]
from [dbo].[ARCHEAMAF]
where DATE_INTEGRATION = (select min(DATE_INTEGRATION) from [dbo].[ARCHEAMAF])