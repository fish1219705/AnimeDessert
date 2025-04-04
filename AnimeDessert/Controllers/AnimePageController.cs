using AnimeDessert.Interfaces;
using AnimeDessert.Models;
using AnimeDessert.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimeDessert.Controllers
{
    [Route("Anime")]
    public class AnimePageController : Controller
    {
        private readonly IAnimeService _animeService;

        // dependency injection of service interfaces
        public AnimePageController(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        // GET: Anime
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AnimeDto> animeDtos = await _animeService.ListAnimes();
            return View(animeDtos);
        }

        // GET: Anime/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            AnimeDto? animeDto = await _animeService.FindAnime(id);

            return animeDto != null
                ? View(animeDto)
                : View("Error", new ErrorViewModel() { Errors = ["Anime not found."] }); ;
        }

        // GET: Anime/New
        [HttpGet("New")]
        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        // POST: Anime/Add
        [HttpPost("Add")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Add([FromForm] AddAnimeRequest request)
        {
            (ServiceResponse response, AnimeDto? animeDto) = await _animeService.AddAnime(request);

            return (response.Status == ServiceStatus.Created && animeDto != null)
                ? RedirectToAction("Details", "AnimePage", new { id = animeDto.AnimeId })
                : PartialView("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // GET: Anime/{id}/Edit
        [HttpGet("{id}/Edit")]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            AnimeDto? animeDto = await _animeService.FindAnime(id);

            return animeDto != null
                ? View(animeDto)
                : View("Error", new ErrorViewModel() { Errors = ["Anime not found."] }); ;
        }

        // POST: Anime/{id}/Update
        [HttpPost("Anime/{id}/Update")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateAnimeRequest request)
        {
            ServiceResponse response = await _animeService.UpdateAnime(id, request);

            return response.Status == ServiceStatus.Updated
                ? RedirectToAction("Details", "AnimePage", new { id = id })
                : View("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // GET: Anime/{id}/ConfirmDelete
        [HttpGet("Anime/{id}/ConfirmDelete")]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            AnimeDto? animeDto = await _animeService.FindAnime(id);

            return animeDto != null
                ? View(animeDto)
                : View("Error", new ErrorViewModel() { Errors = ["Anime not found."] }); ;
        }

        // POST: Anime/{id}/Delete
        [HttpPost("Anime/{id}/Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _animeService.DeleteAnime(id);

            return response.Status == ServiceStatus.Deleted
                ? RedirectToAction("Index", "AnimePage")
                : View("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // GET: Anime/{id}/NewImages
        [HttpGet("{id}/NewImages")]
        [Authorize]
        public async Task<IActionResult> NewImages(int id)
        {
            AnimeDto? animeDto = await _animeService.FindAnime(id);

            return animeDto != null
                ? View(animeDto)
                : View("Error", new ErrorViewModel() { Errors = ["Anime not found."] });
        }

        // POST: Anime/{id}/AddImages
        [HttpPost("{id}/AddImages")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> AddImages(int id, [FromForm] AddImagesToAnimeRequest request)
        {
            ServiceResponse response = await _animeService.AddImagesToAnime(id, request);

            return response.Status == ServiceStatus.Created
                ? RedirectToAction("Details", "AnimePage", new { id = id })
                : PartialView("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // POST: Anime/{id}/RemoveImages
        [HttpPost("{id}/RemoveImages")]
        [Authorize]
        public async Task<IActionResult> RemoveImages(int id, [FromForm] RemoveImagesFromAnimeRequest request)
        {
            ServiceResponse response = await _animeService.RemoveImagesFromAnime(id, request);

            return response.Status == ServiceStatus.Deleted
                ? RedirectToAction("Details", "AnimePage", new { id = id })
                : View("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // GET: Anime/{id}/NewMusics
        [HttpGet("{id}/NewMusics")]
        [Authorize]
        public async Task<IActionResult> NewMusics(int id)
        {
            AnimeDto? animeDto = await _animeService.FindAnime(id);

            return animeDto != null
                ? View(animeDto)
                : View("Error", new ErrorViewModel() { Errors = ["Anime not found."] });
        }

        // POST: Anime/{id}/AddMusics
        [HttpPost("{id}/AddMusics")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> AddMusics(int id, [FromForm] AddMusicsToAnimeRequest request)
        {
            ServiceResponse response = await _animeService.AddMusicsToAnime(id, request);

            return response.Status == ServiceStatus.Created
                ? RedirectToAction("Details", "AnimePage", new { id = id })
                : PartialView("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // POST: Anime/{id}/RemoveMusics
        [HttpPost("{id}/RemoveMusics")]
        [Authorize]
        public async Task<IActionResult> RemoveMusics(int id, [FromForm] RemoveMusicsFromAnimeRequest request)
        {
            ServiceResponse response = await _animeService.RemoveMusicsFromAnime(id, request);

            return response.Status == ServiceStatus.Deleted
                ? RedirectToAction("Details", "AnimePage", new { id = id })
                : View("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // GET: Anime/{id}/NewCharacters
        [HttpGet("{id}/NewCharacters")]
        [Authorize]
        public async Task<IActionResult> NewCharacters(int id)
        {
            AnimeDto? animeDto = await _animeService.FindAnime(id);
            IEnumerable<CharacterVersionDto> characterVersionDtos = await _animeService.FindAvailableCharactersForAnime(id);

            return animeDto != null
                ? View(new AnimeAvailableCharacter { AnimeDto = animeDto, CharacterVersionDtos = characterVersionDtos })
                : View("Error", new ErrorViewModel() { Errors = ["Anime not found."] });
        }

        // POST: Anime/{id}/AddCharacters
        [HttpPost("{id}/AddCharacters")]
        [Authorize]
        public async Task<IActionResult> AddCharacters(int id, [FromForm] AddCharacterVersionsToAnimeRequest request)
        {
            ServiceResponse response = await _animeService.AddCharacterVersionsToAnime(id, request);

            return response.Status == ServiceStatus.Created
                ? RedirectToAction("Details", "AnimePage", new { id = id })
                : PartialView("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // POST: Anime/{id}/RemoveCharacters
        [HttpPost("{id}/RemoveCharacters")]
        [Authorize]
        public async Task<IActionResult> RemoveCharacters(int id, [FromForm] RemoveCharacterVersionsFromAnimeRequest request)
        {
            ServiceResponse response = await _animeService.RemoveCharacterVersionsFromAnime(id, request);

            return response.Status == ServiceStatus.Deleted
                ? RedirectToAction("Details", "AnimePage", new { id = id })
                : View("Error", new ErrorViewModel() { Errors = response.Messages });
        }
    }
}
