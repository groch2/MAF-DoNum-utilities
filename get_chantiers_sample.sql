use [Contrat]
go

with chantier ([ROW_NUMBER], [ChantierID], [ChantierEtatID], [DateCrea], [DateDebut], [DateMod], [DateOuvertureChantier], [Description], [OrigineChantierID], [ReferenceGC], [TypeChantierID], [UserCrea], [UserMod], [ValidFrom], [ValidTo]) as (
    SELECT ROW_NUMBER() OVER(ORDER BY [ChantierID] ASC)
        ,[ChantierID]
        ,[ChantierEtatID]
        ,[DateCrea]
        ,[DateDebut]
        ,[DateMod]
        ,[DateOuvertureChantier]
        ,[Description]
        ,[OrigineChantierID]
        ,[ReferenceGC]
        ,[TypeChantierID]
        ,[UserCrea]
        ,[UserMod]
        ,[ValidFrom]
        ,[ValidTo]
    FROM [Contrat].[Geco].[Chantier]
    where [ReferenceGC] is not null)
select [ChantierID]
    ,[ReferenceGC]
    ,[ValidFrom]
    ,[ValidTo]
    ,[ChantierEtatID]
    -- ,[DateCrea]
    ,[DateDebut]
    -- ,[DateMod]
    ,[DateOuvertureChantier]
    ,[Description]
    ,[OrigineChantierID]
    ,[TypeChantierID]
    ,[UserCrea]
    ,[UserMod]
from chantier
where [ROW_NUMBER] between 100 and 200