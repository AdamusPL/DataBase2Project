CREATE OR ALTER PROCEDURE RegisterStudent
    @Login nvarchar(255),
    @PasswordHash nvarchar(255),
    @Name nvarchar(57),
    @Surname nvarchar(100),
    @FieldsOfStudiesIdsJson nvarchar(1000)
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

        INSERT INTO [Student] (UserId)
        VALUES (@UserId);

        DECLARE @StudentId AS int = CAST(SCOPE_IDENTITY() as int);

        INSERT INTO [Student_FieldOfStudy] (StudentId, FieldOfStudyId)
        SELECT @StudentId, value AS FieldOfStudyId FROM OpenJson(@FieldsOfStudiesIdsJson);
    END TRY  
    BEGIN CATCH  
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH  
    COMMIT TRANSACTION;
END;