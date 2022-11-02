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
            var abilities = new Abilities
            {
                Id = id,
                Strength = 0,
                Dexterity = 0,
                Constitution = 0,
                Charisma = 0,
                Intelligence = 0,
                Wisdom = 0
            };
            var inventory = new CharacterInventory
            {
                Id = id,
                ItemName = "things",
                Amount = 0
            };
            var party = new Party { Name = response.Party.Name };
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
                party,
                response.AbilitiesId,
                abilities,
                response.InventoryId,
                inventory
                );

            return character;
        }
    }
}
