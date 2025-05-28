INSERT INTO Roles (Name) VALUES ('admin');
INSERT INTO Roles (Name) VALUES ('kierownik');
INSERT INTO Roles (Name) VALUES ('pracownik');
INSERT INTO Roles (Name) VALUES ('dostawca');
INSERT INTO Roles (Name) VALUES ('spedytor');
INSERT INTO Roles (Name) VALUES ('magazynier');
iNSERT INTO Roles (Name) VALUES ('klient');
Insert into Dzialy (Nazwa) VALUES ('IT');
INSERT INTO Pracownicy (Name, Surname, Id_stanowiska, Id_dzialu, Addres, Nr_tel)
VALUES
('Alicja', 'Kruk', 1, 3, 'ul. Krokusowa 24, Poznań', '+48 500650575')
INSERT INTO Pracownicy (Name, Surname, Id_stanowiska, Id_dzialu, Addres, Nr_tel)
VALUES
('admin', 'admin', 1, 3, ' - ', ' - ')
insert into Users (Username, PasswordHash, RoleId, PracownikId) 
values 
('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 1, 802);