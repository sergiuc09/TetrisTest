using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board2DView : BoardView, IBoardView
{
    public event Action<int> AddLines;
    public event Action BlockFalled;
    public event Action GenerateNextBlock;
    public event Action LinesRemove;
    public event Action<Color> ColorChanged;
    public event Action GameOver;

    [SerializeField] private Color[] _colors;
    private int _indexColor = -1;

    private IObservableModified<Vector3> _observable;

    private IPieceView[,] _grid;

    public void Init(BoardData boardData, Vector3 spawnPosition, IObservableModified<Vector3> observable, int level)
    {
        Initialization(boardData, spawnPosition, level);
        _observable = observable;

        _grid = new IPieceView[_width, _height];

        ChangeBlocksColor();
    }

    public void Spawn(int index)
    {
        _currentBlock = (IBlockView)_blockPool.Get(index);
        _currentBlock.SetPosition(_spawnPosition);

        if (IsGameOver()) return;

        _currentBlock.Activate(true);
        _currentBlock.CanMove += IsValidMove;
        _currentBlock.Falled += OnBlockFalled;
        _currentBlock.ReadyFalled += OnBlockReadyFalled;
        _observable.Register((IObserverModified<Vector3>)_currentBlock);
    }

    public void OnLevelChanged(int level)
    {
        ChangeBlocksColor();

        UpdateBlocksFallTime(level);
    }

    private bool IsGameOver()
    {
        bool gameOver = false;

        for (int i = 0; i < _currentBlock.Pieces.Length; i++)
        {
            if (!IsValidMove(_currentBlock.Pieces[i].Position))
            {
                gameOver = true;
                break;
            }
        }

        if (gameOver) GameOver?.Invoke();

        return gameOver;
    }

    private bool IsValidMove(Vector3 position)
    {
        int pos_x = Mathf.RoundToInt(position.x);
        int pos_y = Mathf.RoundToInt(position.y);

        if (pos_x < 0 || pos_x >= _width ||
            pos_y < 0)
        {
            return false;
        }

        if (pos_y < _height && _grid[pos_x, pos_y] != null)
            return false;

        return true;
    }

    private void OnBlockFalled()
    {
        BlockFalled?.Invoke();
    }

    private void OnBlockReadyFalled()
    {
        AddToGrid();
        CurrentBlockReset();

        CheckForLines();
    }

    //
    private void AddToGrid()
    {
        foreach (var piece in _currentBlock.Pieces)
        {
            int pos_x = Mathf.RoundToInt(piece.Position.x);
            int pos_y = Mathf.RoundToInt(piece.Position.y);

            if (pos_x < 0 || pos_x >= _width ||
                pos_y >= _height || pos_y < 0)
                continue;

            _grid[pos_x, pos_y] = (IPieceView)_piecePool.Get();
            _grid[pos_x, pos_y].SetPosition(new Vector3(pos_x, pos_y, 0));
            _grid[pos_x, pos_y].Activate(true);

            //
            //((IColorChangeable)_grid[pos_x, pos_y])?.SetColor(((IColorGetable)piece).GetColor());
            IColorChangeable colorChangeable = (IColorChangeable)_grid[pos_x, pos_y];
            if (colorChangeable != null)
            {
                IColorGetable colorGetable = (IColorGetable)piece;
                if (colorGetable != null)
                    colorChangeable.SetColor(colorGetable.GetColor());
            }
        }
    }

    private void CurrentBlockReset()
    {
        _observable.Unregister((IObserverModified<Vector3>)_currentBlock);
        _currentBlock.CanMove -= IsValidMove;
        _currentBlock.Falled -= OnBlockFalled;
        _currentBlock.ReadyFalled -= OnBlockReadyFalled;
        _currentBlock.Activate(false);
        _currentBlock = null;
    }

    private void CheckForLines()
    {
        List<int> lines = GetLinesCompleted();
        int count = lines.Count;
        if (count > 0)
        {
            StartCoroutine(DeleteLinesCoroutine(lines, count));
        }
        else GenerateNextBlock?.Invoke();
    }

    public List<int> GetLinesCompleted()
    {
        List<int> lines = new List<int>();

        for (int i = 0; i < _height; i++)
        {
            bool has_line = true;
            for (int j = 0; j < _width; j++)
            {
                if (_grid[j, i] == null)
                {
                    has_line = false;
                    break;
                }
            }

            if (has_line) lines.Add(i);
        }

        return lines;
    }

    private void ChangeBlocksColor()
    {
        _indexColor++;
        if (_indexColor >= _colors.Length) _indexColor = 0;

        int length = _blockPool.Items.Length;
        for (int i = 0; i < length; i++)
        {
            IBlockView block = (IBlockView)_blockPool.Items[i];
            for (int j = 0; j < block.Pieces.Length; j++)
            {
                ((IColorChangeable)block.Pieces[j])?.SetColor(_colors[_indexColor]);
            }
        }

        ColorChanged?.Invoke(_colors[_indexColor]);
    }

    private void DeletePiece(int x, int y)
    {
        _grid[x, y].Activate(false);
        _grid[x, y].SetPosition(Vector3.zero);
        _grid[x, y] = null;
    }

    #region Coroutines

    private IEnumerator DeleteLinesCoroutine(List<int> lines, int count)
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);

        for (int i = 0; i < count; i++)
        {
            LinesRemove?.Invoke();

            int right = _width / 2;
            int left = right - 1;
            while (left >= 0 || right < _width)
            {
                if(left >= 0)
                {
                    DeletePiece(left, lines[i]);
                    left--;
                }

                if(right < _width)
                {
                    DeletePiece(right, lines[i]);
                    right++;
                }

                yield return wfs;
            }
            
        }

        AddLines?.Invoke(count);
        StartCoroutine(RowsDownCoroutine(lines));
    }

    private IEnumerator RowsDownCoroutine(List<int> lines)
    {
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();

        for (int i = _height - 1; i >= 0; i--)
        {
            if (lines.Contains(i))
            {
                for (int y = i; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        if (_grid[x, y] != null)
                        {
                            _grid[x, y - 1] = _grid[x, y];
                            _grid[x, y] = null;
                            _grid[x, y - 1].SetPosition(_grid[x, y - 1].Position + Vector3.down);
                        }
                    }
                }

                yield return wfeof;
            }
        }

        GenerateNextBlock?.Invoke();
    }

    #endregion

}
