USE [Contrat]
GO

SELECT [Numéro_du_Destinataire] as 'compte id'
      ,count([Numéro_du_Destinataire]) as 'nb documents minikiosque'
FROM [Donum].[DocumentsMNKNonTranfere]
--where [Numéro_du_Destinataire] = 200234
group by [Numéro_du_Destinataire]
having count([Numéro_du_Destinataire]) > 0
GO
