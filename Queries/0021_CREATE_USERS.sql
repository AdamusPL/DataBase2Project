USE [University-Main];

CREATE LOGIN Administration WITH PASSWORD = 'zaq1@WSX';
CREATE USER AdministrationUser FOR LOGIN Administration;
CREATE ROLE AdministrationRole;
ALTER ROLE AdministrationRole ADD MEMBER AdministrationUser;
GO


CREATE LOGIN Student WITH PASSWORD = 'zaq1@WSX';
CREATE USER StudentUser FOR LOGIN Student;
CREATE ROLE StudentRole;
ALTER ROLE StudentRole ADD MEMBER StudentUser;
GO


CREATE LOGIN Lecturer WITH PASSWORD = 'zaq1@WSX';
CREATE USER LecturerUser FOR LOGIN Lecturer;
CREATE ROLE LecturerRole;
ALTER ROLE LecturerRole ADD MEMBER LecturerUser;
GO

