--Administrator ma:
--wszystko na wszystko
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Absence TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Course TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Email TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Faculty TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.FieldOfStudy TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.FieldOfStudy_Course TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Grade TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.[Group] TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.GroupType TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Group_Lecturer TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Lecturer TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Regularity TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Semester TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Student TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Student_FieldOfStudy TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Student_Group TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.[User] TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.UserLoginInformation TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.WorkPhone TO AdministrationRole;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Administrator TO AdministrationRole;

--SELECT na widokach:
GRANT SELECT ON dbo.GroupInfo TO AdministrationRole;
GRANT SELECT ON UserContact TO AdministrationRole;
GRANT SELECT ON CourseInfo TO AdministrationRole;

-- Execute na wszystkich procedurach
GRANT EXECUTE ON dbo.RegisterStudent TO AdministrationRole;
GRANT EXECUTE ON dbo.GetStudentWeeklyPlan TO AdministrationRole;
GRANT EXECUTE ON dbo.GetLecturerWeeklyPlan TO AdministrationRole;
GRANT EXECUTE ON dbo.GetStudentGradesInGroup TO AdministrationRole;
GRANT EXECUTE ON dbo.GetStudentFinalGradesInSemester TO AdministrationRole;
GRANT EXECUTE ON dbo.GetStudentWeightedAverageGradeInSemester TO AdministrationRole;
GRANT EXECUTE ON dbo.GetStudentAbsencesInGroup TO AdministrationRole;
GRANT EXECUTE ON dbo.GetStudentGradesInGroup TO AdministrationRole;
GRANT EXECUTE ON dbo.GetGroupGrades TO AdministrationRole;
GRANT EXECUTE ON dbo.GetCourseFinalGrades TO AdministrationRole;
GRANT EXECUTE ON dbo.GetStudentsAbsencesInGroupCount TO AdministrationRole;
GRANT EXECUTE ON dbo.RegisterLecturer TO AdministrationRole;

--Student ma:
--select na wszystko
GRANT SELECT ON dbo.Absence TO StudentRole;
GRANT SELECT ON dbo.Course TO StudentRole;
GRANT SELECT ON dbo.Email TO StudentRole;
GRANT SELECT ON dbo.Faculty TO StudentRole;
GRANT SELECT ON dbo.FieldOfStudy TO StudentRole;
GRANT SELECT ON dbo.FieldOfStudy_Course TO StudentRole;
GRANT SELECT ON dbo.[Group] TO StudentRole;
GRANT SELECT ON dbo.GroupType TO StudentRole;
GRANT SELECT ON dbo.Group_Lecturer TO StudentRole;
GRANT SELECT ON dbo.Lecturer TO StudentRole;
GRANT SELECT ON dbo.Regularity TO StudentRole;
GRANT SELECT ON dbo.Semester TO StudentRole;
GRANT SELECT ON dbo.Student TO StudentRole;
GRANT SELECT ON dbo.Student_FieldOfStudy TO StudentRole;
GRANT SELECT ON dbo.Student_Group TO StudentRole;
GRANT SELECT ON dbo.[User] TO StudentRole;
GRANT SELECT ON dbo.WorkPhone TO StudentRole;
GRANT SELECT ON dbo.Administrator TO StudentRole;

--update na oceny i haslo
GRANT SELECT, UPDATE ON dbo.Grade TO StudentRole;
GRANT SELECT, UPDATE ON dbo.UserLoginInformation TO StudentRole;

--SELECT na widokach:
GRANT SELECT ON dbo.GroupInfo TO StudentRole;
GRANT SELECT ON UserContact TO StudentRole;
GRANT SELECT ON CourseInfo TO StudentRole;

--Execute na 
GRANT EXECUTE ON dbo.RegisterStudent TO StudentRole;
GRANT EXECUTE ON dbo.GetStudentWeeklyPlan TO StudentRole;
GRANT EXECUTE ON dbo.GetStudentGradesInGroup TO StudentRole;
GRANT EXECUTE ON dbo.GetStudentFinalGradesInSemester TO StudentRole;
GRANT EXECUTE ON dbo.GetStudentWeightedAverageGradeInSemester TO StudentRole;
GRANT EXECUTE ON dbo.GetStudentAbsencesInGroup TO StudentRole;

--prowadzacy ma
--select na wszystko:
GRANT SELECT ON dbo.Course TO LecturerRole;
GRANT SELECT ON dbo.Email TO LecturerRole;
GRANT SELECT ON dbo.Faculty TO LecturerRole;
GRANT SELECT ON dbo.FieldOfStudy TO LecturerRole;
GRANT SELECT ON dbo.FieldOfStudy_Course TO LecturerRole;
GRANT SELECT ON dbo.[Group] TO LecturerRole;
GRANT SELECT ON dbo.GroupType TO LecturerRole;
GRANT SELECT ON dbo.Group_Lecturer TO LecturerRole;
GRANT SELECT ON dbo.Lecturer TO LecturerRole;
GRANT SELECT ON dbo.Regularity TO LecturerRole;
GRANT SELECT ON dbo.Semester TO LecturerRole;
GRANT SELECT ON dbo.Student TO LecturerRole;
GRANT SELECT ON dbo.Student_FieldOfStudy TO LecturerRole;
GRANT SELECT ON dbo.Student_Group TO LecturerRole;
GRANT SELECT ON dbo.[User] TO LecturerRole;
GRANT SELECT ON dbo.WorkPhone TO LecturerRole;
GRANT SELECT ON dbo.Administrator TO LecturerRole;

--insert na oceny i nieobecnosci
GRANT SELECT, INSERT ON dbo.Absence TO LecturerRole;
GRANT SELECT, INSERT ON dbo.Grade TO LecturerRole;

--update na haslo
GRANT SELECT, UPDATE ON dbo.UserLoginInformation TO LecturerRole;

--SELECT na widokach:
GRANT SELECT ON dbo.GroupInfo TO LecturerRole;
GRANT SELECT ON UserContact TO LecturerRole;
GRANT SELECT ON CourseInfo TO LecturerRole;

--execute na:
GRANT EXECUTE ON dbo.RegisterLecturer TO LecturerRole;
GRANT EXECUTE ON dbo.GetLecturerWeeklyPlan TO LecturerRole;
GRANT EXECUTE ON dbo.GetStudentGradesInGroup TO LecturerRole;
GRANT EXECUTE ON dbo.GetGroupGrades TO LecturerRole;
GRANT EXECUTE ON dbo.GetCourseFinalGrades TO LecturerRole;
GRANT EXECUTE ON dbo.GetStudentAbsencesInGroup TO LecturerRole;
GRANT EXECUTE ON dbo.GetStudentsAbsencesInGroupCount TO LecturerRole;