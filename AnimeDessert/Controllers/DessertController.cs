using AnimeDessert.Interfaces;
using AnimeDessert.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeDessert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DessertController : ControllerBase
    {
        private readonly IDessertService _dessertService;
        // dependency injection of service interfaces
        public DessertController(IDessertService DessertService)
        {
            _dessertService = DessertService;
        }

        /// <summary>
        /// Returns a list of Desserts, each represented by a DessertDto with their asscoiated Ingredients and Reviews
        /// </summary>
        /// <returns>
        /// 200 OK
        /// [{DessertDto}, {DessertDto}, ...]
        /// </returns>
        /// <example>
        /// GET: api/Dessert/List -> [{DessertDto}, {DessertDto}, ...]
        /// </example>
        [HttpGet(template: "List")]
        public async Task<ActionResult<IEnumerable<DessertDto>>> ListDesserts()
        {
            // empty list of data transfer object DessertDto
            IEnumerable<DessertDto> DessertDtos = await _dessertService.ListDesserts();

            return Ok(DessertDtos);
        }


        /// <summary>
        /// Returns a single Dessert specified by its {id}
        /// </summary>
        /// <param name="id">The dessert id</param>
        /// <returns>
        /// 200 OK
        /// {DessertDto}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/Dessert/Find/1 -> {DessertDto}
        /// </example>

        [HttpGet(template: "Find/{id}")]
        public async Task<ActionResult<DessertDto>> FindDessert(int id)
        {

            var dessert = await _dessertService.FindDessert(id);

            // if the dessert could not be located, return 404 Not Found
            if (dessert == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dessert);
            }
        }

        /// <summary>
        /// Updates a Dessert
        /// </summary>
        /// <param name="id">The ID of the dessert to update</param>
        /// <param name="DessertDto">The required information to update the dessert ()</param>
        /// <returns>
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// or
        /// 204 No Content
        /// </returns>
        /// <example>
        /// PUT: api/Dessert/Update/2
        /// Request Headers: Content-Type: application/json, cookie: .AspNetCore.Identity.Application={token}
        /// Request Body: {DessertDto}
        /// ->
        /// Response Code: 204 No Content
        /// </example>
        [HttpPut(template: "Update/{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateDessert(int id, DessertDto DessertDto)
        {
            // {id} in URL must match DessertId in POST Body
            if (id != DessertDto.DessertId)
            {
                //404 Bad Request
                return BadRequest();
            }

            ServiceResponse response = await _dessertService.UpdateDessert(DessertDto);
            if (response.Status == ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);

            }
            else if (response.Status == ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            //Statis = Updated
            return NoContent();
        }


        /// <summary>
        /// Adds a Dessert
        /// </summary>
        /// <param name="DessertDto">The required information to add the dessert (DessertName,DessertDescription...)</param>
        /// <returns>
        /// 201 Created
        /// Location: api/Dessert/Find/{DessertId}
        /// {DessertDto}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// POST: api/Dessert/Add
        /// Request Headers: Content-Type: application/json, cookie: .AspNetCore.Identity.Application={token}
        /// Request Body: {DessertDto}
        /// ->
        /// Response Code: 201 Created
        /// Response Headers: Location: api/Dessert/Find/{DessertId}
        /// </example>
        [HttpPost(template: "Add")]
        [Authorize]
        public async Task<ActionResult<Dessert>> AddDessert(DessertDto DessertDto)
        {
            ServiceResponse response = await _dessertService.AddDessert(DessertDto);

            if (response.Status == ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            // returns 201 Created with Location
            return Created($"api/Dessert/FindDessert/{response.CreatedId}", DessertDto);
        }

        /// <summary>
        /// Deleted the dessert
        /// </summary>
        /// <param name="id">The id of the dessert to delete</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// DELETE: api/Dessert/Delete/2
        /// Headers: cookie: .AspNetCore.Identity.Application={token}
        /// ->
        /// Response Code: 204 No Content
        /// </example>
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteDessert(int id)
        {
            ServiceResponse response = await _dessertService.DeleteDessert(id);

            if (response.Status == ServiceStatus.NotFound)
            {
                return NotFound();
            }
            else if (response.Status == ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            return NoContent();

        }


        /// <summary>
        /// Returns a list of Desserts for a specific ingredient by its {id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 200 OK
        /// [{DessertDto}, {DessertDto},..]
        /// </returns>
        /// <example>
        /// GET: api/Dessert/ListForIngredient/3 -> [{DessertDto}, {DessertDto},..]
        /// </example>
        [HttpGet(template: "ListForIngredient/{id}")]
        public async Task<IActionResult> ListDessertsForIngredient(int id)
        {
            // empty list of data transfer object DessertDto
            IEnumerable<DessertDto> DessertDtos = await _dessertService.ListDessertsForIngredient(id);
            // return 200 OK with DessertDtos
            return Ok(DessertDtos);
        }

        /// <summary>
        /// Unlinks a dessert from a ingredient
        /// </summary>
        /// <param name="dessertId">The id of the dessert</param>
        /// <param name="ingredientId">The id of the ingredient</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 302 Redirect (/Identity/Account/Login)
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// Delete: api/Dessert/Unlink?DessertId=4&IngredientId=1
        /// Headers: cookie: .AspNetCore.Identity.Application={token}
        /// ->
        /// Response Code: 204 No Content
        /// </example>
        [HttpDelete("Unlink")]
        [Authorize]
        public async Task<ActionResult> Unlink(int dessertId, int ingredientId)
        {
            ServiceResponse response = await _dessertService.UnlinkDessertFromIngredient(dessertId, ingredientId);

            if (response.Status == ServiceStatus.NotFound)
            {
                return NotFound();
            }
            else if (response.Status == ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            return NoContent();

        }

        /// <summary>
        /// Links a dessert to a ingredient
        /// </summary>
        /// <param name="dessertId">The id of the dessert</param>
        /// <param name="ingredientId">The id of the ingredient</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 302 Redirect (/Identity/Account/Login)
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// Post: api/Dessert/Link?DessertId=4&IngredientId=1
        /// Headers: cookie: .AspNetCore.Identity.Application={token}
        /// ->
        /// Response Code: 204 No Content
        /// </example>
        [HttpPost("Link")]
        [Authorize]
        public async Task<ActionResult> Link(int dessertId, int ingredientId)
        {
            ServiceResponse response = await _dessertService.LinkDessertToIngredient(dessertId, ingredientId);

            if (response.Status == ServiceStatus.NotFound)
            {
                return NotFound();
            }
            else if (response.Status == ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            return NoContent();

        }

        /// <summary>
        /// Returns a list of Images for Dessert {id}
        /// </summary>
        /// <returns>
        /// 200 OK
        /// [ {ImageDto}, {ImageDto}, ... ]
        /// </returns>
        /// <example>
        /// GET: api/Dessert/ListImages/1 -> [ {ImageDto}, {ImageDto}, ... ]
        /// </example>
        [HttpGet(template: "ListImages/{id}")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> ListImagesForDessert(int id)
        {
            IEnumerable<ImageDto> imageDtos = await _dessertService.ListImagesForDessert(id);
            return Ok(imageDtos);
        }

        /// <summary>
        /// Adds Images to a Dessert
        /// </summary>
        /// <param name="request">The required information to add Images to an Dessert (ImageFiles)</param>
        /// <returns>
        /// 200 OK
        /// [string, ...] Including a message for no. of affected records
        /// or
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// or
        /// 500 Internal Server Error
        /// </returns>
        /// <example>
        /// POST: api/Dessert/AddImages/1
        /// Request Headers: { "Content-Type": "multipart/form-data" }
        /// Request Form Data: {AddImagesToDessertRequest}
        /// ->
        /// Response Code: 200 OK
        /// Response Body: ["3 records are affected."]
        /// </example>
        [HttpPost(template: "AddImages/{id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<ActionResult> AddImagesToDessert(int id, [FromForm] AddImagesToDessertRequest request)
        {
            ServiceResponse response = await _dessertService.AddImagesToDessert(id, request);

            switch (response.Status)
            {
                case ServiceStatus.BadRequest:
                    return BadRequest(response.Messages);
                case ServiceStatus.NotFound:
                    return NotFound(response.Messages);
                case ServiceStatus.Error:
                    return StatusCode(500, response.Messages);
                default:
                    // Status = Created
                    return Ok(response.Messages);
            }
        }

        /// <summary>
        /// Remove Images from a Dessert
        /// </summary>
        /// <param name="id">The id of the Dessert</param>
        /// <param name="request">The request object, including a list of Image Ids (int)</param>
        /// <returns>
        /// 200 OK
        /// [string, ...] Including a message for no. of affected records
        /// or
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// or
        /// 500 Internal Server Error
        /// </returns>
        /// <example>
        /// DELETE: api/Dessert/RemoveImages/1
        /// Headers: { "Content-Type": "application/json" }
        /// Body: { ImageIds: [ 1, 2, 3 ] }
        /// ->
        /// Response Code: 200 OK
        /// Response Body: ["3 records are affected."]
        /// </example>
        [HttpDelete(template: "RemoveImages/{id}")]
        [Consumes("application/json")]
        [Authorize]
        public async Task<ActionResult> RemoveImagesFromDessert(int id, [FromBody] RemoveImagesFromDessertRequest request)
        {
            ServiceResponse response = await _dessertService.RemoveImagesFromDessert(id, request);

            switch (response.Status)
            {
                case ServiceStatus.BadRequest:
                    return BadRequest(response.Messages);
                case ServiceStatus.NotFound:
                    return NotFound(response.Messages);
                case ServiceStatus.Error:
                    return StatusCode(500, response.Messages);
                default:
                    // Status = Deleted
                    return Ok(response.Messages);
            }
        
        }
    }
}
