using System;
using UnityEngine;

public interface IBoardService
{
    event Action<int> LinesChanged;
    event Action<int> ScoreChanged;
    event Action<int> LevelChanged;
    event Action<int> NextBlockGenerated;
    event Action<int, int> BlockStatisticUpdate;

    int NextBlock { get; }

    void AddLines(int value);
    void IncreaseScore();
    void GenerateNextBlockIndex();
}
