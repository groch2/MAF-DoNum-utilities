USE [MAF_NSI]
GO

WITH PERSONNE_P ([RowNumber], [CompteID], [TypeCompteID], [PersonneID], [Nom], [Prenom]) AS (
	SELECT ROW_NUMBER() OVER(ORDER BY P.[PersonneID] ASC)
		  ,C.[CompteID]
		  ,C.[TypeCompteID]
		  ,P.[PersonneID]
		  ,P.Nom
		  ,P.Prenom
	FROM [Personnes].[Personne] P
	JOIN [Personnes].[Compte] C
	ON P.PersonneID = C.PersonneID
	WHERE P.TypePersonneId = 1
	AND C.TypeCompteID IN (1,2,3,4,8,9,16,19,20,21,30,34,36,40,51,64,41,43,48,37,35,25,26,27,22,23,24,28,29,31,32,33,52,53,54,55,56,63)
	AND Nom is not null
	AND Nom <> ''
	AND Prenom is not null
	AND Prenom <> '')
SELECT [PersonneID], [CompteID], [Nom], [Prenom], [Espace].[Description] as [EspaceCompte]
FROM PERSONNE_P
JOIN [RefMaf].[TypeCompte]
ON PERSONNE_P.[TypeCompteID] = [TypeCompte].[TypeCompteId]
JOIN [RefMaf].[Espace]
ON [TypeCompte].EspaceId = [Espace].EspaceId
WHERE [RowNumber] BETWEEN 10000 AND 30000
GO
