using BackendAPI.Database;
using BackendAPI.Models;
using MySql.Data.MySqlClient;

namespace BackendAPI.Services
{
    public interface IResponseService
    {
        List<Response> GetAllResponses();
        List<Response> GetResponsesByCampaignId(int campaignId);
    }

    public class ResponseService : IResponseService
    {
        private readonly IDatabaseWrapper _databaseWrapper;

        public ResponseService(IDatabaseWrapper databaseWrapper)
        {
            _databaseWrapper = databaseWrapper;
        }

        public List<Response> GetAllResponses()
        {
            string query = "SELECT Campaign_ID, Record_ID, Lead_Flag, Lead_Timestamp, Funded_Flag, Funded_Timestamp FROM Responses";
            return _databaseWrapper.ExecuteQuery(query, MapToResponse);
        }

        public List<Response> GetResponsesByCampaignId(int campaignId)
        {
            string query = "SELECT Campaign_ID, Record_ID, Lead_Flag, Lead_Timestamp, Funded_Flag, Funded_Timestamp " +
                           "FROM Responses WHERE Campaign_ID = @CampaignId";
            var parameters = new[] { new MySqlParameter("@CampaignId", campaignId) };
            return _databaseWrapper.GetEntitiesByQuery(query, MapToResponse, parameters);
        }

        private Response MapToResponse(MySqlDataReader reader)
        {
            return new Response
            {
                Campaign_ID = reader.GetInt32("Campaign_ID"),
                Record_ID = reader.GetInt32("Record_ID"),
                Lead_Flag = reader.GetBoolean("Lead_Flag"),
                Lead_Timestamp = reader.IsDBNull(reader.GetOrdinal("Lead_Timestamp")) 
                                 ? null 
                                 : reader.GetDateTime("Lead_Timestamp"),
                Funded_Flag = reader.GetBoolean("Funded_Flag"),
                Funded_Timestamp = reader.IsDBNull(reader.GetOrdinal("Funded_Timestamp")) 
                                   ? null 
                                   : reader.GetDateTime("Funded_Timestamp")
            };
        }
    }
}
