
public interface IPlayerModel
{
    int Score { get; }
    void UpdateScore(int level, int score);
}

public class PlayerModel : IPlayerModel
{
    private PlayerData _playerData;
    private PlayerSave _playerSave;

    public int Score => _playerData.score;

    public PlayerModel()
    {
        /*_playerSave = new PlayerSave(() =>
        {
            _playerData = _playerSave.Load();
        });*/
        _playerSave = new PlayerSave();
        _playerData = _playerSave.Load();
    }

    public void UpdateScore(int level, int score)
    {
        if (_playerData.score < score)
        {
            _playerData.score = score;
            _playerData.level_reached = level;

            _playerSave.Save(_playerData);
        }
    }
}
