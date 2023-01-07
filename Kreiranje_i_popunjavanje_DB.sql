--Ukljucio sam i ovaj fajl ukoliko je potrebno kreirati novu SQL bazu podataka sa tabelama i popuniti ih
--create database Biblioteka;
use Biblioteka
create table Clan (
	id int identity(1,1) primary key,
	ime nvarchar(50),
	prezime nvarchar(50)
);
create table Knjiga (
	id int identity(1,1) primary key,
	naslov nvarchar(50),
	autor nvarchar(50),
	godina_izdavanja int,
	kod_clana int,
	foreign key (kod_clana) references Clan(id)
);

insert into Clan (ime, prezime) values
	('Petar', 'Petrovic'),
	('Janko', 'Jankovic'),
	('Marko', 'Markovic'),
	('Ana', 'Anicic'),
	('Petra', 'Petric'),
	('Salimana', 'Podbaric'),
	('Tariffa', 'Strujinic');

insert into Knjiga (naslov, autor, godina_izdavanja, kod_clana) values
	('Na Drini cuprija', 'Ivo Andric', '1997', 2),
	('Dervis i smrt', 'Mesa Selimovic', '1998', NULL),
	('Beli ocnjak', 'Dzek London', '1990', 6);