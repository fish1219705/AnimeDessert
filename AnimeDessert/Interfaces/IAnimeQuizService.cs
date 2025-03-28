using AnimeDessert.Models;

namespace AnimeDessert.Interfaces
{
    public interface IAnimeQuizService
    {
        Task<(ServiceResponse, AnimeQuizDto?)> GenerateAnimeQuiz(int numOfQuestions);

        Task<int> GetTotalAvailable();
    }
}
