-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roch DESCHASEAUX
-- Create date: 15/02/2023
-- Description:	retourne les documents de la vue [GED].[NormalizedDocument] dont
--              la colonne libellé contient la sous chaîne passée en paramètre
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[RechercheDocumentByLibelleSubstring]
	@LibelleSubstring AS VARCHAR(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		 [AssigneRedacteur]
		,[AssureurId]
		,[CategoriesCote]
		,[CategoriesFamille]
		,[CategoriesTypeDocument]
		,[ChantierId]
		,[Commentaire]
		,[CompteId]
		,[DateDocument]
		,[DeposeLe]
		,[DeposePar]
		,[DocumentId]
		,[Important]
		,[Libelle]
		,[NumeroContrat]
		,[NumeroGc]
		,[NumeroSinistre]
		,[PeriodeValiditeDebut]
		,[PeriodeValiditeFin]
		,[QualiteValideeLe]
		,[QualiteValideePar]
		,[QualiteValideeValide]
		,[ReferenceSecondaire]
		,[Sens]
		,[SousDossierSinistre]
		,[Statut]
		,[TraiteLe]
		,[TraitePar]
		,[TypeGarantie]
		,[VuLe]
		,[VuPar]
	FROM [GED].[NormalizedDocument_2]
	--WHERE [Libelle] LIKE CONCAT('%', @LibelleSubstring, '%')
	where [NormalizedDocument_2].[DocumentId] = @LibelleSubstring
END
GO
