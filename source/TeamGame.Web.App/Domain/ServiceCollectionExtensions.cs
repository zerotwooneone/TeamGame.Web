using Microsoft.Extensions.DependencyInjection;
using TeamGame.Domain.Contracts.Game;
using TeamGame.Domain.Contracts.Lobby;
using TeamGame.Domain.Game;
using TeamGame.Domain.Lobby;
using TeamGame.Web.App.Domain.Game;
using IGameRepository = TeamGame.Domain.Contracts.Game.IGameRepository;

namespace TeamGame.Web.App.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<ILobbyService, LobbyService>()
            .AddSingleton<IGameRepository, GameRepository>()
            .AddSingleton<IGameService, GameService>();
    }
}