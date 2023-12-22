using DataProducer;
using DataProducer.Generators;

var cleaner = new Cleaner();

//Kolejność ma znaczenie!!!
var generator = new Generator([
                new FacultyGenerator(),
                new FieldOfStudyGenerator(),
                new AdministratorGenerator(),
                new LecturerGenerator(),
                new StudentGenerator(),
                new CourseGenerator(),
                new GroupGenerator()
                ]); ;

//var generator = new Generator([new CSVGenerator()]);

cleaner.Clean();
generator.Generate();