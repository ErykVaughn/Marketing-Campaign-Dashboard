using MySql.Data.MySqlClient;
using BackendAPI.Database;


namespace BackendAPI.Services
{

    public interface IAudienceService
    {
        List<Audience> GetAllAudiences();
        List<Audience> GetAudiencesByCampaignId(int campaignId);
    }

    public class AudienceService : IAudienceService
    {
        private readonly IDatabaseWrapper _databaseWrapper;

        public AudienceService(IDatabaseWrapper databaseWrapper)
        {
            _databaseWrapper = databaseWrapper;
        }

        public List<Audience> GetAllAudiences()
        {
            string query = "SELECT Campaign_ID, Record_ID, Has_Phone, Has_Email FROM Audiences";
            return _databaseWrapper.ExecuteQuery(query, MapAudience);
        }

        public List<Audience> GetAudiencesByCampaignId(int campaignId)
        {
            string query = "SELECT Campaign_ID, Record_ID, Has_Phone, Has_Email FROM Audiences WHERE Campaign_ID = @CampaignId";
            var parameters = new[] { new MySqlParameter("@CampaignId", campaignId) };
            return _databaseWrapper.GetEntitiesByQuery(query, MapAudience, parameters);
        }

        private Audience MapAudience(MySqlDataReader reader)
        {
            return new Audience
            {
                Campaign_ID = reader.GetInt32("Campaign_ID"),
                Record_ID = reader.GetInt32("Record_ID"),
                Has_Phone = reader.GetBoolean("Has_Phone"),
                Has_Email = reader.GetBoolean("Has_Email")
            };
        }
    }
}

