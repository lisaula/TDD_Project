using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.Out.WriteLine("Hola mundo");
            /*using (var con = new SqlConnection("Data Source=(local)\\SQLEXPRESS; Integrated Security=True;"))
            {
                con.Open();

                DataTable databases = con.GetSchema("Databases");
                foreach (DataRow database in databases.Rows)
                {
                    String databaseName = database.Field<String>("database_name");
                    short dbID = database.Field<short>("dbid");
                    DateTime creationDate = database.Field<DateTime>("create_date");
                    System.Console.Out.WriteLine(databaseName + " id "+ dbID + " dateTime "+creationDate.ToLongDateString());
                }
            }*/
            using (var con = new SqlConnection("Data Source=(local)\\SQLEXPRESS;Integrated Security=false;Initial Catalog=Prueba2;User ID=prueba2; Password=luis;"))
            {
                con.Open();
    using (SqlCommand command = con.CreateCommand())
                {
                    string database_name = "Alfredito";
                    command.CommandText = @"use " + database_name + @"
select* from sys.tables";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                      while (reader.Read())
                        {
                            var schemaName = reader.GetString(0);
                            int tableName=0;
                            if (!reader.IsDBNull(1))
                                 tableName = reader.GetInt32(1);
                            System.Console.Out.WriteLine(schemaName+" "+tableName);
                            // your code goes here...
                        }
                    }
                }
            }
            
            System.Console.ReadKey();
        }
    }
}
