using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform SpawnPosition;
    //public Block[] Blocks;
    public BlockContainer[] Blocks;
    public GameUI GameUI;
    public MessageScript Message;

    public Color[] ColorsBlocks;

    //
    private int _lengthBlocks = 0;

    private int _width = 10;
    private int _height = 20;
    private Transform[,] _grid;

    public int Width => _width;
    public int Height => _height;
    public Transform[,] Grid => _grid;

    private int _indexNext;
    private int _indexColor;

    //
    private int _lines, _score, _level;
    private int[] _blocksCount;

    private bool is_paused = false;
    private bool game_over = false;

    private void Awake()
    {
        _grid = new Transform[_width, _height];

        _lengthBlocks = Blocks.Length;
        _blocksCount = new int[_lengthBlocks];
    }

    private void Start()
    {
        _level = GameData.level;

        GameUI.SetGameType(GameData.gameType);
        GameUI.SetLines(0);
        GameUI.SetTopScore(GameData.topScore);
        GameUI.SetScore(0);
        GameUI.SetLevel(_level);

        _indexNext = Random.Range(0, _lengthBlocks);
        CreateNewBlock();

        //
        GameUI.SetBlocksColor(ColorsBlocks[_indexColor]);
    }

    private void Update()
    {
        if(PlayerInput.EnterPressed())
        {
            AudioManager.Instance.PlaySelection();
            if (game_over)
                SceneLoader.Load(Scenes.Menu);
            else if (is_paused)
            {
                is_paused = false;
                Message.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                is_paused = true;
                Message.SetPause();
                Time.timeScale = 0;
            }
        }
    }

    #region Public

    public void CreateNewBlock()
    {
        _blocksCount[_indexNext]++;
        GameUI.SetBlockCount(_indexNext, _blocksCount[_indexNext]);

        Block block = Instantiate(Blocks[_indexNext].Prefab_Block, SpawnPosition.position, Quaternion.identity);
        if (!block.Initialization(this, ColorsBlocks[_indexColor], _level))
        {
            Destroy(block.gameObject);
            //print("Game Over!!!");
            Message.SetGameover();
            return;
        }
        _indexNext = Random.Range(0, Blocks.Length);
        GameUI.SetNextBlock(Blocks[_indexNext].BlockUI, ColorsBlocks[_indexColor]);
    }

    public void ScoreIncrease()
    {
        _score += 12;
        GameUI.SetScore(_score);
    }

    //
    public void CheckForLines()
    {
        int count = 0;
        for (int i = _height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);

                count++;

                AudioManager.Instance.PlayLineRemove();
            }
        }

        if (count > 0)
        {
            _lines += count;
            GameUI.SetLines(_lines);
            if (_lines % 10 == 0)
            {
                _level++;
                GameUI.SetLevel(_level);

                if (_indexColor < ColorsBlocks.Length - 1)
                    _indexColor++;
                else _indexColor = 0;
                GameUI.SetBlocksColor(ColorsBlocks[_indexColor]);
            }

            //
            _score += count * 300;
            GameUI.SetScore(_score);
        }
    }

    private bool HasLine(int i)
    {
        for (int j = 0; j < _width; j++)
        {
            if (_grid[j, i] == null)
                return false;
        }

        return true;
    }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < _width; j++)
        {
            Destroy(_grid[j, i].gameObject);
            _grid[j, i] = null;
        }
    }

    private void RowDown(int i)
    {
        for (int k = i; k < _height; k++)
        {
            for (int j = 0; j < _width; j++)
            {
                if (_grid[j, k] != null)
                {
                    _grid[j, k - 1] = _grid[j, k];
                    _grid[j, k] = null;
                    _grid[j, k - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    #endregion

}
