using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TeamGame.Web.App.Controllers;

[Route("[controller]/{id}/[action]")]
public class LobbyController : Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <example>https://localhost:44386/lobby/fTIKuJ6zKUedsPMJ79OOhA/join</example>
    public IActionResult Join([FromRoute] string id)
    {
        // var g = Guid.NewGuid();
        // var test = g.ToUrlBase64();
        // var bl = test.TryGetGuidFromUrlBase64(out var bc);
        // var sb = bc.ToUrlBase64();
        
        const int expectedLength = 22;
        if (string.IsNullOrWhiteSpace(id) ||
            id.Length != expectedLength)
        {
            return new UnauthorizedResult();
        }

        if (!id.TryGetGuidFromUrlBase64(out var gameId) ||
            gameId == Guid.Empty)
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