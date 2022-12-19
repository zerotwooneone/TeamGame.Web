namespace TeamGame.Domain.Contracts.Lobby;

public interface ILobbyService
{
    Task<bool> Join(string lobbyId, string playerId);
    bool TrySanitizeLobbyId(string input, out string lobbyId);
}