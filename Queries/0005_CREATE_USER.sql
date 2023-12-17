CREATE TABLE [User] (
    Id int IDENTITY(1, 1) NOT NULL,
    Name varchar(57) NOT NULL,
    Surname varchar(100) NOT NULL,
    PRIMARY KEY (Id)
);

CREATE INDEX User_Name ON [User] (Name);
CREATE INDEX User_Surname ON [User] (Surname);
CREATE INDEX User_Name_Surname ON [User] (Name, Surname);
