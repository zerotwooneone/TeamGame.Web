using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TeamGame.Domain.Contracts.Lobby;

namespace TeamGame.Web.App.Controllers;

[Route("[controller]/{id}/[action]")]
public class LobbyController : Controller
{
    private readonly ILobbyService _lobbyService;

    public LobbyController(
        ILobbyService lobbyService)
    {
        _lobbyService = lobbyService;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <example>https://localhost:44386/lobby/ZlRJS3VKNnpLVWVkc1BNSjc5T09oQQ/join</example>
    public async Task<IActionResult> Join(
        [FromRoute] string id)
    {
        const int maxLength = 128;
        const int minLength = 10;
        if (string.IsNullOrWhiteSpace(id) ||
            id.Length > maxLength ||
            id.Length < minLength)
        {
            return new UnauthorizedResult();
        }

        var decoded = Base64UrlEncoder.Decode(id);
        if (!_lobbyService.TrySanitizeLobbyId(decoded, out var lobbyId) ||
            string.IsNullOrWhiteSpace(lobbyId))
        {
            return new UnauthorizedResult();
        }

        //todo:get a real user id
        var userId = Guid.NewGuid().ToString();
        if (!(await _lobbyService.Join(lobbyId, userId)))
        {
            return new UnauthorizedResult();
        }
        
        return new JsonResult("hi");
    }
}