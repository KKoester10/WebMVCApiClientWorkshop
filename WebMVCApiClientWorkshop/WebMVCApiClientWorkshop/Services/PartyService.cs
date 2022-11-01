using DnDCharacter.Models;
using WebMVCApiClientWorkshop.Helpers;
using WebMVCApiClientWorkshop.Models;
using WebMVCApiClientWorkshop.Services.Interfaces;
namespace WebMVCApiClientWorkshop.Services
{
    public class PartyService : IPartyService
    {

        private readonly HttpClient _client;
        public const string BasePath = "/api/Parties/";

        public PartyService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<Party>> FindAll()
        {
            var responseGet = await _client.GetAsync(BasePath);

            var response = await responseGet.ReadContentAsync<List<Party>>();

            return response;
        }

        public async Task<Party> FindOne(int id)
        {
            var request = BasePath + id.ToString();
            var responseGet = await _client.GetAsync(request);

            var response = await responseGet.ReadContentAsync<Party>();

            var party = new Party(response.Id, response.Name);

            return party;
        }
    }
}
