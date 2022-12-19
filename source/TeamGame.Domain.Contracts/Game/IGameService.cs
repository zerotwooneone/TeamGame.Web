namespace TeamGame.Domain.Contracts.Game;

public interface IGameService
{
    Task Start(string gameId, IEnumerable<(string teamId, string playerId)> teamMap);
}