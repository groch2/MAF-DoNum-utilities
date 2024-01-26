USE [GEDMAF]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RechercheDocumentByLibelleSubstring]
		@LibelleSubstring = '20220509637979862013117450'

SELECT	'Return Value' = @return_value

GO
