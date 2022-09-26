USE [MAF_NSI]
GO

WITH PERSONNE_P ([RowNumber], [CompteID], [PersonneID], [Nom], [Prenom]) AS (
	SELECT ROW_NUMBER() OVER(ORDER BY P.[PersonneID] ASC)
		  ,C.[CompteID]
		  ,P.[PersonneID]
		  ,P.Nom
		  ,P.Prenom
	FROM [Personnes].[Compte] C
	JOIN [Personnes].[Personne] P
	ON C.PersonneID = P.PersonneID
	WHERE P.TypePersonneId = 1
	AND Nom is not null
	AND Nom <> ''
	AND Prenom is not null
	AND Prenom <> '')
SELECT * FROM PERSONNE_P
WHERE [RowNumber] BETWEEN 9700 AND 9750
GO
