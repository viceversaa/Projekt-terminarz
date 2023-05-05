CREATE TABLE s_stopien_zaawansowania(
	id_stopien_zaawansowania int NOT NULL PRIMARY KEY,
	stopien varchar(20)	NOT NULL
);
INSERT INTO s_stopien_zaawansowania VALUES (1, 'poczatkujacy')
INSERT INTO s_stopien_zaawansowania VALUES (2, 'średniozaawansowany')
INSERT INTO s_stopien_zaawansowania VALUES (3, 'zaawansowany')

UPDATE s_stopien_zaawansowania
SET stopien = 'początkujący'
WHERE id_stopien_zaawansowania = 1;

CREATE TABLE s_plec(
	id_plec int NOT NULL PRIMARY KEY,
	typ varchar(10) NOT NULL
);
INSERT INTO s_plec VALUES (1, 'kobieta')
INSERT INTO s_plec VALUES (2, 'mezczyzna')

CREATE TABLE osoby (
	id_osoba int NOT NULL PRIMARY KEY,
	nazwisko varchar(50) NOT NULL,
	imie	varchar(30) NOT NULL,
	drugie_imie varchar(30),
	id_plec int NOT NULL FOREIGN KEY REFERENCES s_plec(id_plec),
	data_urodzin date NOT NULL
);
ALTER TABLE osoby
ADD nr_telefonu varchar(30) NOT NULL, email	varchar(50)

ALTER TABLE osoby
ADD haslo varchar(30)
-------------------- porobic inserty do osob --------------------

CREATE TABLE kursanci (
	id_kursant int NOT NULL PRIMARY KEY,
	id_osoba int NOT NULL FOREIGN KEY REFERENCES osoby(id_osoba),
	id_stopien_zaawansowania int FOREIGN KEY REFERENCES s_stopien_zaawansowania(id_stopien_zaawansowania)
);
ALTER TABLE kursanci
ADD wzrost_cm int NOT NULL, waga_kg float NOT NULL
-------------------- dodac kursantow --------------------

CREATE TABLE s_funkcje (
	id_funkcja int NOT NULL PRIMARY KEY,
	funkcja varchar(30) NOT NULL
);
INSERT INTO s_funkcje VALUES (1, 'właściciel/ka')
INSERT INTO s_funkcje VALUES (2, 'instruktor/ka')

CREATE TABLE pracownicy(
	id_pracownik int NOT NULL PRIMARY KEY,
	id_osoba int NOT NULL FOREIGN KEY REFERENCES osoby(id_osoba),
	id_funkcja int NOT NULL FOREIGN KEY REFERENCES s_funkcje(id_funkcja)
);
ALTER TABLE pracownicy
ADD aktywny int NOT NULL
-------------------- dodac pracownikow --------------------

CREATE TABLE s_typ_konia (
	id_typ_konia int NOT NULL PRIMARY KEY,
	typ	varchar(10) NOT NULL
);

INSERT INTO s_typ_konia VALUES (1, 'koń')
INSERT INTO s_typ_konia VALUES (2, 'kucyk')

CREATE TABLE konie (
	id_kon int NOT NULL PRIMARY KEY,
	imie varchar(50) NOT NULL,
	id_typ_konia int NOT NULL FOREIGN KEY REFERENCES s_typ_konia(id_typ_konia),
	wytrzymalosc_kg int,
	id_stopien_zaawansowania int FOREIGN KEY REFERENCES s_stopien_zaawansowania(id_stopien_zaawansowania)
);

CREATE TABLE s_typ_zajec(
	id_typ_zajec int NOT NULL PRIMARY KEY,
	typ varchar(20) NOT NULL,
	cena int,
	czas_min int,
	uwagi varchar(255)
);

INSERT INTO s_typ_zajec(id_typ_zajec, typ, cena, czas_min) VALUES(1, 'jazda indywidualna', 150, 60)
INSERT INTO s_typ_zajec VALUES(2, 'jazda grupowa', 80, 60, 'cena za osobę, dozwolona ilość osób: 2-5')
INSERT INTO s_typ_zajec(id_typ_zajec, typ, cena, czas_min) VALUES(3, 'lonża', 90, 30)

CREATE TABLE lekcje(
	id_lekcja INT NOT NULL PRIMARY KEY,
	dzien_data date,
	godzina varchar(6),
	id_typ_zajec INT FOREIGN KEY REFERENCES s_typ_zajec(id_typ_zajec),
	id_pracownik INT FOREIGN KEY REFERENCES pracownicy(id_pracownik),
	id_kursant INT FOREIGN KEY REFERENCES kursanci(id_kursant)
);

ALTER TABLE lekcje
ADD id_kon INT  FOREIGN KEY REFERENCES konie(id_kon)