using DOCOSoft.UserAPI.DTOs;
using DOCOSoft.UserAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOCOSoft.UserAPI.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound(new { message = "[MSG008] User not found." });

        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] RequestCreateDto requestCreate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userResponse = await userService.AddUserAsync(requestCreate);
        if (userResponse.Data == null)
            return BadRequest(new { message = userResponse.Message });

        return CreatedAtAction(nameof(GetById), new { id = userResponse.Data.Id }, userResponse);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, RequestUpdateDto requestUpdate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userUpdated = await userService.UpdateUserAsync(id, requestUpdate);
        if (userUpdated == null)
            return NotFound(new { message = "[MSG011] User not found." });

        return Ok(userUpdated);
    }
}


