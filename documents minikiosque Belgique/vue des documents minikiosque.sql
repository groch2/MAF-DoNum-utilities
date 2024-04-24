SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [Donum].[DocumentsMNKNonTranfere]
AS
SELECT *
FROM dbo.GED_Editions ge
WHERE ge.Numéro_de_Demande NOT IN (SELECT mnk_id FROM MNK_EXT_INDEX)
AND ge.Supprime = 0;
GO
