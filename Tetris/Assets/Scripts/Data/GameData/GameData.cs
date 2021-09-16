

public class GameData
{
    public static GameData Instance { get; private set; }

    private string _gameType;
    private int _level;

    public string GameType => _gameType;
    public int Level => _level;

    public GameData()
    {
        if (Instance != null) return;

        Instance = this;
    }

    public void SetGameType(string gameType)
    {
        _gameType = gameType;
    }

    public void SetLevel(int level)
    {
        _level = level;
    }
}
