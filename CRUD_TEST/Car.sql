create table dbo.Car(
CarID int identity (1,1),
Car_Brand varchar (100),
Car_Type varchar (100),
Date_Of_Purchased date,
PhotoFileName varchar (500)

)

insert into dbo.Car values
('BMW 1 Series','Hatcback','2020-01-05','1MBMW.jpg')

select * from dbo.Car