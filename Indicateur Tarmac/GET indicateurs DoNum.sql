USE [Tarmac]
GO

SELECT [Indicateur].[IndicateurId]
      ,[Indicateur].[IsActif]
      ,[Indicateur].[ApiUrl]
	  ,[ProfilIndicateur].ProfilId
	  ,[MAF_Security].[Sec].[SecurityProfile].[Name] AS [security profile name]
FROM [Tarmac].[Indicateur]
JOIN [Tarmac].[ProfilIndicateur]
ON [Indicateur].IndicateurId = [ProfilIndicateur].IndicateurId
JOIN [MAF_Security].[Sec].[SecurityProfile]
ON [MAF_Security].[Sec].[SecurityProfile].[SecurityProfileId] = [ProfilIndicateur].ProfilId
WHERE [ApiUrl] LIKE '%/api/documents/queueindicateurtarmac%'
ORDER BY [Indicateur].[IndicateurId], [ProfilIndicateur].ProfilId
GO
