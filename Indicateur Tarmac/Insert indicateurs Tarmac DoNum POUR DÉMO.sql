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
           ,'http://localhost:56326/api/documents/queueindicateurtarmac/1/'
           ,'#2596be'
           ,0)
declare @IndicateurId_donum_file_d_attente_a_traiter as int = (select SCOPE_IDENTITY())
INSERT INTO [Tarmac].[ProfilIndicateur]
           ([ProfilId]
           ,[IndicateurId]
           ,[Ordre])
     VALUES
           (14
           ,@IndicateurId_donum_file_d_attente_a_traiter
           ,0)
print(concat('indicateur "à traiter": ', @IndicateurId_donum_file_d_attente_a_traiter))

INSERT INTO [Tarmac].[Indicateur]
           ([IsActif]
           ,[ApiUrl]
           ,[Couleur]
           ,[IsVueManagerExist])
     VALUES
           (1
           ,'http://localhost:56326/api/documents/queueindicateurtarmac/0/'
           ,'#2596be'
           ,0)
declare @IndicateurId_donum_file_d_attente_nouveau as int = (select SCOPE_IDENTITY())
INSERT INTO [Tarmac].[ProfilIndicateur]
           ([ProfilId]
           ,[IndicateurId]
           ,[Ordre])
     VALUES
           (14
           ,@IndicateurId_donum_file_d_attente_nouveau
           ,0)
print(concat('indicateur "nouveau": ', @IndicateurId_donum_file_d_attente_nouveau))
GO
