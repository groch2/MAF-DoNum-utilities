select
	 Numéro_du_Destinataire AS CompteId
	,Numéro_de_Demande AS Document_Id
	,Date_de_la_Demande
from editions.dbo.GED_Editions
where Numéro_de_Demande not in (select MNK_ID from editions.dbo.MNK_EXT_INDEX)
and Supprime = 0
and Numéro_du_Destinataire is not null