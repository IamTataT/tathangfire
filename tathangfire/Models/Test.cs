using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;

namespace tathangfire.Models
{
    public class Test
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
    }
    public class GetTest
    {
            /*public IEnumerable<Test> GetTests()
            {
                using (var conn = new SqlConnection(@"Data Source=test-sql-server.ct55wsfs7cmp.ap-northeast-1.rds.amazonaws.com,1433;Initial Catalog=samuraigamez-dev;Integrated Security=False;Persist Security Info=True;User ID=admin;Password=password"))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "SELECT DisplayName, Username FROM User";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Test
                            {
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                            };
                        };
                    }
                }

            }*/
    }

}