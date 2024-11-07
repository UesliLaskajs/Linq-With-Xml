using System.Xml.Linq;

namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UniversityManagment universityManagment = new UniversityManagment();
            universityManagment.LinqSelector();
            universityManagment.OrderByAge();
            universityManagment.StudentsFromCit();
            universityManagment.InputParam();


            string studentsXML =  //Xml is a markup language that provides rules to define any data
   @"<Students>
        <Student>
            <Name>Toni</Name>
            <Age>21</Age>
            <University>Yale</University>
            <Semester>6</Semester>
        </Student>
        <Student>
            <Name>Carla</Name>
            <Age>17</Age>
            <University>Yale</University>
            <Semester>1</Semester>
        </Student>
        <Student>
            <Name>Leyla</Name>
            <Age>19</Age>
            <University>Beijing Tech</University>
            <Semester>3</Semester>
        </Student>
        <Student>
            <Name>Frank</Name>
            <Age>25</Age>
            <University>Beijing Tech</University>
            <Semester>10</Semester>
        </Student>
    </Students>";

            XDocument studentsXdoc = XDocument.Parse(studentsXML);// Object to Get The Xml Data Inside 

            var studentNames = from students in studentsXdoc.Descendants("Student")  //First we Extract the data from the xml with Linq 
                               select new
                               {
                                   Name = students.Element("Name").Value,
                                   Age = students.Element("Age").Value,
                                   University = students.Element("University").Value,
                                   Semester=students.Element("Semester").Value

                               };


            foreach (var student in studentNames) //Used Linq
            {
                Console.WriteLine($"{student.Name},{student.Age},{student.University},{student.Semester}");
            }


            var studentOrder = from studs in studentNames orderby studs.Age select studs;

            foreach (var student in studentNames)
            {
                Console.WriteLine($"Ordered {student.Name},{student.Age},{student.University},{student.Semester}");
            }


            Console.ReadKey();



        }

    }


    class University
    {
        public int Id { get; set; }//Properties to Get Values and Create the University Class
        public string Name { get; set; }   
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        //Foreign Key

        public int UniversityId { get; set; }

        public void Print()
        {
            Console.WriteLine($"Student: {Name}, Age: {Age}");
        }
    }

    class UniversityManagment
    {
        public List<University> university;//Created a List with The University Class Properties

        public List<Student> students;//Created a List with The University Class Properties

        public UniversityManagment()
        {
            university = new List<University>();//Initialised an empty List With Charachteristics of University Class Properties
            students = new List<Student>();

            university.Add(new University { Id = 1, Name = "Canadian Institute Of Technology" });//Added Valus Into THe List
            university.Add(new University { Id = 2, Name = "Fakulteti I Shkencave Te Natyres" });


            students.Add(new Student { Id = 1, Name = "Uesli", Age = 20, Gender = "Male", UniversityId = 1 });
            students.Add(new Student { Id = 2, Name = "Aldo", Age = 20, Gender = "TransGender", UniversityId = 2 });
            students.Add(new Student { Id = 3, Name = "Enso", Age = 20, Gender = "Male", UniversityId = 1 });
            students.Add(new Student { Id = 4, Name = "Bruno", Age = 20, Gender = "Male", UniversityId = 2 });
            students.Add(new Student { Id = 5, Name = "Natalie", Age = 20, Gender = "Female", UniversityId = 2 });
        }

        public void LinqSelector()
        {
            IEnumerable<Student> maleStudents = from student in students where student.Gender == "Male" select student;//Ienumerable Type to Select Data From the List Query
            foreach (Student student in maleStudents)
            {
                student.Print();
            }

        }

        public void OrderByAge()
        {
            IEnumerable<Student> studentOrder = from student in students orderby student.Age select student;

            foreach (Student student in studentOrder)
            {
                student.Print();
            }
        }

        public void StudentsFromCit()
        {
            IEnumerable<Student> citStudents = from student in students
                                               join university in this.university on student.UniversityId equals university.Id
                                               where university.Name == "Canadian Institute Of Technology"
                                               select student;

            foreach (Student student in citStudents)
            {
                Console.WriteLine(student.Name);
            }
        }

        public void InputParam()
        {
            Console.WriteLine("Select An Number for University");
            int input = int.Parse(Console.ReadLine());

            IEnumerable<Student> selectedstudents = from student in students join university in this.university on student.UniversityId equals university.Id
                                                    where university.Id == input
                                                    select student;
            foreach (Student student in selectedstudents)
            {
                student.Print();

            }


        }

        public void orderByNameAndUni()
        {
            var collection = from student in students
                             join university in this.university on student.UniversityId equals university.Id
                             orderby student.Name
                             select new { StudentName=student.Name, UniversityName= university.Name };



            foreach(var col in collection)
            {
                Console.WriteLine($"Student Name {col.StudentName} and UniversityName{col.UniversityName}");
            }

    }
    }

}
