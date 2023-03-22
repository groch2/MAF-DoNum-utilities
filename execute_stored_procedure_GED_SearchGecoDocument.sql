USE [GEDMAF]
GO

declare @ListOfInt as [dbo].[ListOfIntType]
declare @ListOfNumeroContrat as [dbo].[ListOfNumeroContratType]
declare @ListOfIdDoc as [dbo].[ListOfIdDocType]
declare @ListOfFamille as [dbo].[ListOfFamilleType]
declare @ListOfCote as [dbo].[ListOfCoteType]

DECLARE	@return_value int

EXEC	@return_value = [GED].[SearchGecoDocument]
		--@CodeGestionnaireRedacteur = N'ROD',
		--@LibelleDocument = '09',
		@ChantierIds = @ListOfInt,
		@NumerosContrats = @ListOfNumeroContrat,
		@AttestationDocIds = @ListOfIdDoc,
		@Familles = @ListOfFamille,
		@Cotes = @ListOfCote

SELECT	'Return Value' = @return_value
GO
