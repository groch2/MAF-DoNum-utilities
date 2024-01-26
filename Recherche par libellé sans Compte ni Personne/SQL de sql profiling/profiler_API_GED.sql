exec sp_executesql N'SELECT [dtoDocumentSearch].[AssigneDepartement], [dtoDocumentSearch].[AssigneGroup], [dtoDocumentSearch].[AssigneRedacteur], [dtoDocumentSearch].[AssureurId], [dtoDocumentSearch].[CanalPrincipal], 
[dtoDocumentSearch].[CanalSecondaire], [dtoDocumentSearch].[CategoriesCote], [dtoDocumentSearch].[CategoriesFamille], [dtoDocumentSearch].[CategoriesTypeDocument], [dtoDocumentSearch].[ChantierId], 
[dtoDocumentSearch].[CodeBarreId], [dtoDocumentSearch].[CodeOrigine], [dtoDocumentSearch].[Commentaire], [dtoDocumentSearch].[CompteId], [dtoDocumentSearch].[DateDocument],
[dtoDocumentSearch].[DateNum] AS [DateNumerisation], [dtoDocumentSearch].[DeposeLe], [dtoDocumentSearch].[DeposePar], [dtoDocumentSearch].[DOCN], [dtoDocumentSearch].[DocumentId],
[dtoDocumentSearch].[DocumentValide], [dtoDocumentSearch].[DuplicationId], [dtoDocumentSearch].[Link], [dtoDocumentSearch].[FichierNom], [dtoDocumentSearch].[FichierNombrePages],
COALESCE([dtoDocumentSearch].[FichierTaille], 0) AS [FichierTaille], [dtoDocumentSearch].[Horodatage], [dtoDocumentSearch].[Important], [dtoDocumentSearch].[IsHorsWorkFlowSinapps],
[dtoDocumentSearch].[Libelle], [dtoDocumentSearch].[ModifieLe], [dtoDocumentSearch].[ModifiePar], [dtoDocumentSearch].[MultiCompteId], [dtoDocumentSearch].[NumeroAvenant],
[dtoDocumentSearch].[NumeroContrat], [dtoDocumentSearch].[NumeroGc], [dtoDocumentSearch].[NumeroProposition], [dtoDocumentSearch].[NumeroSinistre],
[dtoDocumentSearch].[PeriodeValiditeDebut], [dtoDocumentSearch].[PeriodeValiditeFin], [dtoDocumentSearch].[PersonneId], [dtoDocumentSearch].[PresenceAr],
[dtoDocumentSearch].[Preview], [dtoDocumentSearch].[Priorite], [dtoDocumentSearch].[Provenance], [dtoDocumentSearch].[QualiteValideeLe], [dtoDocumentSearch].[QualiteValideePar], 
[dtoDocumentSearch].[QualiteValideeValide], [dtoDocumentSearch].[ReferenceAttestation], [dtoDocumentSearch].[ReferenceSecondaire],
[dtoDocumentSearch].[RefTiers], [dtoDocumentSearch].[RegroupementId], [dtoDocumentSearch].[Sens], [dtoDocumentSearch].[SousDossierSinistre],
[dtoDocumentSearch].[Statut], [dtoDocumentSearch].[Tenant], [dtoDocumentSearch].[TraiteLe], [dtoDocumentSearch].[TraitePar], [dtoDocumentSearch].[TypeContact],
[dtoDocumentSearch].[TypeGarantie], [dtoDocumentSearch].[VisibiliteExterne], [dtoDocumentSearch].[VisibilitePapsExtranet], [dtoDocumentSearch].[VuLe],
[dtoDocumentSearch].[VuPar]
FROM [GED].[NormalizedDocument] AS [dtoDocumentSearch]
WHERE (([dtoDocumentSearch].[Statut] = @__TypedProperty_0) 
AND [dtoDocumentSearch].[CategoriesFamille] IN (@__TypedProperty_1, @__TypedProperty_2, @__TypedProperty_3)) 
AND ((CHARINDEX(@__TypedProperty_4, [dtoDocumentSearch].[Libelle]) > 0) OR (@__TypedProperty_4 = N''''))
ORDER BY [dtoDocumentSearch].[DOCN] DESC, [dtoDocumentSearch].[DocumentId]
OFFSET @__TypedProperty_5 ROWS FETCH NEXT @__TypedProperty_6 ROWS ONLY',N'@__TypedProperty_0 nvarchar(30),@__TypedProperty_1 nvarchar(50),
@__TypedProperty_2 nvarchar(50),@__TypedProperty_3 nvarchar(50),@__TypedProperty_4 nvarchar(4000),@__TypedProperty_5 int,@__TypedProperty_6 int',
@__TypedProperty_0=N'INDEXE',@__TypedProperty_1=N'DOCUMENTS CONTRAT',@__TypedProperty_2=N'DOCUMENTS EMOA',@__TypedProperty_3=N'DOCUMENTS PERSONNES',
@__TypedProperty_4=N'amiable',@__TypedProperty_5=1000,@__TypedProperty_6=1001