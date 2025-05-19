-- PROCEDURA
IF OBJECT_ID('dbo.update_plac_buraczany', 'P') IS NOT NULL
    DROP PROCEDURE dbo.update_plac_buraczany;
GO

-- TRIGGERS
IF OBJECT_ID('dbo.fill_plac_buraczany', 'TR') IS NOT NULL
    DROP TRIGGER dbo.fill_plac_buraczany;
GO

IF OBJECT_ID('dbo.fill_plac_produktownia', 'TR') IS NOT NULL
    DROP TRIGGER dbo.fill_plac_produktownia;
GO

IF OBJECT_ID('dbo.fill_silos', 'TR') IS NOT NULL
    DROP TRIGGER dbo.fill_silos;
GO

IF OBJECT_ID('dbo.fill_silos_pakownia', 'TR') IS NOT NULL
    DROP TRIGGER dbo.fill_silos_pakownia;
GO

IF OBJECT_ID('dbo.fill_magazyn', 'TR') IS NOT NULL
    DROP TRIGGER dbo.fill_magazyn;
GO

IF OBJECT_ID('dbo.fill_magazyn_sprzedaz', 'TR') IS NOT NULL
    DROP TRIGGER dbo.fill_magazyn_sprzedaz;
GO

IF OBJECT_ID('dbo.update_plac_buraczany_trigger', 'TR') IS NOT NULL
    DROP TRIGGER dbo.update_plac_buraczany_trigger;
GO

IF OBJECT_ID('dbo.update_plac_produktownia', 'TR') IS NOT NULL
    DROP TRIGGER dbo.update_plac_produktownia;
GO

IF OBJECT_ID('dbo.update_silos', 'TR') IS NOT NULL
    DROP TRIGGER dbo.update_silos;
GO

IF OBJECT_ID('dbo.update_silos_pakownia', 'TR') IS NOT NULL
    DROP TRIGGER dbo.update_silos_pakownia;
GO

IF OBJECT_ID('dbo.update_magazyn', 'TR') IS NOT NULL
    DROP TRIGGER dbo.update_magazyn;
GO

IF OBJECT_ID('dbo.update_magazyn_sprzedaz', 'TR') IS NOT NULL
    DROP TRIGGER dbo.update_magazyn_sprzedaz;
GO
