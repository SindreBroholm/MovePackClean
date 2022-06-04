using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataSource;


public class DapperContext
{
	private readonly string connectionString;
    private readonly IConfiguration configuration;

    public DapperContext(IConfiguration configuration)
	{
        this.configuration = configuration;
		connectionString = configuration.GetConnectionString("SqlConnection");
    }

	public IDbConnection CreateConnection()
    {
		return new SqlConnection(connectionString);
    }
}

