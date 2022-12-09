namespace TeamGame.Domain.Team;

public sealed class Team : IDisposable
{
    public readonly string Id;
    public readonly TeamToken TeamToken;

    private Team(
        string id, 
        TeamToken teamToken)
    {
        Id = id;
        TeamToken = teamToken;
    }

    public static Team Create(
        string id,
        TeamToken teamToken)
    {
        return new Team(id, teamToken);
    }
    
    private bool _isDisposed;
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        TeamToken.Dispose();
    }
}