CREATE TABLE UserLoginInformation (
    UserId int NOT NULL FOREIGN KEY REFERENCES [User](Id),
    Login varchar(255) NOT NULL,
    Password varchar(255) NOT NULL,
    PRIMARY KEY (UserId)
);
CREATE UNIQUE INDEX UserLoginInformation_Login ON UserLoginInformation (Login);
