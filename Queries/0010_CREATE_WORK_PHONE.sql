CREATE TABLE WorkPhone (
    UserId int NOT NULL FOREIGN KEY REFERENCES [User](Id) ON DELETE CASCADE,
    Phone nvarchar(12) NOT NULL,
    PRIMARY KEY (UserId)
);
