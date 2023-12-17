CREATE TABLE Faculty (
    Id varchar(10) NOT NULL,
    Name varchar(100) NOT NULL,
    PRIMARY KEY (Id)
);
CREATE UNIQUE INDEX Faculty_Name ON Faculty (Name);
