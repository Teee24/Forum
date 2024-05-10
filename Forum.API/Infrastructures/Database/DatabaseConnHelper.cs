using Microsoft.Data.SqlClient;

namespace Forum.API.Infrastructures.Database;

public class DatabaseConnHelper
{
    private readonly IConfiguration _configuration;

    public DatabaseConnHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    //要先安裝SqlClient套件
    public SqlConnection ForumConnection() => new SqlConnection(_configuration.GetConnectionString("Forum"));
}
