create database ProiectSgbd
use ProiectSgbd
create table tCursuri
(CodCurs varchar(30),
NumeCurs varchar(30) primary key,
NrCredite int
)
create table tProfesori
(Nume varchar(30),
Vechime int,
NumeCurs varchar(30) constraint pk_numec primary key references tCursuri(NumeCurs)
)

create table tStudenti
(CodStudent varchar(30) primary key,
NumeStudent varchar(30),
AnUniversitar int
)
create table tNote
(NumeCurs varchar(30) constraint fk_nume foreign key references tCursuri(NumeCurs),
CodStudent varchar(30)  constraint fk_cod foreign key references tStudenti(CodStudent),
Nota int
)

create table tConturi
(Email varchar(30) primary key,
Parola varchar(30),
EsteAdmin bit
)


create procedure ps_DeleteCont(@Email char(30))
as 
begin
delete from tConturi
where Email=@Email
end
select * from tNote