CREATE OR ALTER TRIGGER fill_plac_buraczany
ON dostawy
AFTER INSERT
AS
BEGIN
    INSERT INTO Plac_buraczany (Id_dostawy, Ilosc_burakow, Data_operacji)
    SELECT id_dostawy, Ilosc_towaru, data_dostawy
    FROM inserted;
END;