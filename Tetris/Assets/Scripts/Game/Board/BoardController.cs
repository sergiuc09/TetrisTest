using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardController
{
    void StartGame();
}

public class BoardController : IBoardController, IDisposable
{
    private IBoardView _view;
    private IBoardService _service;

    public BoardController(IBoardView view, IBoardService service)
    {
        _view = view;
        _service = service;

        _view.AddLines += _service.AddLines;
        _view.GenerateNextBlock += GenerateNextBlock; 
    }

    public void StartGame()
    {
        _view.Spawn(_service.NextBlock);
        _service.GenerateNextBlockIndex();
    }

    public void Dispose()
    {
        _view.AddLines -= _service.AddLines;
        _view.GenerateNextBlock -= GenerateNextBlock;
    }

    private void GenerateNextBlock()
    {
        _view.Spawn(_service.NextBlock);
        //
        _service.IncreaseScore();
        _service.GenerateNextBlockIndex();
    }

} // End Class
