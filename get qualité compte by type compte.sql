USE [MAF_NSI]
GO

SELECT
	[TypeCompteId]
	,tc.[Name] as ' type compte name'
	,tc.[QualiteCompteId]
	,qc.[Name] as 'qualité compte name'
FROM [RefMaf].[TypeCompte] tc
join [RefMaf].[QualiteCompte] qc
on tc.QualiteCompteId = qc.QualiteCompteId
GO
