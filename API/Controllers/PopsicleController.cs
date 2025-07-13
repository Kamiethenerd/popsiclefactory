using Microsoft.AspNetCore.Mvc;
using static API.PopsicleModel;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PopsicleController : ControllerBase
{
    [HttpPost("CreatePopsicle")]
    public IActionResult CreatePopsicle(Popsicle popsicle)
    {
        //check popsicle is valid
        if (ModelState.IsValid)
        {
            //check if popsicle with same name and flavor exists in the list
            var popsicleModel = new PopsicleModel();
            var existingPopsicle = popsicleModel.TestPopsicles
                .FirstOrDefault(p => p.Name.Equals(popsicle.Name, StringComparison.OrdinalIgnoreCase) ||
                                     p.Flavor.Equals(popsicle.Flavor, StringComparison.OrdinalIgnoreCase));
            //if it exists, return 409 Conflict with existing popsicle
            if (existingPopsicle != null)
            {
                return Conflict(new
                {
                    Message = "Popsicle with the same name or flavor already exists.",
                    ExistingPopsicle = existingPopsicle
                });
            }
            else
            {
                try
                {
                    //add the new popsicle to the list
                    popsicle.Id = popsicleModel.TestPopsicles.Count + 1; //simulate ID generation
                    popsicleModel.TestPopsicles.Add(popsicle);

                    //return 201 Created with the created popsicle
                    return Ok(popsicle);

                }
                catch (Exception ex)
                {
                    //log the exception
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating the popsicle.", Error = ex.Message });
                }
            }


        }

        //retrun bad request with validation errors
        return BadRequest(ModelState);
    }
    [HttpPost("ReplacePopsicle/{id}")]
    public IActionResult ReplacePopsicle(int id, Popsicle newPopsicle)
    {
        //check popsicle is valid
        if (ModelState.IsValid)
        {
            //check db if this popsicle exists
            var popsicleModel = new PopsicleModel();
            var existingPopsicle = popsicleModel.TestPopsicles.FirstOrDefault(p => p.Id == id);
            //if it exists, replace it
            if (existingPopsicle != null)
            {

                try
                {
                    //save the changes
                    //ensure the ID remains the same
                    newPopsicle.Id = id;
                    //replace the existing popsicle with the new one
                    popsicleModel.TestPopsicles.Remove(existingPopsicle);
                    popsicleModel.TestPopsicles.Add(newPopsicle);
                    //return 200 OK with the updated popsicle
                    return Ok(newPopsicle);
                }
                catch (Exception ex)
                {
                    //log the exception
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while replacing the popsicle.", Error = ex.Message });
                }
            }
            else
            {
                //if it does not exist, return 404 Not Found
                return NotFound(new { Message = "Popsicle not found." });
            }
        }

        //return bad request with validation errors
        return BadRequest(ModelState);
    }
    [HttpPut("UpdatePopsicle/{id}")]
    public IActionResult UpdatePopsicle(int id, Popsicle popsicleUpdate)
    {
        //check popsicle is valid
        if (ModelState.IsValid)
        {
            //check db if this popsicle exists
            var popsicleModel = new PopsicleModel();
            var existingPopsicle = popsicleModel.TestPopsicles.FirstOrDefault(p => p.Id == id);
            //if it exists, update it
            if (existingPopsicle != null)
            {

                try
                {
                    //if popsicle name is not an empty string, update the existing popsicle name
                    if (!string.IsNullOrWhiteSpace(popsicleUpdate.Name))
                    {   
                        existingPopsicle.Name = popsicleUpdate.Name;
                    }
                    //if popsicle flavor is not an empty string, update the existing popsicle flavor
                    if (!string.IsNullOrWhiteSpace(popsicleUpdate.Flavor))
                    {
                        existingPopsicle.Flavor = popsicleUpdate.Flavor;
                    }
                    //if popsicle description is not an empty string, update the existing popsicle description
                    if (!string.IsNullOrWhiteSpace(popsicleUpdate.Description))
                    {
                        existingPopsicle.Description = popsicleUpdate.Description;
                    }
                    //if popsicle quantity is not null, update the existing popsicle quantity
                    if (popsicleUpdate.Quantity != null)
                    { 
                        existingPopsicle.Quantity = popsicleUpdate.Quantity;
                    }
                    

                    //return 200 OK with the updated popsicle
                    return Ok(existingPopsicle);
                }
                catch (Exception ex)
                {
                    //log the exception
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while updating the popsicle.", Error = ex.Message });
                }
            }
            else
            {
                //if it does not exist, return 404 Not Found
                return NotFound(new { Message = "Popsicle not found." });
            }
        }

        //return bad request with validation errors
        return BadRequest(ModelState);
    }
    [HttpDelete("DeletePopsicle/{id}")]
    public IActionResult DeletePopsicle(int id)
    {
        //check db if this popsicle exists
        var popsicleModel = new PopsicleModel();
        var existingPopsicle = popsicleModel.TestPopsicles.FirstOrDefault(p => p.Id == id);
        //if it exists, delete it
        if (existingPopsicle != null)
        {
            try
            {
                //remove the existing popsicle from the list
                popsicleModel.TestPopsicles.Remove(existingPopsicle);           
        //return 200 OK with a success message
                return Ok(new { Message = "Popsicle deleted successfully." });
            }
            catch (Exception ex)
            {
                //log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while deleting the popsicle.", Error = ex.Message });
            }
        }
        else
        {
            //if it does not exist, return 404 Not Found
            return NotFound(new { Message = "Popsicle not found." });
        }
    }
    [HttpGet("SearchPopsicles")]
    public IActionResult SearchPopsicles(string searchTerm)
    {
        //check if search term is null or empty
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return BadRequest(new { Message = "Search term cannot be null or empty." });
        }

        //search the list of popsicles
        var popsicleModel = new PopsicleModel();
        var results = popsicleModel.TestPopsicles
            .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        p.Flavor.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();

        //if no results found, return 404 Not Found
        if (!results.Any())
        {
            return NotFound(new { Message = "No popsicles found matching the search term." });
        }

        //return 200 OK with the search results
        return Ok(results);
    }
    [HttpGet("GetPopsicleById/{id}")]
    public IActionResult GetPopsicleById(int id)
    {
        //check db if this popsicle exists
        var popsicleModel = new PopsicleModel();
        var existingPopsicle = popsicleModel.TestPopsicles.FirstOrDefault(p => p.Id == id);
        //if it exists, return it
        if (existingPopsicle != null)
        {
            return Ok(existingPopsicle);
        }
        else
        {
            //if it does not exist, return 404 Not Found
            return NotFound(new { Message = "Popsicle not found." });
        }
    }
    [HttpGet("GetAllPopsicles")]
    public IActionResult GetAllPopsicles()
    {
        //return the list of popsicles
        var popsicleModel = new PopsicleModel();
        return Ok(popsicleModel.TestPopsicles);
    }
}