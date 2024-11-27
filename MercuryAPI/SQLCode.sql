alter procedure AddPerson(
	@Personnr varchar(13),
	@FirstName varchar(32),
	@LastName varchar(32),
	@YearOfBirth int,

	@ID int output
	)
as begin
	insert into
		Persons
	values
		(@Personnr, @FirstName, @LastName, @YearOfBirth)

	set @ID = SCOPE_IDENTITY();
end
go


create procedure UpdatePerson
	(
		@ID int,
		@Personnr varchar(13),
		@FirstName varchar(32),
		@LastName varchar(32),
		@YearOfBirth int
	)
as 
begin
	update
		Persons
	set
		Personnr = @Personnr,
		FirstName = @FirstName,
		LastName = @LastName,
		YearOfBirth = @YearOfBirth
	where
		Persons.ID = @ID
end
go

select 
	P.*,
	'---' as [ ],
	K.*
from 
	Persons as P
left join
	Kontaktuppgifter as K on
	K.PersonsID = P.ID
go


insert into
	Persons
values
	('19910410-5235', 'Rasmus', 'Läckberg', 1991)
go