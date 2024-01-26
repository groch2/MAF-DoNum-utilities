USE [Tarmac]
GO

DECLARE @IndicateurId_a_supprimer TABLE ([IndicateurId] int)
INSERT INTO @IndicateurId_a_supprimer
SELECT [IndicateurId]
FROM [Tarmac].[Indicateur]
WHERE [ApiUrl] LIKE '%/api/documents/queueindicateurtarmac%'

DELETE [ProfilIndicateur]
FROM [Tarmac].[ProfilIndicateur]
JOIN @IndicateurId_a_supprimer AS IndicateurId_a_supprimer
ON [ProfilIndicateur].IndicateurId = IndicateurId_a_supprimer.[IndicateurId]

DELETE [Indicateur]
FROM [Tarmac].[Indicateur]
JOIN @IndicateurId_a_supprimer AS IndicateurId_a_supprimer
ON [Indicateur].IndicateurId = IndicateurId_a_supprimer.[IndicateurId]

GO
