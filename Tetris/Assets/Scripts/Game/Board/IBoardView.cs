using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardView
{
    event Action<int> AddLines;
    event Action BlockFalled;
    event Action GenerateNextBlock;
    event Action LinesRemove;
    event Action<Color> ColorChanged;
    event Action GameOver;

    void Init(BoardData boardData, Vector3 spawnPosition, IObservableModified<Vector3> observable, int level);
    void Spawn(int index);

    void OnLevelChanged(int value);
}
