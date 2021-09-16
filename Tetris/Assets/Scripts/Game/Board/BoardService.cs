using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardService : IBoardService
{
    public event Action<int> LinesChanged;
    public event Action<int> ScoreChanged;
    public event Action<int> LevelChanged;

    public event Action<int> NextBlockGenerated;
    public event Action<int, int> BlockStatisticUpdate;

    private IPlayerModel _playerModel;

    private int _lines, _score, _level;

    private int _countBlocks;
    private int _nextBlock;

    public int NextBlock => _nextBlock;

    private int[] _blocksCountStatistics;

    public BoardService(IPlayerModel playerModel, int countBlocks, int level)
    {
        _playerModel = playerModel;
        _countBlocks = countBlocks;
        _level = level;

        _nextBlock = Random.Range(0, _countBlocks);

        _blocksCountStatistics = new int[_countBlocks];
        
    }

    public void AddLines(int value)
    {
        _lines += value;
        LinesChanged?.Invoke(_lines);

        int count = _lines / 10;
        count -= _level;
        if (count > 0)
            IncreaseLevel(count);
    }

    public void IncreaseScore()
    {
        _score += 20;
        ScoreChanged?.Invoke(_score);
    }

    public void GenerateNextBlockIndex()
    {
        _blocksCountStatistics[_nextBlock]++;
        BlockStatisticUpdate?.Invoke(_nextBlock, _blocksCountStatistics[_nextBlock]);

        int index = Random.Range(0, _countBlocks);
        if(index == _nextBlock)
            index = Random.Range(0, _countBlocks);

        _nextBlock = index;

        NextBlockGenerated?.Invoke(_nextBlock);
    }

    private void IncreaseLevel(int value)
    {
        _level += value;
        LevelChanged?.Invoke(_level);

        _score += 100;
        ScoreChanged?.Invoke(_score);

        //
        _playerModel.UpdateScore(_level, _score);
    }

}
