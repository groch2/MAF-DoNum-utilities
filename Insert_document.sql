use [GEDMAF]
go
declare @FROM_DOCN as varchar(9) = '000000012'
declare @DOCN as varchar(9) = '023456712'
declare @ID_GED as varchar(32) = '20150622010006660000908'
declare @ID_DOC as varchar(32) = @ID_GED

declare @ID_DOC_AVAILABLE as bit = (SELECT CASE WHEN COUNT(0) = 0 THEN 1 ELSE 0 END FROM [dbo].[ARCHEAMAF] WHERE [ID_DOC] = @ID_DOC)
declare @DOCN_AVAILABLE as bit = (SELECT CASE WHEN COUNT(0) = 0 THEN 1 ELSE 0 END FROM [dbo].[ARCHEAMAF] WHERE [DOCN] = @DOCN)

IF @ID_DOC_AVAILABLE = 0 OR @DOCN_AVAILABLE = 0
BEGIN
	IF @ID_DOC_AVAILABLE = 0
		PRINT 'ID DOC NOT AVAILABLE'
	IF @DOCN_AVAILABLE = 0
		PRINT 'DOCN NOT AVAILABLE'
END
ELSE
BEGIN
	declare @CODE_GESTIONNAIRE_REDAC as varchar(8) = 'ROD'
	declare @NO_ADHERENT as int = 1974
	INSERT INTO [dbo].[ARCHEAMAF]
			   ([DOCN]
			   ,[ID_GED]
			   ,[ID_DOC]
			   ,[DATE_ARCHIVAGE]
			   ,[NOM_MEDIA]
			   ,[STATUS]
			   ,[LINK_ANNOT]
			   ,[LINK_OPTIQUE]
			   ,[ANNOT_DOC_OPTIQUE]
			   ,[LINK]
			   ,[DATE_GRAVAGE]
			   ,[SITE_NAME]
			   ,[LINK_SITE]
			   ,[FUSION]
			   ,[FLAG_DISPATCH]
			   ,[FLAG_RETOURS]
			   ,[DATE_NUM]
			   ,[HEURE_NUM]
			   ,[REF_ARCHIVE]
			   ,[CODE_PIECE]
			   ,[DATE_RECEPT]
			   ,[SITE_NUM]
			   ,[OPER_NUM]
			   ,[NO_DOSSIER]
			   ,[NO_ADHERENT]
			   ,[REF_TIERS]
			   ,[REF_AUTRE]
			   ,[PROVENANCE]
			   ,[PROVENANCE_DETAIL]
			   ,[DESTINATAIRE]
			   ,[DESTINATAIRE_DETAIL]
			   ,[GESTIONNAIRE_REDAC]
			   ,[DATE_DOCUMENT]
			   ,[DATE_INTEGRATION]
			   ,[OPERATEUR]
			   ,[SERVICE]
			   ,[GROUPE]
			   ,[SENS]
			   ,[MODE]
			   ,[NATURE]
			   ,[FAMILLE]
			   ,[TYPE_DOC]
			   ,[COTE]
			   ,[PER_SRV_A_NOTIFIER]
			   ,[CONSERV_ORIGINAL]
			   ,[PHASE]
			   ,[VALEUR_JURIDIQUE]
			   ,[LECTURE_PAPIER]
			   ,[CONSERVATION_TEMP]
			   ,[ARCHIVAGE_LONG]
			   ,[TYPE_CONTACT]
			   ,[DATE_DELAI1]
			   ,[DATE_DELAI2]
			   ,[PRIORITE]
			   ,[NEXT_ACTION1]
			   ,[NEXT_ACTION2]
			   ,[DATE_NEXT_ACTION1]
			   ,[DATE_NEXT_ACTION2]
			   ,[OBSERVATION]
			   ,[PARTAGEABILITE]
			   ,[PAGE]
			   ,[STATUS_INDEXATION]
			   ,[DATE_CLOTURE]
			   ,[CODEPIECE]
			   ,[ID_NXO]
			   ,[REF_NXO]
			   ,[USER_NXO]
			   ,[CACHE_01]
			   ,[CACHE_02]
			   ,[CACHE_03]
			   ,[CACHE_04]
			   ,[CACHE_05]
			   ,[CACHE_06]
			   ,[CACHE_07]
			   ,[CACHE_08]
			   ,[CACHE_09]
			   ,[CACHE_10]
			   ,[FichierOrigine]
			   ,[DATE_PURGE]
			   ,[NO_CONTRAT]
			   ,[ID_REASS]
			   ,[LIB_REASS]
			   ,[ID_TRAITE]
			   ,[LIB_TRAITE]
			   ,[ACCESS]
			   ,[NO_TRAITE]
			   ,[ANNEE_DEB]
			   ,[ANNEE_FIN]
			   ,[NO_AVENANT]
			   ,[ID_CODESOC]
			   ,[TYPE_ADHERENT]
			   ,[DOC_SIZE]
			   ,[NO_DP]
			   ,[NO_GC]
			   ,[NO_PROPOS]
			   ,[CODE_GESTIONNAIRE_REDAC]
			   ,[LIB_DOC]
			   ,[PRESENCE_AR]
			   ,[VISIBILITE_EXTERNE]
			   ,[CODE_ORIGINE]
			   ,[REF_SECONDAIRE]
			   ,[DATE_MAJ]
			   ,[CODE_MAJ]
			   ,[CODE_SOUS_DOSSIER]
			   ,[DATE_VISU]
			   ,[CODE_REDACTEUR_VISU]
			   ,[DATE_QUALITE]
			   ,[CODE_REDACTEUR_QUALITE]
			   ,[VALIDE_QUALITE]
			   ,[CODE_REDACTEUR_DEPOSE]
			   ,[DATE_TRAITE_DOC]
			   ,[CODE_REDACTEUR_TRAITE_DOC]
			   ,[CODE_DEPARTEMENT]
			   ,[DIGEST]
			   ,[PREVIEW]
			   ,[MultiCompteId]
			   ,[ChantierId]
			   ,[DateValiditeFin]
			   ,[DateValiditeDebut]
			   ,[CodeAssureur]
			   ,[TypeGarantie]
			   ,[Conformite]
			   ,[Commentaire]
			   ,[DocumentValide]
			   ,[ReferenceAttestation]
			   ,[VisibilitePapsExtranet]
			   ,[PersonneID]
			   ,[DuplicationId]
			   ,[numeroGC]
			   ,[Tenant]
			   ,[Horodatage])
	SELECT @DOCN
		  ,@ID_GED
		  ,@ID_DOC
		  ,[DATE_ARCHIVAGE]
		  ,[NOM_MEDIA]
		  ,[STATUS]
		  ,[LINK_ANNOT]
		  ,[LINK_OPTIQUE]
		  ,[ANNOT_DOC_OPTIQUE]
		  ,[LINK]
		  ,[DATE_GRAVAGE]
		  ,[SITE_NAME]
		  ,[LINK_SITE]
		  ,[FUSION]
		  ,[FLAG_DISPATCH]
		  ,[FLAG_RETOURS]
		  ,[DATE_NUM]
		  ,[HEURE_NUM]
		  ,[REF_ARCHIVE]
		  ,[CODE_PIECE]
		  ,[DATE_RECEPT]
		  ,[SITE_NUM]
		  ,[OPER_NUM]
		  ,[NO_DOSSIER]
		  ,@NO_ADHERENT
		  ,[REF_TIERS]
		  ,[REF_AUTRE]
		  ,[PROVENANCE]
		  ,[PROVENANCE_DETAIL]
		  ,[DESTINATAIRE]
		  ,[DESTINATAIRE_DETAIL]
		  ,[GESTIONNAIRE_REDAC]
		  ,[DATE_DOCUMENT]
		  ,[DATE_INTEGRATION]
		  ,[OPERATEUR]
		  ,[SERVICE]
		  ,[GROUPE]
		  ,[SENS]
		  ,[MODE]
		  ,[NATURE]
		  ,[FAMILLE]
		  ,[TYPE_DOC]
		  ,[COTE]
		  ,[PER_SRV_A_NOTIFIER]
		  ,[CONSERV_ORIGINAL]
		  ,[PHASE]
		  ,[VALEUR_JURIDIQUE]
		  ,[LECTURE_PAPIER]
		  ,[CONSERVATION_TEMP]
		  ,[ARCHIVAGE_LONG]
		  ,[TYPE_CONTACT]
		  ,[DATE_DELAI1]
		  ,[DATE_DELAI2]
		  ,[PRIORITE]
		  ,[NEXT_ACTION1]
		  ,[NEXT_ACTION2]
		  ,[DATE_NEXT_ACTION1]
		  ,[DATE_NEXT_ACTION2]
		  ,[OBSERVATION]
		  ,[PARTAGEABILITE]
		  ,[PAGE]
		  ,[STATUS_INDEXATION]
		  ,[DATE_CLOTURE]
		  ,[CODEPIECE]
		  ,[ID_NXO]
		  ,[REF_NXO]
		  ,[USER_NXO]
		  ,[CACHE_01]
		  ,[CACHE_02]
		  ,[CACHE_03]
		  ,[CACHE_04]
		  ,[CACHE_05]
		  ,[CACHE_06]
		  ,[CACHE_07]
		  ,[CACHE_08]
		  ,[CACHE_09]
		  ,[CACHE_10]
		  ,[FichierOrigine]
		  ,[DATE_PURGE]
		  ,[NO_CONTRAT]
		  ,[ID_REASS]
		  ,[LIB_REASS]
		  ,[ID_TRAITE]
		  ,[LIB_TRAITE]
		  ,[ACCESS]
		  ,[NO_TRAITE]
		  ,[ANNEE_DEB]
		  ,[ANNEE_FIN]
		  ,[NO_AVENANT]
		  ,[ID_CODESOC]
		  ,[TYPE_ADHERENT]
		  ,[DOC_SIZE]
		  ,[NO_DP]
		  ,[NO_GC]
		  ,[NO_PROPOS]
		  ,@CODE_GESTIONNAIRE_REDAC
		  ,[LIB_DOC]
		  ,[PRESENCE_AR]
		  ,[VISIBILITE_EXTERNE]
		  ,[CODE_ORIGINE]
		  ,[REF_SECONDAIRE]
		  ,[DATE_MAJ]
		  ,[CODE_MAJ]
		  ,[CODE_SOUS_DOSSIER]
		  ,[DATE_VISU]
		  ,[CODE_REDACTEUR_VISU]
		  ,[DATE_QUALITE]
		  ,[CODE_REDACTEUR_QUALITE]
		  ,[VALIDE_QUALITE]
		  ,[CODE_REDACTEUR_DEPOSE]
		  ,[DATE_TRAITE_DOC]
		  ,[CODE_REDACTEUR_TRAITE_DOC]
		  ,[CODE_DEPARTEMENT]
		  ,[DIGEST]
		  ,[PREVIEW]
		  ,[MultiCompteId]
		  ,[ChantierId]
		  ,[DateValiditeFin]
		  ,[DateValiditeDebut]
		  ,[CodeAssureur]
		  ,[TypeGarantie]
		  ,[Conformite]
		  ,[Commentaire]
		  ,[DocumentValide]
		  ,[ReferenceAttestation]
		  ,[VisibilitePapsExtranet]
		  ,[PersonneID]
		  ,[DuplicationId]
		  ,[numeroGC]
		  ,[Tenant]
		  ,[Horodatage]
	FROM [dbo].[ARCHEAMAF]
	where [DOCN]= @FROM_DOCN
END