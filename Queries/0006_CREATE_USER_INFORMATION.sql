CREATE TABLE UserLoginInformation (
    UserId int NOT NULL FOREIGN KEY REFERENCES [User](Id) ON DELETE CASCADE,
    Login nvarchar(255) NOT NULL,
    Password nvarchar(255) NOT NULL,
    PRIMARY KEY (UserId)
);
CREATE UNIQUE INDEX UserLoginInformation_Login ON UserLoginInformation (Login);
