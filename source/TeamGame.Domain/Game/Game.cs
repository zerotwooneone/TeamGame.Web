using TeamGame.Domain.Contracts.Game;
using TeamGame.Domain.Util;

namespace TeamGame.Domain.Game;

public sealed class Game : IGame, IDisposable
{
    public string Id { get; init; }
    private readonly ObservablePropertyHelper<Round.Round> _round;
    public ObservableProperty<Round.Round> Round => _round.Property;
    private readonly Dictionary<string,Team.Team> _teams;
    public IReadOnlyDictionary<string,Team.Team> Teams => _teams;
    public readonly Board.Board Board;
    private Dictionary<string, Pickup.Pickup> _pickups;
    public IReadOnlyDictionary<string, Pickup.Pickup> Pickups => _pickups;

    private Game(
        string id, 
        ObservablePropertyHelper<Round.Round> round, 
        Dictionary<string,Team.Team> teams, 
        Board.Board board, 
        Dictionary<string, Pickup.Pickup> pickups)
    {
        Id = id;
        _round = round;
        _teams = teams;
        Board = board;
        _pickups = pickups;
    }

    public static Game Create(
        string id,
        Round.Round round,
        IEnumerable<Team.Team> teams,
        Board.Board board,
        IEnumerable<Pickup.Pickup> pickups)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException($"invalid game id {id}", nameof(id));
        }
        var teamList = teams.ToList();
        if (teamList.Count < 2)
        {
            throw new ArgumentException($"must start with at least 2 teams", nameof(teams));
        }
        return new Game(
            id,
            ObservablePropertyHelper<Round.Round>.Create(round),
            teamList.ToDictionary(t=>t.Id),
            board,
            pickups.ToDictionary(p=>p.Id));
    }

    private bool _isDisposed;
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        if (Round.Assignable.HasBeenSet)
        {
            Round.Assignable.Value.Dispose();
        }
        _round.Dispose();
        foreach (var team in _teams.Values)
        {
            team.Dispose();
        }

        Board.Dispose();

        foreach (var pickup in _pickups.Values)
        {
            pickup.Dispose();
        }
    }
}
