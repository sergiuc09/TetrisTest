using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GameHUDData
{
    public BaseGameHUD GameType;
    public BaseGameHUD Lines;
    public BaseGameHUD TopScore;
    public BaseGameHUD Score;
    public BaseGameHUD Level;
    public BaseGameHUD NextBlock;
    public BaseGameHUD Statistics;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private DeviceInput _deviceInput;
    [SerializeField] private BoardView _boardView;
    [SerializeField] private BoardData _boardData;

    [SerializeField] private Transform _spawnPosition;

    [SerializeField] private GameHUDData _gameHUDData;

    [SerializeField] private GameUIData _gameUIData;

    //
    private Action OnDispose;

    private IBoardController _boardController;

    private void Awake()
    {
        IPlayerModel playerModel = new PlayerModel();

        //
        IDeviceInput deviceInput = (IDeviceInput)_deviceInput;
        PlayerInput playerInput = new PlayerInput(deviceInput);
        IBoardService boardService = new BoardService(
            playerModel, _boardData.blockDB.blocksPrefabs.Length, GameData.Instance.Level);
        IBoardView boardView = (IBoardView)_boardView;

        // HUD Set's
        _gameHUDData.GameType.SetValue(GameData.Instance.GameType);
        _gameHUDData.Level.SetValue(GameData.Instance.Level);
        _gameHUDData.TopScore.SetValue(playerModel.Score);

        boardService.LinesChanged += _gameHUDData.Lines.SetValue;
        boardService.ScoreChanged += _gameHUDData.Score.SetValue;
        boardService.LevelChanged += _gameHUDData.Level.SetValue;
        boardService.NextBlockGenerated += _gameHUDData.NextBlock.SetValue;
        boardService.BlockStatisticUpdate += _gameHUDData.Statistics.SetValue;
        boardView.ColorChanged += ((IColorChangeable)_gameHUDData.NextBlock).SetColor;
        boardView.ColorChanged += ((IColorChangeable)_gameHUDData.Statistics).SetColor;

        boardService.LevelChanged += boardView.OnLevelChanged;

        boardView.Init(_boardData, _spawnPosition.position, playerInput, GameData.Instance.Level);
        _boardController = 
            new BoardController(boardView, boardService);

        //
        GameAction gameAction = new GameAction();
        deviceInput.EnterPressed += gameAction.OnPaused;
        boardView.GameOver += gameAction.OnGameOver;

        new GameStateMachine(_gameUIData, gameAction);

        //
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        deviceInput.EnterPressed += audioManager.PlaySelection;
        deviceInput.LeftPressed += audioManager.PlaySelection;
        deviceInput.RightPressed += audioManager.PlaySelection;
        deviceInput.UpPressed += audioManager.PlayBlockRotation;

        boardView.BlockFalled += audioManager.PlayBlockFalled;
        boardView.LinesRemove += audioManager.PlayLineRemove;
        boardView.GameOver += audioManager.PlayGameOver;
        //

        OnDispose = () =>
        {
            ((IDisposable)playerInput)?.Dispose();
            ((IDisposable)_boardController)?.Dispose();

            boardService.LinesChanged -= _gameHUDData.Lines.SetValue;
            boardService.ScoreChanged -= _gameHUDData.Score.SetValue;
            boardService.LevelChanged -= _gameHUDData.Level.SetValue;
            boardService.NextBlockGenerated -= _gameHUDData.NextBlock.SetValue;
            boardService.BlockStatisticUpdate -= _gameHUDData.Statistics.SetValue;
            boardView.ColorChanged -= ((IColorChangeable)_gameHUDData.NextBlock).SetColor;
            boardView.ColorChanged -= ((IColorChangeable)_gameHUDData.Statistics).SetColor;

            boardService.LevelChanged -= boardView.OnLevelChanged;

            deviceInput.EnterPressed -= gameAction.OnPaused;
            boardView.GameOver -= gameAction.OnGameOver;

            // Audio listeners remove
            deviceInput.EnterPressed -= audioManager.PlaySelection;
            deviceInput.LeftPressed -= audioManager.PlaySelection;
            deviceInput.RightPressed -= audioManager.PlaySelection;
            deviceInput.UpPressed -= audioManager.PlayBlockRotation;

            boardView.BlockFalled -= audioManager.PlayBlockFalled;
            boardView.LinesRemove -= audioManager.PlayLineRemove;
            boardView.GameOver -= audioManager.PlayGameOver;
        };
    }

    private void Start()
    {
        _boardController.StartGame();
    }

    private void OnDisable()
    {
        OnDispose?.Invoke();
    }

}
