using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using Repository.Models;

namespace Oazachaosu.Models.Repository {
  public class GroupRepository {

    private DatabaseConnector Connector { get; set; }
    private LocalDbContext DbContext = new LocalDbContext();

    public GroupRepository() {
      Connector = new DatabaseConnector();
    }

    public IList<Group> GetGroupsForApi(long userId, DateTime lastUpdateTime) {
      string query = "SELECT * FROM Groups WHERE UserId = @UserId AND LastUpdateTime > @LastUpdateTime";
      query = "SELECT * FROM Groups";
      MySqlCommand command = new MySqlCommand(query);
      //command.Parameters.AddWithValue("@UserId", userId);
      //command.Parameters.AddWithValue("@LastUpdateTime", lastUpdateTime);
      IEnumerable<DataRow> rows = Connector.ExecuteForRows(command);

      IList<Group> groups = new List<Group>();
      foreach (var row in rows) {
        
      }
      return groups;
    }
  }
}