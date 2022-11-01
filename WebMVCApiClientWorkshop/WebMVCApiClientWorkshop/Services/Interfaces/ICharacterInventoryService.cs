using DnDCharacter.Models;

namespace WebMVCApiClientWorkshop.Services.Interfaces
{
    public interface ICharacterInventoryService
    {
        Task<IEnumerable<CharacterInventory>> FindAll();

        Task<CharacterInventory> FindOne(int id);
    }
}
