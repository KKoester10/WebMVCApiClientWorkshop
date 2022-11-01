using DnDCharacter.Models;
using WebMVCApiClientWorkshop.Helpers;
using WebMVCApiClientWorkshop.Services.Interfaces;

namespace WebMVCApiClientWorkshop.Services
{
    public class CharacterInventoryService : ICharacterInventoryService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/CharactersInventories/";

        public CharacterInventoryService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CharacterInventory>> FindAll()
        {
            var responseGet = await _client.GetAsync(BasePath);

            var response = await responseGet.ReadContentAsync<List<CharacterInventory>>();

            return response;
        }

        public async Task<CharacterInventory> FindOne(int id)
        {
            var request = BasePath + id.ToString();
            var responseGet = await _client.GetAsync(request);

            var response = await responseGet.ReadContentAsync<CharacterInventory>();

            var characterInventory = new CharacterInventory(response.Id, response.ItemName, response.Amount);

            return characterInventory;
        }
    }
}
