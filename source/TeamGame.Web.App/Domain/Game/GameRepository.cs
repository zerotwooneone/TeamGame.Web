using TeamGame.Domain.Contracts.Game;

namespace TeamGame.Web.App.Domain.Game;

public class GameRepository : IGameRepository
{
    private IGame? _game;

    /// <inheritdoc cref="TeamGame.Domain.Game.GameService.GameId"/>
    private const string GameId = TeamGame.Domain.Game.GameService.GameId;
    public bool Exists(string gameId)
    {
        return GameId == gameId;
    }

    public async Task Add(IGame game)
    {
        _game = game;
    }
}