create table dbo.Company(
CompanyID int identity(1,1),
CompanyName varchar (500)
)

select * from dbo.Company

insert into dbo.Company values(
'Finance'
)