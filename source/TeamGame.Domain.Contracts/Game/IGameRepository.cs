namespace TeamGame.Domain.Contracts.Game;

public interface IGameRepository
{
    bool Exists(string gameId);
    Task Add(IGame game);
}