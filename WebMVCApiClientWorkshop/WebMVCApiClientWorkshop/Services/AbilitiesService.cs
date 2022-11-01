using DnDCharacter.Models;
using WebMVCApiClientWorkshop.Helpers;
using WebMVCApiClientWorkshop.Services.Interfaces;

namespace WebMVCApiClientWorkshop.Services
{
    public class AbilitiesService : IAbilitiesService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/Abilities/";

        public AbilitiesService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<Abilities>> FindAll()
        {
            var responseGet = await _client.GetAsync(BasePath);

            var response = await responseGet.ReadContentAsync<List<Abilities>>();

            return response;
        }

        public async Task<Abilities> FindOne(int id)
        {
            var request = BasePath + id.ToString();
            var responseGet = await _client.GetAsync(request);

            var response = await responseGet.ReadContentAsync<Abilities>();

            var abilities = new Abilities(
                response.Id,
                response.Strength,
                response.Constitution,
                response.Charisma,
                response.Wisdom,
                response.Intelligence,
                response.Dexterity);

            return abilities;
        }
    }
}
