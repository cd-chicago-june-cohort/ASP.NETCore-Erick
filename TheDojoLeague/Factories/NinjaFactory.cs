using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using TheDojoLeague.Models;
using TheDojoLeague.Factory;

namespace TheDojoLeague.Factory{
    public class NinjaFactory : IFactory<Ninja>{
        private string connectionString;
        public NinjaFactory(){
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=dojoNinja;SslMode=None";
        }
        internal IDbConnection Connection{
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public IEnumerable<Dojo> findAll(){
            using (IDbConnection dbConnection = Connection) {
                return dbConnection.Query<Dojo>("SELECT * FROM dojos");
            }
        }

        public void Add(Ninja ninja){
            using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO ninjas (name, level, description, dojo_id) VALUES (@Name, @Level, @Length, @Description, @Dojo_Id)";
                dbConnection.Open();
                dbConnection.Execute(query, ninja);
            }
        }
    }
}
