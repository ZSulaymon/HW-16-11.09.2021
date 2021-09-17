using System;
using System.Data.SqlClient;


namespace HW_16_11._09._2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var conString = "" +
                "Data source=localhost; " +
                "Initial catalog=AcademySummer; " +
                "user id=sa; " +
                "password=123456";

            var conn = new SqlConnection(conString);
            conn.Open();

            Console.Write("1. Create person(insert)\n2. Choose all(select all)\n3. Choose one(select by id)\n4. Update info(Update)\n5. Delete info(Delete)\nChoice:");
            int.TryParse(Console.ReadLine(), out var choice);
            switch (choice)
            {
                case 1:
                    {
                        CreatePerson(conString);
                    }
                    break;
                case 2:
                    {
                        ChooseAll(conString);
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine("inter ID person");
                        int.TryParse(Console.ReadLine(), out var ID);
                        ChooseById(conString, ID);
                    }
                    break;
                case 4:
                    {
                        ChooseAll(conString);
                        Console.WriteLine("inter ID for update");
                        int.TryParse(Console.ReadLine(), out var ID);
                        UpdateInfo(conString, ID);
                    }
                    break;
                case 5:
                    {
                        ChooseAll(conString);
                        Console.WriteLine("inter id for delete");
                        int.TryParse(Console.ReadLine(), out var ID);
                        DeleteInfo(conString, ID);
                    }
                    break;
                default:
                    Console.WriteLine("Wrong choose.");
                    break;
            }
        }
        private static void CreatePerson(string conString)
        {
            var person = new Person();
            Console.WriteLine("Inter your FirstName");
            person.FirstName = Console.ReadLine();
            Console.WriteLine("Inter your LastName ");
            person.LastName = Console.ReadLine();
            Console.WriteLine("Inter your MiddleName");
            person.MiddleName = Console.ReadLine();
            Console.WriteLine("Inter your birthday");
            person.BirthDate = Convert.ToDateTime(Console.ReadLine());

            var connection = new SqlConnection(conString);
            var query = "INSERT INTO [dbo].[Person] ([LastName],[FirstName],[MiddleName],[BirthDate]) VALUES (@firstName" +
                ", @lastName,@middleName,@birthDay)";

            var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@middleName", person.MiddleName);
            command.Parameters.AddWithValue("@birthDay", person.BirthDate);

            connection.Open();
            var result = command.ExecuteNonQuery();
            if (result > 0) { Console.WriteLine("Persom created succesfull"); }
            connection.Close();


        }
        private static void ChooseAll(string conString)
        {
            Person[] people = new Person[0];
            var connection = new SqlConnection(conString);
            var query = "SELECT * FROM[dbo].[Person]";

            var command = connection.CreateCommand();
            command.CommandText = query;
            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var person = new Person { };
                person.Id = int.Parse(reader["Id"].ToString());
                person.FirstName = reader["FirstName"].ToString();
                person.LastName = reader["Lastname"].ToString();
                person.MiddleName = reader["MiddleName"].ToString();
                person.BirthDate = DateTime.Parse(reader["BirthDate"].ToString());
                AddPeron(ref people, person);
            }
            connection.Close();
            foreach (var person in people)
            {
                Console.WriteLine($"ID:{person.Id},firstName:{person.FirstName},lastname:{person.LastName},middleName:{person.MiddleName},birthday:{person.BirthDate}");
            }
        }
        private static void AddPeron(ref Person[] people, Person person)
        {
            if (people == null)
            {
                return;
            }

            Array.Resize(ref people, people.Length + 1);

            people[people.Length - 1] = person;
        }


        private static void ChooseById(string conString, int id)
        {
            //Person[] people = new Person[0];             
            var connection = new SqlConnection(conString);
            connection.Open();
            var query = "SELECT [Id],[LastName],[FirstName],[MiddleName],[BirthDate] FROM[dbo].[Person] where Id = @ID";
            var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@ID", id);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var person = new Person { };
                person.Id = int.Parse(reader["Id"].ToString());
                person.FirstName = reader["FirstName"].ToString();
                person.LastName = reader["Lastname"].ToString();
                person.MiddleName = reader["MiddleName"].ToString();
                person.BirthDate = DateTime.Parse(reader["BirthDate"].ToString());
                //AddPeron(ref people, person);
                Console.WriteLine($"ID:{person.Id},firstName:{person.FirstName},lastname:{person.LastName},middleName:{person.MiddleName},birthday:{person.BirthDate}");

            }
            connection.Close();
            //foreach (var person in people)
            //{
              //  Console.WriteLine($"ID:{person.Id},firstName:{person.FirstName},lastname:{person.LastName},middleName:{person.MiddleName},birthday:{person.BirthDate}");
            //}
        }
        private static void UpdateInfo(string conString,int id)
        {
            var person = new Person();
            Console.WriteLine("Inter your FirstName");
            person.FirstName = Console.ReadLine();
            Console.WriteLine("Inter your LastName ");
            person.LastName = Console.ReadLine();
            Console.WriteLine("Inter your MiddleName");
            person.MiddleName = Console.ReadLine();
            Console.WriteLine("Inter your birthday");
            person.BirthDate = Convert.ToDateTime(Console.ReadLine());
            
            var connection = new SqlConnection(conString);
            connection.Open();
            var query = "update [dbo].[Person] set [FirstName] = @firstName,[LastName] =@lastName,[MiddleName]=@middleName, [BirthDate] =@birthDay   where Id = @ID";
            var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@middleName", person.MiddleName);
            command.Parameters.AddWithValue("@birthDay", person.BirthDate);
            command.Parameters.AddWithValue("@ID", id);


            //connection.Open();
            var result = command.ExecuteNonQuery();
            if (result > 0) { Console.WriteLine("Persom updated succesfull"); }
            connection.Close();

            //var reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    var person = new Person { };
            //    person.Id = int.Parse(reader["Id"].ToString());
            //    person.FirstName = reader["FirstName"].ToString();
            //    person.LastName = reader["Lastname"].ToString();
            //    person.MiddleName = reader["MiddleName"].ToString();
            //    person.BirthDate = DateTime.Parse(reader["BirthDate"].ToString());
            //    //AddPeron(ref people, person);
            //    // Console.WriteLine($"ID:{person.Id},firstName:{person.FirstName},lastname:{person.LastName},middleName:{person.MiddleName},birthday:{person.BirthDate}");
            //}
            Console.WriteLine("deleted");

            connection.Close();

        }
        private static void DeleteInfo(string conString, int id)
        {
            //Person[] people = new Person[0];
            var connection = new SqlConnection(conString);
            connection.Open();
            var query = "delete FROM[dbo].[Person] where Id = @ID";
            var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@ID", id);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var person = new Person { };
                person.Id = int.Parse(reader["Id"].ToString());
                person.FirstName = reader["FirstName"].ToString();
                person.LastName = reader["Lastname"].ToString();
                person.MiddleName = reader["MiddleName"].ToString();
                person.BirthDate = DateTime.Parse(reader["BirthDate"].ToString());
                //AddPeron(ref people, person);
                // Console.WriteLine($"ID:{person.Id},firstName:{person.FirstName},lastname:{person.LastName},middleName:{person.MiddleName},birthday:{person.BirthDate}");
            }
            Console.WriteLine("deleted");

            connection.Close();
            //foreach (var person in people)
            //{
            //  Console.WriteLine($"ID:{person.Id},firstName:{person.FirstName},lastname:{person.LastName},middleName:{person.MiddleName},birthday:{person.BirthDate}");
            //}
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
