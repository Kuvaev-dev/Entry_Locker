using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Entry_Locker.Models.Repositories
{
    public class User_Repository
    {
        string connectionString = "Data Source=SQL5080.site4now.net;Initial Catalog=db_a79439_regdb;User Id=db_a79439_regdb_admin;Password=qwerty009";
        public void Autorezation(User new_user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var sqlQuery = "EXEC [AddUser] @Date, @Time, @IP";
                        var values = new
                        {
                            Date = new_user.Date,
                            Time = new_user.Time,
                            IP = new_user.IP
                        };
                        db.Query(sqlQuery, values, transaction);
                        transaction.Commit();
                    }
                    catch (Exception) 
                    { 
                        transaction.Rollback(); 
                    }
                }
            }

        }
    }
}
