using TeamGame.Domain.Board;
using TeamGame.Domain.Token;
using TeamGame.Domain.Util;

namespace TeamGame.Domain.Team;

public sealed class TeamToken : IDisposable
{
    public readonly TokenShape Shape;
    public readonly TokenColor Color;
    private readonly ObservablePropertyHelper<BoardLocation> _location;
    public ObservableProperty<BoardLocation> Location => _location.Property;
    private readonly ObservablePropertyHelper<Pickup.Pickup?> _pickup;
    public ObservableProperty<Pickup.Pickup?> Pickup => _pickup.Property;

    private TeamToken(
        TokenShape shape, 
        TokenColor color, 
        ObservablePropertyHelper<BoardLocation> location, 
        ObservablePropertyHelper<Pickup.Pickup?> pickup)
    {
        Shape = shape;
        Color = color;
        _location = location;
        _pickup = pickup;
    }
    public static TeamToken Create(
        TokenShape shape, 
        TokenColor color, 
        BoardLocation location, 
        Pickup.Pickup? pickup)
    {
        return new TeamToken(
            shape,
            color,
            ObservablePropertyHelper<BoardLocation>.Create(location),
            ObservablePropertyHelper<Pickup.Pickup?>.Create(pickup));
    }

    private bool _isDisposed;
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        _location.Dispose();
        if (Pickup.Assignable is {HasBeenSet: true, Value: { }})
        {
            Pickup.Assignable.Value.Dispose();
        }
        _pickup.Dispose();
    }
}