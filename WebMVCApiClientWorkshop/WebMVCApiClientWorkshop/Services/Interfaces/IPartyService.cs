using DnDCharacter.Models;

namespace WebMVCApiClientWorkshop.Services.Interfaces
{
    public interface IPartyService
    {
        Task<IEnumerable<Party>> FindAll();

        Task<Party> FindOne(int id);
    }
}
