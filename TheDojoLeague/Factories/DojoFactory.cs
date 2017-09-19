using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using TheDojoLeague.Models;
using TheDojoLeague.Factory;

namespace TheDojoLeague.Factory{
    public class DojoFactory : IFactory<Dojo>{
        private string connectionString;
        public DojoFactory(){
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=dojoNinja;SslMode=None";
        }
        internal IDbConnection Connection{
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public IEnumerable<Dojo> getDojos(){
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                return dbConnection.Query<Dojo>("SELECT * FROM dojos");
            }
        }

        public void Add(Dojo dojo){
            using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO dojos (name, location, description) VALUES (@Name, @Location, @Information)";
                dbConnection.Open();
                dbConnection.Execute(query, dojo);
            }
        }
    }
}
