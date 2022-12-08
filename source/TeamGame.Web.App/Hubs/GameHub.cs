using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TeamGame.Web.App.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        
    }
}
