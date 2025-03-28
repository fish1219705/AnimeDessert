using AnimeDessert.Models;

namespace AnimeDessert.Interfaces
{
    public interface IMusicService
    {
        // base CRUD
        Task<MusicDto?> FindMusic(int id);

        // related methods
        Task<ServiceResponse> AddSingersToMusic(int id, AddSingersToMusicRequest request);

        Task<ServiceResponse> RemoveSingersFromMusic(int id, RemoveSingersFromMusicRequest request);

        Task<IEnumerable<StaffDto>> FindAvailableSingersForMusic(int id);
    }
}
