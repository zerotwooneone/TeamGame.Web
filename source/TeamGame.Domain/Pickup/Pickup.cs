using TeamGame.Domain.Token;
using TeamGame.Domain.Util;

namespace TeamGame.Domain.Pickup;

public sealed class Pickup : IDisposable
{
    public readonly string Id;
    public readonly TokenShape Shape;
    public readonly TokenColor Color;
    private readonly List<string> _classes;
    public IReadOnlyList<string> Classes => _classes;
    private readonly ObservablePropertyHelper<bool> _isHeld;
    public ObservableProperty<bool> IsHeld => _isHeld.Property;

    private Pickup(
        string id, 
        TokenShape shape, 
        TokenColor color, 
        List<string> classes, 
        ObservablePropertyHelper<bool> isHeld)
    {
        Id = id;
        Shape = shape;
        Color = color;
        _classes = classes;
        _isHeld = isHeld;
    }

    public static Pickup Create(
        string id,
        TokenShape shape,
        TokenColor color,
        IEnumerable<string> classes,
        bool isHeld = false)
    {
        if (!Enum.IsDefined(typeof(TokenShape), shape))
        {
            throw new ArgumentException($"invalid shape {shape}", nameof(shape));
        }
        if (!Enum.IsDefined(typeof(TokenColor), color))
        {
            throw new ArgumentException($"invalid color {color}", nameof(color));
        }
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException($"invalid pickup id {id}", nameof(id));
        }
        var classList = classes
            .Where(c=>!string.IsNullOrWhiteSpace(c))
            .ToList();
        return new Pickup(
            id,
            shape,
            color,
            classList,
            ObservablePropertyHelper<bool>.Create(isHeld));
    }

    private bool _isDisposed;
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        _isHeld.Dispose();
    }
}