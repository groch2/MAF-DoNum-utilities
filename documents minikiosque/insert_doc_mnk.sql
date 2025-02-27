USE [editions]
GO

INSERT INTO [dbo].[GED_Editions]
           ([Num?_de_Demande]
           ,[Nom_du_Document]
           ,[Date_Impression]
           ,[Num?_du_Destinataire]
           ,[Cle_du_Destinataire]
           ,[Date_de_la_Demande]
           ,[Heure_de_la_Demande]
           ,[Demandeur]
           ,[Flag_Edition]
           ,[OLE_Document]
           ,[Chemin]
           ,[csocsisi]
           ,[annesisi]
           ,[cpopsisi]
           ,[sinizzid]
           ,[sinczzid]
           ,[prpnzzid]
           ,[prpczzid]
           ,[polnzzid]
           ,[polczzid]
           ,[Fiche_Jurisprudence]
           ,[Supprime]
           ,[DateDebut]
           ,[DateFin]
           ,[numechan]
           ,[ReadOnly]
           ,[FlagGedIris])
SELECT 
(SELECT MAX([Num?_de_Demande]) + 1 FROM [dbo].[GED_Editions])
,[Nom_du_Document]
,[Date_Impression]
,16803
,[Cle_du_Destinataire]
,[Date_de_la_Demande]
,[Heure_de_la_Demande]
,'ROD'--[Demandeur]
,[Flag_Edition]
,[OLE_Document]
,[Chemin]
,[csocsisi]
,[annesisi]
,[cpopsisi]
,[sinizzid]
,[sinczzid]
,[prpnzzid]
,[prpczzid]
,[polnzzid]
,[polczzid]
,[Fiche_Jurisprudence]
,[Supprime]
,[DateDebut]
,[DateFin]
,[numechan]
,[ReadOnly]
,[FlagGedIris]
from [dbo].[GED_Editions]
where [Num?_de_Demande] = 1460495
GO
