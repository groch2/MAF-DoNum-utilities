USE [MAF_NSI]
GO

WITH PERSONNE_P ([RowNumber], [CompteID], [TypeCompteID], [PersonneID], [RaisonSociale]) AS (
	SELECT ROW_NUMBER() OVER(ORDER BY P.[PersonneID] ASC)
		  ,C.[CompteID]
		  ,C.[TypeCompteID]
		  ,P.[PersonneID]
		  ,P.[RaisonSociale]
	FROM [Personnes].[Personne] P
	JOIN [Personnes].[Compte] C
	ON P.PersonneID = C.PersonneID
	where p.TypePersonneId = 2
	and RaisonSociale is not null
	and RaisonSociale <> '')
SELECT [PersonneID], [CompteID], [RaisonSociale], [Espace].[Description] as [EspaceCompte]
FROM PERSONNE_P
JOIN [RefMaf].[TypeCompte]
ON PERSONNE_P.[TypeCompteID] = [TypeCompte].[TypeCompteId]
JOIN [RefMaf].[Espace]
ON [TypeCompte].EspaceId = [Espace].EspaceId
WHERE [RowNumber] BETWEEN 20040 AND 21000
GO
