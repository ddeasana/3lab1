using database;
using System.Text.Json.Serialization;


namespace database
{  
        public interface IEntity
        {
            public string LastName { get; set; }
            public string[] Methods { get; }
            public string ToString();
        }
        public class Entity : IEntity, IPLayChess
        {
            string lastName;
            public string LastName { get => lastName; set => lastName = value; }
            [JsonConstructor]
            public Entity(string LastNameInput)
            {
                lastName = LastNameInput;
            }
            public virtual string[] Methods { get { return new string[] {"PlayChess"}; } }
        public string PlayChess()
        {
            return LastName + " plays chess";
        }
        public override string ToString() => lastName;
        }
        public class Student : Entity, IStudy
        {
            private string studentID;
            private int? gpa;
            private int? course;
            private string? country;
            private string? numberOfTheScoreBook;
            public int? Course { get => course; set => course = value; }
            public string StudentID { get => studentID; set => studentID = value; }
            public int? GPA { get => gpa; set => gpa = value; }
            public string? Country { get => country; set => country = value; }
            public string? NumberOfTheScoreBook { get => numberOfTheScoreBook; set => numberOfTheScoreBook = value; }
            public Student(string LastNameInput, string StudentIDInput) : base(LastNameInput)
            {
                studentID = StudentIDInput;
            }
            [JsonConstructor]
            public Student(int? Course, string StudentID, int? GPA, string? Country, string? NumberOfTheScoreBook, string LastName) : base(LastName)
            {
                (studentID, gpa, course, country, numberOfTheScoreBook) = (StudentID, GPA, Course, Country, NumberOfTheScoreBook);
            }
            public Student(string LastName, string StudentID, int? Course, int? GPA, string? Country, string? NumberOfTheScoreBook) :
                this(Course, StudentID, GPA, Country, NumberOfTheScoreBook, LastName)
            { }
            public string Study()
            {
                course = course == 6 ? 1 : course + 1;
                return LastName + " is now studing in " + course + " course";
            }
            
        public override string[] Methods { get { return base.Methods.Union(new string[] { "Study" }).ToArray(); } }
            public override string ToString() =>
                "Student - " + LastName +
                ", StudentID: " + studentID +
                ", Course: " + Course +
                ", GPA: " + GPA +
                ", Country: " + Country +
                ", NumberOfTheScoreBook: " + NumberOfTheScoreBook;
        }
        public class McWorker : Entity, ICook
        {
            public McWorker(string LastName) : base(LastName){}
            public string Cook()
            {
                return LastName + " prepared your order!";
            }
            public override string[] Methods { get { return base.Methods.Union(new string[] { "Cook" }).ToArray(); } }
            public override string ToString() => "McWorker - " + LastName;
        }
        public class Manager : Entity, IManage
        {
        public Manager(string LastName) : base(LastName) { }
            public string Manage()
            {
                return LastName + " manages something!";
            }
            public override string[] Methods { get { return base.Methods.Union(new string[] { "Manage" }).ToArray(); } }
            public override string ToString() => "Manager - " + LastName;
        }
    }
