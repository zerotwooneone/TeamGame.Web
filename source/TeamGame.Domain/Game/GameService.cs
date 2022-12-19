using TeamGame.Domain.Board;
using TeamGame.Domain.Contracts.Game;
using TeamGame.Domain.Team;
using TeamGame.Domain.Token;

namespace TeamGame.Domain.Game;

public sealed class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    /// <summary>
    /// This is a hack for dev purposes
    /// </summary>
    public const string GameId = @"fTIKuJ6zKUedsPMJ79OOhA";

    public GameService(
        IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task Start(
        string gameId, 
        IEnumerable<(string teamId, string playerId)> teamMap)
    {
        if (_gameRepository.Exists(gameId))
        {
            throw new ArgumentException($"cannot start game in progress ${gameId}", nameof(gameId));
        }

        int maxActions = 5;
        var round = Round.Round.Create(1,DateTimeOffset.Now.AddMinutes(5), maxActions);
        var teams = teamMap.Select(m =>
        {
            var teamToken = TeamToken.Create(
                TokenShape.Circle, 
                TokenColor.Green, 
                BoardLocation.Create(0, 0), null);
            return Team.Team.Create(m.teamId,
                teamToken);
        });
        const int spaceSize = 25;
        const int rowCount = 5;
        const int columnCount = rowCount;
        var rows = Enumerable.Range(0, rowCount)
            .Select(rowIndex => Enumerable.Range(0, columnCount).Select(columnIndex =>
            {
                return Space.Create(true, null, null, null);
            }));
        var board = Board.Board.Create(
            rowCount,
            columnCount,
            rows,
            spaceSize);
        var game = Game.Create(
            gameId,
            round,
            teams,
            board,
            Enumerable.Empty<Pickup.Pickup>());

        await _gameRepository.Add(game);
    }
}