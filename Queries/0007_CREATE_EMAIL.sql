CREATE TABLE Email (
    Id int IDENTITY(1, 1) NOT NULL,
    UserId int NOT NULL FOREIGN KEY REFERENCES [User](Id),
    Email varchar(255) NOT NULL,
    PRIMARY KEY (Id)
);

