USE [Tarmac]
GO

declare @environment_code as varchar(3) = 'INT'
INSERT INTO [Tarmac].[Indicateur]
           ([IsActif]
           ,[ApiUrl]
           ,[Couleur]
           ,[IsVueManagerExist])
     VALUES
           (1
           ,concat('https://api-donum-intra.', @environment_code, '.maf.local/api/documents/queueindicateurtarmac?documentQueueStatus=a+traiter&redacteur=')
           ,'#2596be'
           ,0)
declare @IndicateurId_donum_file_d_attente_a_traiter as int = (select SCOPE_IDENTITY())
INSERT INTO [Tarmac].[ProfilIndicateur]
           ([ProfilId]
           ,[IndicateurId]
           ,[Ordre])
     VALUES
           (68
           ,@IndicateurId_donum_file_d_attente_a_traiter
           ,0)
print(@IndicateurId_donum_file_d_attente_a_traiter)

INSERT INTO [Tarmac].[Indicateur]
           ([IsActif]
           ,[ApiUrl]
           ,[Couleur]
           ,[IsVueManagerExist])
     VALUES
           (1
           ,concat('https://api-donum-intra.', @environment_code, '.maf.local/api/documents/queueindicateurtarmac?documentQueueStatus=nouveau&redacteur=')
           ,'#2596be'
           ,0)
declare @IndicateurId_donum_file_d_attente_nouveau as int = (select SCOPE_IDENTITY())
INSERT INTO [Tarmac].[ProfilIndicateur]
           ([ProfilId]
           ,[IndicateurId]
           ,[Ordre])
     VALUES
           (68
           ,@IndicateurId_donum_file_d_attente_nouveau
           ,0)
print(@IndicateurId_donum_file_d_attente_nouveau)
GO
