using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Oazachaosu.Models.Repository {
  public class DatabaseConnector {
    private const string ConnectionString = "server=localhost;user id=admin;password=admin;persistsecurityinfo=True;database=wordki";

    public IEnumerable<DataRow> ExecuteForRows(MySqlCommand command) {
      IEnumerable<DataRow> rows;
      using (MySqlConnection conn = new MySqlConnection(ConnectionString)) {
        command.Connection = conn;
        command.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        MySqlDataAdapter adp = new MySqlDataAdapter(command);
        conn.Open();
        adp.Fill(dt);
        rows = dt.AsEnumerable().ToList();
      }
      return rows;
    }

  }
}