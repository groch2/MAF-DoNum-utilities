SELECT TOP 100
	[CompteID]
	,[QualiteCompte].[QualiteCompteId]
FROM [MAF_NSI].[Personnes].[Compte]
JOIN [MAF_NSI].[RefMaf].[TypeCompte]
ON [Compte].[TypeCompteID] = [TypeCompte].[TypeCompteId]
JOIN [MAF_NSI].[RefMaf].[QualiteCompte]
ON [TypeCompte].[QualiteCompteId] = [QualiteCompte].[QualiteCompteId]
GO
