using DayEndStatusAPI.Interface;
using DayEndStatusAPI.Models;
using Microsoft.Data.SqlClient;

namespace DayEndStatusAPI.Implementation
{
    public class Server1Compass:IServer1compass
    {
        public string ConnectionString = null;
        public IConfiguration Configuration { get; set; }

        public Server1Compass(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables();
            Configuration = configuration;

            ConnectionString = Configuration.GetSection("ConnectionString:SQLServer").Value;
        }

        #region || GetCurrencyUom ||
        public ICollection<StatusMessage> GetStatusMessages()
        {
            try
            {
                string Query = @"
    SELECT TOP 1
        CASE
            WHEN fde_int_MiningStatus = 590 THEN 'Day end Completed'
            ELSE
                (SELECT 'DayEndNotCompleted' as MiningStatus
                 FROM facilitydayend
                 WHERE fde_int_MiningStatus = 580 OR fde_int_MiningStatus IS NULL)
        END AS MiningStatus,
        CASE
            WHEN fde_int_ReportingStatus = 590 THEN 'Day end Completed'
            ELSE
                (SELECT 'DayEndNotCompleted' as ReportingStatus
                 FROM facilitydayend
                 WHERE fde_int_ReportingStatus = 580 OR fde_int_ReportingStatus IS NULL)
        END AS ReportingStatus
    FROM
        facilitydayend
    INNER JOIN
        storagefacility ON stf_int_FacilityId = fde_int_FacilityId
    WHERE
        fde_dtm_DayEndDate = '2024-02-24'
    ORDER BY
        fde_dtm_Datetime DESC;
";



                //SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, Query);
                var statusMessages = new List<StatusMessage>();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cm = new SqlCommand(Query);
                    try
                    {

                        cm.Connection = connection;
                        connection.Open(); //oracle connection object
                        SqlDataReader dataReader = cm.ExecuteReader();
                        //reader.Read();
                        while (dataReader.Read())
                        {
                            statusMessages.Add(new StatusMessage()
                            {
                                
                                MiningStatus = (string)dataReader["MiningStatus"],
                                ReportingStatus = (string)dataReader["ReportingStatus"]
                            });
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                    }
                }
                return statusMessages;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                SqlConnection cn = null;
                try
                {
                    cn = new SqlConnection(ConnectionString);
                    cn.Close();
                }
                finally
                {
                    if (cn != null) cn.Dispose();
                }
                return null;
            }
        }
        #endregion
    }
}
