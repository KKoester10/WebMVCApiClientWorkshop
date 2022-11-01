using DnDCharacter.Models;
using WebMVCApiClientWorkshop.Helpers;
using WebMVCApiClientWorkshop.Services.Interfaces;

namespace WebMVCApiClientWorkshop.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/Characters/";

        public CharacterService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<Character>> FindAll()
        {
            var responseGet = await _client.GetAsync(BasePath);

            var response = await responseGet.ReadContentAsync<List<Character>>();

            return response;
        }

        public async Task<Character> FindOne(int id)
        {
            var request = BasePath + id.ToString();
            var responseGet = await _client.GetAsync(request);

            var response = await responseGet.ReadContentAsync<Character>();

            var character = new Character(
                response.Id,
                response.PlayerName,
                response.Class, 
                response.Level,
                response.Race,
                response.Allignment,
                response.Background,
                response.ProficiencyBonus,
                response.Experiance,
                response.ArmorClass, 
                response.Initiative,
                response.HitPoints,
                response.Speed,
                response.PartyId,
                response.Party,
                response.AbilitiesId,
                response.Abilities,
                response.InventoryId,
                response.Inventory);

            return character;
        }
    }
}
