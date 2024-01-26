use [MAF_Security]
go

-- identifiants des profils de sécurité qui sont rattachés aux rôle dont le libellé contient 'Donum'
with [donum_security_profile_id] ([security_profile_id])
as (
	select distinct([SecurityProfile].[SecurityProfileId])
	from [Sec].[SecurityProfile]
	join [Sec].[SecurityProfileRole]
		on [SecurityProfile].[SecurityProfileId] = [SecurityProfileRole].[SecurityProfileId]
	join [Ref].[Role]
		on [SecurityProfileRole].[RoleId] = [Role].[RoleId]
		and [Role].[Libelle] like '%donum%'
)
select [SecurityProfile].[SecurityProfileId]
	,[SecurityProfile].[Name] as [SecurityProfileName]
	,[SecurityProfile].[Description] as [SecurityProfileDescription]
from [donum_security_profile_id]
join [Sec].[SecurityProfile]
	on [SecurityProfile].[SecurityProfileId] = [donum_security_profile_id].[security_profile_id]
order by [SecurityProfile].[Name]
GO
