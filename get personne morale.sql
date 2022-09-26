USE [MAF_NSI]
GO

SELECT TOP 100 [CompteID]
      ,P.[PersonneID]
	  ,P.RaisonSociale
FROM [Personnes].[Compte] C
JOIN [Personnes].[Personne] P
on C.PersonneID = P.PersonneID
where p.TypePersonneId = 2
and RaisonSociale is not null
and RaisonSociale <> ''
GO
