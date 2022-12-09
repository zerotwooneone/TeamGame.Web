using TeamGame.Domain.Util;

namespace TeamGame.Domain.Round;

public sealed class Round : IDisposable
{
    public readonly int Id;
    public readonly DateTimeOffset End;
    public readonly int MaxActions;
    private readonly ObservablePropertyHelper<bool> _hasEnded;
    public ObservableProperty<bool> HasEnded => _hasEnded.Property;

    private Round(
        int id, 
        DateTimeOffset end, 
        int maxActions, 
        ObservablePropertyHelper<bool> hasEnded)
    {
        Id = id;
        End = end;
        MaxActions = maxActions;
        _hasEnded = hasEnded;
    }
    public static Round Create(
        int id, 
        DateTimeOffset end, 
        int maxActions, 
        bool hasEnded = false)
    {
        return new Round(
        id,
        end,
        maxActions,
        ObservablePropertyHelper<bool>.Create(hasEnded));
    }

    private bool _isDisposed;
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        _hasEnded.Dispose();
    }
}