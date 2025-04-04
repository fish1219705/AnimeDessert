using AnimeDessert.Interfaces;
using AnimeDessert.Models;
using AnimeDessert.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimeDessert.Controllers
{
    [Route("Character")]
    public class CharacterPageController : Controller
    {
        private readonly ICharacterService _characterService;

        // dependency injection of service interfaces
        public CharacterPageController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        // GET: Character
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CharacterDto> characterDtos = await _characterService.ListCharacters();
            return View(characterDtos);
        }

        // GET: Character/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            CharacterDto? characterDto = await _characterService.FindCharacter(id);

            if (characterDto != null)
            {
                IEnumerable<DessertDto> dessertDtos = await _characterService.ListDessertsForCharacter(id);
                characterDto.DessertDtos = dessertDtos.ToList();
            }

            return characterDto != null
                ? View(characterDto)
                : View("Error", new ErrorViewModel() { Errors = ["Character not found."] }); ;
        }

        // GET: Character/New
        [HttpGet("New")]
        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        // POST: Character/Add
        [HttpPost("Add")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Add([FromForm] AddCharacterRequest request)
        {
            (ServiceResponse response, CharacterDto? characterDto) = await _characterService.AddCharacter(request);

            return (response.Status == ServiceStatus.Created && characterDto != null)
                ? RedirectToAction("Details", "CharacterPage", new { id = characterDto.CharacterId })
                : PartialView("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // GET: Character/{id}/Edit
        [HttpGet("{id}/Edit")]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            CharacterDto? characterDto = await _characterService.FindCharacter(id);

            return characterDto != null
                ? View(characterDto)
                : View("Error", new ErrorViewModel() { Errors = ["Character not found."] }); ;
        }

        // POST: Character/{id}/Update
        [HttpPost("{id}/Update")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCharacterRequest request)
        {
            ServiceResponse response = await _characterService.UpdateCharacter(id, request);

            return response.Status == ServiceStatus.Updated
                ? RedirectToAction("Details", "CharacterPage", new { id = id })
                : View("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // GET: Character/{id}/ConfirmDelete
        [HttpGet("{id}/ConfirmDelete")]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            CharacterDto? characterDto = await _characterService.FindCharacter(id);

            return characterDto != null
                ? View(characterDto)
                : View("Error", new ErrorViewModel() { Errors = ["Character not found."] }); ;
        }

        // POST: Character/{id}/Delete
        [HttpPost("{id}/Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _characterService.DeleteCharacter(id);

            return response.Status == ServiceStatus.Deleted
                ? RedirectToAction("Index", "CharacterPage")
                : View("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // GET: Character/{id}/NewVersions
        [HttpGet("{id}/NewVersions")]
        [Authorize]
        public async Task<IActionResult> NewVersions(int id)
        {
            CharacterDto? characterDto = await _characterService.FindCharacter(id);

            return characterDto != null
                ? View(characterDto)
                : View("Error", new ErrorViewModel() { Errors = ["Character not found."] });
        }

        // POST: Character/{id}/AddVersions
        [HttpPost("{id}/AddVersions")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> AddVersions(int id, [FromForm] AddVersionToCharacterRequest request)
        {
            (ServiceResponse response, CharacterVersionDto? characterVersionDto) = await _characterService.AddVersionToCharacter(id, request);

            return (response.Status == ServiceStatus.Created && characterVersionDto != null)
                ? RedirectToAction("Details", "CharacterVersionPage", new { id = characterVersionDto.CharacterVersionId })
                : PartialView("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        // POST: Character/{id}/RemoveVersions
        [HttpPost("{id}/RemoveVersions")]
        [Authorize]
        public async Task<IActionResult> RemoveVersions(int id, [FromForm] RemoveVersionsFromCharacterRequest request)
        {
            ServiceResponse response = await _characterService.RemoveVersionsFromCharacter(id, request);

            return response.Status == ServiceStatus.Deleted
                ? RedirectToAction("Details", "CharacterPage", new { id = id })
                : View("Error", new ErrorViewModel() { Errors = response.Messages });
        }
    }
}
