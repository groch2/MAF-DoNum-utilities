USE [Tarmac]
GO

declare @indicateurATraiterAdresse as nvarchar(100) = 'https://api-donum-intra.int.maf.local/api/documents/queueindicateurtarmac?documentQueueStatus=a+traiter&redacteur='
declare @indicateurATraiterId as int = (select [IndicateurId] FROM [Tarmac].[Indicateur] WHERE [ApiUrl] = @indicateurATraiterAdresse)
update [Tarmac].[Indicateur]
set [ApiUrl] = 'http://localhost:56326/fileAttente?userCode='
where [IndicateurId] = @indicateurATraiterId

declare @indicateurNouveauAdresse as nvarchar(100) = 'https://api-donum-intra.int.maf.local/api/documents/queueindicateurtarmac?documentQueueStatus=nouveau&redacteur='
declare @indicateurNouveauId as int = (select [IndicateurId] FROM [Tarmac].[Indicateur] WHERE [ApiUrl] = @indicateurNouveauAdresse)
update [Tarmac].[Indicateur]
set [ApiUrl] = 'http://localhost:56326/fileAttente?userCode='
where [IndicateurId] = @indicateurNouveauId
