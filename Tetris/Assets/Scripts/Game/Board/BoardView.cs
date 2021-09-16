using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardView : MonoBehaviour
{
    [SerializeField] protected int _width, _height;

    protected GameObjectPool<PieceView> _piecePool;
    protected GameObjectPool<BlockView> _blockPool;

    protected Vector3 _spawnPosition;

    protected IBlockView _currentBlock;
    private float _fallTime;

    protected virtual void Initialization(BoardData boardData, Vector3 spawnPosition, int level)
    {
        _piecePool = new GameObjectPool<PieceView>(boardData.piecePrefab, _width * _height);
        _blockPool = new GameObjectPool<BlockView>(boardData.blockDB.blocksPrefabs);

        _spawnPosition = spawnPosition;

        //
        _fallTime = boardData.blockDB.fallTime;
        UpdateBlocksFallTime(level);
    }

    protected void UpdateBlocksFallTime(int level)
    {
        //_fallTime -= level * 0.025f;
        float fallTime = _fallTime - (level * 0.025f);
        if (fallTime < 0.05f) fallTime = 0.05f;

        //_fallTime = 0.05f;

        for (int i = 0; i < _blockPool.Items.Length; i++)
        {
            ((IBlockView)_blockPool.Items[i])?.SetFallTime(fallTime);
        }
    }
}
