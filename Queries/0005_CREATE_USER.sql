CREATE TABLE [User] (
    Id int IDENTITY(1, 1) NOT NULL,
    Name nvarchar(57) NOT NULL,
    Surname nvarchar(100) NOT NULL,
    PRIMARY KEY (Id)
);

CREATE INDEX User_Name ON [User] (Name);
CREATE INDEX User_Surname ON [User] (Surname);
CREATE INDEX User_Name_Surname ON [User] (Name, Surname);
