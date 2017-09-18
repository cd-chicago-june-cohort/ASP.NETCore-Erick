using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using LostInTheWoods.Models;
using LostInTheWoods.Factory;

namespace LostInTheWoods.Factory{
    public class TrailFactory : IFactory<Trail>{
        private string connectionString;
        public TrailFactory(){
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=lost_in_the_woods;SslMode=None";
        }
        internal IDbConnection Connection{
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(Trail trail){
            using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO trails (name, description, length, elevation_change, longitude, latitude) VALUES (@Name, @Description, @Length, @Elevation_Change, @Longitude, @Latitude)";
                dbConnection.Open();
                dbConnection.Execute(query, trail);
            }
        }

        public IEnumerable<Trail> getTrails(){
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails");
            }
        }

        public Trail FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }
    }
}