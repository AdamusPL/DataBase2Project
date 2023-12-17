CREATE TABLE WorkPhone (
    UserId int NOT NULL FOREIGN KEY REFERENCES [User](Id),
    Phone varchar(12) NOT NULL,
    PRIMARY KEY (UserId)
);
