using TeamGame.Domain.Contracts.Game;
using TeamGame.Domain.Contracts.Lobby;

namespace TeamGame.Domain.Lobby;

public sealed class LobbyService: ILobbyService
{
    private readonly IGameService _gameService;

    /// <inheritdoc cref="TeamGame.Domain.Game.GameService.GameId"/>
    private const string GameId = TeamGame.Domain.Game.GameService.GameId;
    
    public LobbyService(
        IGameService gameService)
    {
        _gameService = gameService;
    }

    public Task<bool> Join(string lobbyId, string playerId)
    {
        //todo: get lobby and add player
        //hack: we are just going to start the game for now
        //we use the lobby id for the game id for now

        Task.Run<int>(async () =>
        {
            //todo:use real team id
            var teamId = Guid.NewGuid().ToString();
            await _gameService.Start(
                lobbyId,
                new[] {(playerId: playerId, teamId: teamId)});
            return 1;
        });
        return Task.FromResult(true);
    }

    public bool TrySanitizeLobbyId(string input, out string lobbyId)
    {
        if (GameId != input)
        {
            lobbyId = String.Empty;
            return false;
        }

        lobbyId = input;
        return true;
    }
}