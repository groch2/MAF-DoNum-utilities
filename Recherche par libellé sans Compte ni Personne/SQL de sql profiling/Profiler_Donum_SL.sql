declare @p2 dbo.ListOfIntType

declare @p3 dbo.ListOfNumeroContratType

declare @p4 dbo.ListOfIdDocType

declare @p6 dbo.ListOfFamilleType
insert into @p6 values(N'DOCUMENTS EMOA')
insert into @p6 values(N'DOCUMENTS CONTRAT')
insert into @p6 values(N'DOCUMENTS PERSONNES')

declare @p7 dbo.ListOfCoteType

exec [GED].[SearchGecoDocument] @CompteId=default,@ChantierIds=@p2,@NumerosContrats=@p3,@AttestationDocIds=@p4,@PersonneId=default,
@Familles=@p6,@Cotes=@p7,@TypeDoc=default,@LibelleDocument='amiable',@Redacteur='',@DateDocumentDebut=default,
@DateDocumentFin=default,@Conformite=default,@DateValiditeDebut=default,@DateValiditeFin=default,@CodeAssureur=default,
@Digest=default,@Sens =default,@NumeroSinistre=default,@Provenance=default,@HasDateQualite=default,@HasDateTraiteDoc=default,
@HasDateVisu=default,@ValideQualite=default,@Groupe=default,@CodeGestionnaireRedacteur=default,@CodeRedacteurQualite=default,
@CodeRedacteurTraiteDoc=default,@CodeRedacteurVisu=default,@RegroupementId=default,@PageNo=0,@PageSize=1000,@NumeroGC=default,
@SortColumn=default,@SortColumnOrder='ASC'