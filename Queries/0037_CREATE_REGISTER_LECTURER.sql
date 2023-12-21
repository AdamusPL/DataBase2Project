CREATE OR ALTER PROCEDURE RegisterLecturer
    @Login nvarchar(255),
    @PasswordHash nvarchar(255),
    @Name nvarchar(57),
    @Surname nvarchar(100),
    @EmailsJson nvarchar(1000)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO [User] (Name, Surname)
        VALUES (@Name, @Surname);

        DECLARE @UserId AS int = CAST(SCOPE_IDENTITY() as int);

        INSERT INTO [UserLoginInformation] (UserId, Login, Password)
        VALUES (@UserId, @Login, @PasswordHash);

        INSERT INTO [Lecturer] (UserId)
        VALUES (@UserId);

        INSERT INTO [Email] (UserId, Email)
        SELECT @UserId, value AS Email FROM OpenJson(@EmailsJson);
    END TRY  
    BEGIN CATCH  
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH  
    COMMIT TRANSACTION;
END;