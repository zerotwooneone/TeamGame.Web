using Microsoft.AspNetCore.Mvc;

namespace TeamGame.Web.App.Controllers;

[Route("[controller]/{id}/[action]")]
public class LobbyController : Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <example>https://localhost:44386/lobby/00000000-0000-0000-0000-000000000001/join</example>
    public IActionResult Join([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return new UnauthorizedResult();
        }

        var lobbyId = id.ToBase64();
        return new JsonResult("hi");
    }
}

public static class GuidExtensions
{
    public static string ToBase64(this Guid guid)
    {
        return Convert.ToBase64String(guid.ToByteArray());
    }
}