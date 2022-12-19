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

public static class GuidExtensions
{
    public static string ToUrlBase64(this Guid guid)
    {
        return Base64UrlEncoder.Encode(guid.ToByteArray());
    }
}

public static class StringExtensions
{
    public static byte[] FromBase64ToBytes(this string value)
    {
        return Base64UrlEncoder.DecodeBytes(value);
    }
    
    public static bool TryGetGuidFromUrlBase64(this string value, out Guid guid)
    {
        var bytes = value.FromBase64ToBytes();
        try
        {
            guid = new Guid(bytes);
            return true;
        }
        catch
        {
            
        }
        guid = Guid.Empty;
        return false;
    }

    /// <summary>
    /// Best used on short string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string FastTrimAllWhitespace(this string str) {

        var len = str.Length;
        var src = str.ToCharArray();
        int dstIdx = 0;

        for (int i = 0; i < len; i++) {
            var ch = src[i];

            switch (ch) {

                case '\u0020': case '\u00A0': case '\u1680': case '\u2000': case '\u2001':

                case '\u2002': case '\u2003': case '\u2004': case '\u2005': case '\u2006':

                case '\u2007': case '\u2008': case '\u2009': case '\u200A': case '\u202F':

                case '\u205F': case '\u3000': case '\u2028': case '\u2029': case '\u0009':

                case '\u000A': case '\u000B': case '\u000C': case '\u000D': case '\u0085':
                    continue;

                default:
                    src[dstIdx++] = ch;
                    break;
            }
        }
        return new string(src, 0, dstIdx);
    }
}