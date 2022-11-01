using DnDCharacter.Models;

namespace WebMVCApiClientWorkshop.Services.Interfaces
{
    public interface IAbilitiesService
    {
        Task<IEnumerable<Abilities>> FindAll();

        Task<Abilities> FindOne(int id);
    }
}
