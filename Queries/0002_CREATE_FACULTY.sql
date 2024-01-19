CREATE TABLE Faculty (
    Id nvarchar(10) NOT NULL,
    Name nvarchar(100) NOT NULL,
    PRIMARY KEY (Id)
);
CREATE UNIQUE INDEX Faculty_Name ON Faculty (Name);
