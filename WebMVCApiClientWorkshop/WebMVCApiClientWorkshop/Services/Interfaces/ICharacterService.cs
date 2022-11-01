using DnDCharacter.Models;

namespace WebMVCApiClientWorkshop.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> FindAll();

        Task<Character> FindOne(int id);
    }
}
