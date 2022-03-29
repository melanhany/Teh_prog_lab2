using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛАБ2
{
    class Program
    {
        class Person : IDateAndCopy
        {
            protected string Name; //закрытое поле типа string, в котором хранится имя
            protected string LastName; //закрытое поле типа string, в котором хранится фамилия
            protected System.DateTime BDate;//закрытое поле типа System.DateTime для даты рождения
            //конструктор c тремя параметрами типа string, string, DateTime для инициализации всех полей класса
            public Person(string studentName, string studentLastName, DateTime studentBDate)
            {
                Name = studentName;
                LastName = studentLastName;
                BDate = studentBDate;
            }

            //конструктор без параметров, инициализирующий все поля класса некоторыми значениями по умолчанию.
            public Person() : this("Aimukhamedov", "Yermakhan", new DateTime(1999, 3, 22))
            { }

            // Свойства c методами get и set: 

            public string StdName //свойство типа string для доступа к полю с именем;
            {
                get
                {
                    return Name;
                }
                set
                {
                    Name = value;
                }

            }

            public string StdLastName //свойство типа string для доступа к полю с фамилией
            {
                get
                {
                    return LastName;
                }
                set
                {
                    LastName = value;
                }

            }

            public DateTime StdBDate //x свойство типа DateTime для доступа к полю с датой рождения
            {
                get
                {
                    return BDate;
                }
                set
                {
                    BDate = value;
                }

            }

            //Свойство типа int c методами get и set для получения информации(get) и изменения (set) года рождения в закрытом поле типа DateTime, в котором хранится дата рождения
            public int intStdBdate
            {
                get
                {
                    return Convert.ToInt32(BDate);
                }
                set
                {
                    BDate = Convert.ToDateTime(value);
                }
            }


            //Перегруженную(override) версию виртуального метода string ToString() для формирования строки со значениями всех полей класса       
            public override string ToString()
            {
                return "\n" + "Name " + Name + "\n" + "LastName " + LastName + "\n" + "Date of birth: " + BDate + "\n";
            }


            //Виртуальный метод string ToShortString(), который возвращает строку, содержащую только имя и фамилию.
            public string ToShortString()
            {
                return "\n" + "Имя: " + Name + "\n" + "Фамилия: " + LastName + "\n";
            }

            /////////переопределить метод virtual bool Equals (object obj) и определить операции 
            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }
                Person objPers = obj as Person;
                /*if (!(obj is Person))
                {
                    return false;
                }одно и тоже*/
                if (obj as Person == null)
                {
                    return false;
                }
                return objPers.Name == StdName && objPers.LastName == StdLastName && objPers.BDate == StdBDate;
            }
            ////////переопределить виртуальный метод int GetHashCode();
            public override int GetHashCode()
            {
                int hashcode = 0;
                char[] NameChar = Name.ToCharArray();

                foreach (char ch in NameChar)
                {
                    hashcode += Convert.ToInt32(ch);
                }
                char[] LastNameChar = LastName.ToCharArray();
                foreach (char ch in LastNameChar)
                {
                    hashcode += Convert.ToInt32(ch);
                }
                hashcode += BDate.Year * BDate.Month;
                return hashcode;
            }

            /////////
            public static bool operator ==(Person lpers, Person rpers)
            {
                if (ReferenceEquals(lpers, rpers))
                {
                    return true;
                }
                if ((object)lpers == null || (object)rpers == null)
                {
                    return false;
                }
                return lpers.StdName == rpers.StdName && lpers.StdBDate == rpers.StdBDate && lpers.StdLastName == rpers.StdLastName;
            }
            public static bool operator !=(Person lpers, Person rpers)
            {
                return !(lpers == rpers);
            }
            public virtual object DeepCopy()
            {
                Person persCopy = new Person(this.StdName, this.StdLastName, this.StdBDate);
                return persCopy;
            }

            DateTime IDateAndCopy.Date
            {
                get
                {
                    return BDate;
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

        }



        class Test
        {
            public string SubjectName { get; set; }
            public bool Status { get; set; }

            public Test(string StdSubjectName, bool StdStatus)
            {
                SubjectName = StdSubjectName;
                Status = StdStatus;
            }
            public Test() : this("Название предмета", false)
            { }

            public override string ToString()
            {
                return "Название предмета: " + SubjectName + " " + "Статус сдачи: " + Status;
            }
        }

        // Определить класс Exam, который имеет три открытых автореализуемых свойства, доступных для чтения и записи
        class Exam : IDateAndCopy //Paper
        {
            public string Discipline { get; set; }
            public int Rate { get; set; }
            public DateTime DateOfExam { get; set; }
            //конструктор с параметрами типа string, int и DateTime для инициализации всех свойств класса;
            public Exam(string discipline, int rate, DateTime dateOfExam)
            {
                this.Discipline = discipline;
                this.Rate = rate;
                this.DateOfExam = dateOfExam;
            }
            //конструктор без параметров, инициализирующий все свойства класса некоторыми значениями по умолчанию;
            public Exam()
            {
                this.Discipline = "";
                this.Rate = 0;
                this.DateOfExam = DateTime.Now;
            }
            // перегруженную(override) версию виртуального метода string ToString() для формирования строки со значениями всех свойств класса.
            public override string ToString()
            {
                return "Название предмета: " + Discipline + " " + "Оценка: " + Rate + " " + "Дата экз.: " + DateOfExam;
            }

            public object DeepCopy()
            {
                Exam CopyExam = new Exam(this.Discipline, this.Rate, this.DateOfExam);
                return CopyExam;
            }///копирование

            DateTime IDateAndCopy.Date
            {
                get
                {
                    return DateOfExam;
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
        }
        //Определить класс Student
        class Student : Person, IDateAndCopy //ReasearchTeam
        {
            private Person person;
            //private Education education;
            public Education education;
            private int group;
            ////systemcollections 
            private System.Collections.ArrayList TestList = new ArrayList();
            private System.Collections.ArrayList ExamList = new ArrayList();
            public System.Collections.ArrayList ListOfTests { get { return TestList; } set { TestList = value; } }
            public System.Collections.ArrayList ListOfExams { get { return ExamList; } set { ExamList = value; } }
            //private readonly List<Exam> exams = new List<Exam>(); old version
            //конструктор c параметрами типа Person, Education, int для инициализации соответствующих полей класса;
            public Student(Person human, Education edu, int grp)
            {
                this.person = human;
                this.education = edu;
                this.group = grp;
            }
            //конструктор без параметров, инициализирующий поля класса значениями по умолчанию.
            public Student() : this(new Person(), Education.Bachelor, 204)
            { }

            //определить свойства c методами get и set
            public Person Person
            {
                get { return person; }
                set { person = value; }
            }

            public Education Education
            {
                get { return education; }
                set { education = value; }
            }

            public int Group
            {
                get { return group; }
                set
                {
                    if (value <= 100 || value > 599)
                    {//дополнить из лекции
                        throw new ArgumentOutOfRangeException("Group number must be between 100 and 600 ");
                    }
                    else
                    {
                        group = value;
                    }
                }
            }

            //public exam[] Exams  old version
            //{
            //    get { return exams.toarray(); }

            //}
            // свойство типа double (только с методом get), в котором вычисляется средний балл как среднее значение оценок в списке сданных экзаменов;
            public double AvgRate
            {
                //get { return exams.Count > 0 ? exams.Average(e=>e.Rate) : 0; }
                get
                {
                    double sum = 0;
                    foreach (Exam qwee in ListOfExams)
                    {
                        sum += qwee.Rate;
                    }
                    return ExamList.Count == 0 ? -1 : sum / ExamList.Count;
                }
            }
            //индексатор булевского типа (только с методом get) с одним параметром типа Education; значение индексатора равно true, если значение поля с
            // формой обучения студента совпадает со значением индекса, и false в противном случае
            public bool this[Education index]
            {
                get
                {
                    return this.Education == index;
                }
            }
            //метод void AddExams ( params Exam [] ) для добавления элементов в список экзаменов
            public void AddExams(params Exam[] exams)
            {
                this.ExamList.AddRange(exams);
            }

            public void AddTests(params Test[] tests)
            {
                this.TestList.AddRange(tests);
            }

            //перегруженную версию виртуального метода string ToString() для формирования строки со значениями всех полей класса, включая список экзаменов;
            public override string ToString()
            {
                return string.Format("Person: {0}\n Education: {1}\n group: {2}\n exams: {3}\n ", person, education, group, string.Join(", ", ExamList));
            }
            //виртуальный метод string ToShortString(), который формирует строку со значениями всех полей класса без списка экзаменов, но со значением среднего балла
            public virtual string ToShortString()
            {
                return string.Format("Person: {0}\n Education: {1}\n group: {2}\n AvgRate: {3:0.00}\n", person, education, group, AvgRate);
            }

            public override object DeepCopy()
            {
                Student CopyStudent = new Student(this.person, this.education, this.group);
                CopyStudent.ListOfTests = ListOfTests;
                CopyStudent.ListOfExams = ListOfExams;
                return CopyStudent;

            }

            public Person getPersonType
            {
                get
                {
                    return new Person(StdName, StdLastName, StdBDate);
                }
                set
                {
                    this.StdName = value.StdName;
                    this.StdLastName = value.StdLastName;
                    this.StdBDate = value.StdBDate;
                }
            }

            public IEnumerable<Test> ListOfTestsPerebor()
            {
                for (int i = 0; i < ExamList.Count; i++)
                {    
                    yield return (Test)TestList[i];
                }
            }

            public IEnumerable<Exam> ListOfExamsPerebor()
            {
                for (int i = 0; i < ExamList.Count; i++)
                {
                    yield return (Exam)ExamList[i];
                }
            }

            public IEnumerable<Exam> ListOfExamsPereborUslovie()
            {
                for (int i = 0; i < ExamList.Count; i++)
                {
                    if (((Exam)ExamList[i]).Rate > 3)
                    {
                        yield return (Exam)ExamList[i];
                    }
                }
            }

            /*DateTime IDateAndCopy.Date
            {
                get
                {
                    return
                }
                set
                {

                }
            }*/
        }


        interface IDateAndCopy
        {
            object DeepCopy();
            DateTime Date { get; set; }
        }

        //Определить тип Education - перечисление(enum) со значениями Specialist, Вachelor, SecondEducation. 
        enum Education { Specialist, Bachelor, SecondEducation }

        private static void Main(string[] args)
        {
            //1.	Создать два объекта типа Person с совпадающими данными 
            Person Person1 = new Person("Bruce", "Wayne", new DateTime(1990, 4, 5));
            Person Person2 = new Person("Bruce", "Wayne", new DateTime(1990, 4, 5));
            Console.WriteLine(Person1.Equals(Person2));
            Console.WriteLine(Person1 == Person2);
            Console.WriteLine(string.Format("Person1 hashcode: {0}, Person2 hashcode: {1} ", Person1.GetHashCode(), Person2.GetHashCode()));

            //2.    Создать объект типа Student, добавить элементы в список экзаменов и зачетов, вывести данные объекта Student
            Student std = new Student(); //Создать один объект типа Student,

            std = new Student(new Person("Ivan", "Ivanov", new DateTime(1990, 4, 5)), Education.Bachelor, 3);

            // C помощью метода AddExams(params Exam*+ ) добавить элементы в список экзаменов и вывести данные объекта Student, используя метод ToString().
            std.AddExams(new Exam("Math", 4, new DateTime(2019, 1, 23)), new Exam("C#", 5, new DateTime(2019, 1, 25)));
            std.AddTests(new Test("Math", true), new Test("C#", false));

            Console.WriteLine(std.ToString());

            foreach (Exam ex in std.ListOfExamsPerebor())
            {
                Console.WriteLine(ex);
            }

            foreach (Test ts in std.ListOfTestsPerebor())
            {
                Console.WriteLine(ts);
            }

            //3.	Вывести значение свойства типа Person для объекта типа Student.
            Console.WriteLine(std.getPersonType.ToString());

            //4.	С помощью метода DeepCopy() создать полную копию объекта Student
            Student stdCopy = (Student)std.DeepCopy();
            std.education = Education.Specialist;
            std.Group = 150;

            Console.WriteLine(stdCopy);
            Console.WriteLine(std);

            /*
            Exam[] odnomer = new Exam[10];

            Exam[,] dvumer = new Exam[2, 5];

            Exam[][] stup = new Exam[4][];

            stup[0] = new Exam[1];

            stup[1] = new Exam[2];

            stup[2] = new Exam[3];

            stup[3] = new Exam[4];

            long milliseconds = DateTime.Now.Ticks;

            for (int p = 0; p < 10000; p++)

                for (int i = 0; i < 10; i++)

                    odnomer[i] = new Exam();

            for (int i = 0; i < 10; i++)

                Console.WriteLine(odnomer[i] + " ");

            milliseconds = DateTime.Now.Ticks - milliseconds;

            Console.WriteLine("time wasted: " + milliseconds + "\n");

            milliseconds = DateTime.Now.Ticks;

            for (int p = 0; p < 10000; p++)

                for (int i = 0; i < 2; i++)

                    for (int j = 0; j < 5; j++)

                        dvumer[i, j] = new Exam();

            Console.WriteLine("\n");

            for (int i = 0; i < 2; i++)

                for (int j = 0; j < 5; j++)

                    Console.WriteLine(dvumer[i, j] + " ");

            milliseconds = DateTime.Now.Ticks - milliseconds;

            Console.WriteLine("time wasted: " + milliseconds + "\n");

            milliseconds = DateTime.Now.Ticks;

            for (int p = 0; p < 10000; p++)

                for (int i = 0; i < 4; i++)

                    for (int j = 0; j < i + 1; j++)

                        stup[i][j] = new Exam();

            Console.WriteLine("\n");

            for (int i = 0; i < 4; i++)

                for (int j = 0; j < i + 1; j++)

                    Console.WriteLine(stup[i][j] + " ");

            milliseconds = DateTime.Now.Ticks - milliseconds;

            Console.WriteLine("time wasted: " + milliseconds + "\n");

            milliseconds = DateTime.Now.Ticks;
            */
            Console.ReadKey();

        }
    }
}
