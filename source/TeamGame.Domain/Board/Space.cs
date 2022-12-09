using TeamGame.Domain.Util;

namespace TeamGame.Domain.Board;

public sealed class Space : IDisposable
{
    public readonly bool Passable;
    private readonly ObservablePropertyHelper<Team.Team?> _team;
    public ObservableProperty<Team.Team?> Team => _team.Property;
    
    private readonly ObservablePropertyHelper<Pickup.Pickup?> _pickup;
    public ObservableProperty<Pickup.Pickup?> Pickup => _pickup.Property;
    
    private readonly ObservablePropertyHelper<DropOff?> _dropOff;
    public ObservableProperty<DropOff?> DropOff => _dropOff.Property;

    private Space(
        bool passable, 
        ObservablePropertyHelper<Team.Team?> team, 
        ObservablePropertyHelper<Pickup.Pickup?> pickup, 
        ObservablePropertyHelper<DropOff?> dropOff)
    {
        Passable = passable;
        _team = team;
        _pickup = pickup;
        _dropOff = dropOff;
    }

    public static Space Create(
        bool passable,
        Team.Team? team,
        Pickup.Pickup? pickup,
        DropOff? dropOff)
    {
        return new Space(
            passable,
            ObservablePropertyHelper<Team.Team?>.Create(team),
            ObservablePropertyHelper<Pickup.Pickup?>.Create(pickup),
            ObservablePropertyHelper<DropOff?>.Create(dropOff));
    }

    private bool _isDisposed;
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        _team.Dispose();
        if (Pickup.Assignable is {HasBeenSet: true, Value: { }})
        {
            Pickup.Assignable.Value.Dispose();
        }
        _pickup.Dispose();
        _dropOff.Dispose();
    }
}