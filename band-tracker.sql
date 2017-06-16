-- Exported from QuickDBD: https://www.quickdatatabasediagrams.com/
-- Link to schema: https://app.quickdatabasediagrams.com/#/schema/CljcO3sAlU6eJf7O3Pkmeg
-- NOTE! If you have used non-SQL datatypes in your design, you will have to change these here.

CREATE DATABASE band_tracker
GO
USE [band_tracker]
GO

CREATE TABLE "bands" (
    "id" int IDENTITY(1,1) NOT NULL ,
    "name" VARCHAR(255)  NOT NULL ,
    CONSTRAINT "pk_bands" PRIMARY KEY (
        "id"
    )
)

GO

CREATE TABLE "venues" (
    "id" int IDENTITY(1,1) NOT NULL ,
    "name" VARCHAR(255)  NOT NULL ,
    CONSTRAINT "pk_venues" PRIMARY KEY (
        "id"
    )
)

GO

CREATE TABLE "bands_venues" (
    "id" int IDENTITY(1,1) NOT NULL ,
    "band_id" int  NOT NULL ,
    "venue_id" int  NOT NULL ,
    CONSTRAINT "pk_bands_venues" PRIMARY KEY (
        "id"
    )
)

GO

CREATE DATABASE band_tracker_test
GO
USE [band_tracker_test]
GO

CREATE TABLE "bands" (
    "id" int IDENTITY(1,1) NOT NULL ,
    "name" VARCHAR(255)  NOT NULL ,
    CONSTRAINT "pk_bands" PRIMARY KEY (
        "id"
    )
)

GO

CREATE TABLE "venues" (
    "id" int IDENTITY(1,1) NOT NULL ,
    "name" VARCHAR(255)  NOT NULL ,
    CONSTRAINT "pk_venues" PRIMARY KEY (
        "id"
    )
)

GO

CREATE TABLE "bands_venues" (
    "id" int IDENTITY(1,1) NOT NULL ,
    "band_id" int  NOT NULL ,
    "venue_id" int  NOT NULL ,
    CONSTRAINT "pk_bands_venues" PRIMARY KEY (
        "id"
    )
)

GO