using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoView : BlockView, IBlockView, IObserverModified<Vector3>
{
    public event Func<Vector3, bool> CanMove;
    public event Action Falled;
    public event Action ReadyFalled;

    public IPieceView[] Pieces => _childPieces;

    [SerializeField] private Vector3 _rotationPoint;
    private float _fallTime; // = 0.8f;

    private bool _moveFaster = false;

    public void Activate(bool flag)
    {
        gameObject.SetActive(flag);

        if (flag) StartCoroutine(MoveDown());
        else transform.rotation = _rotation;
    }

    public void SetFallTime(float time)
    {
        _fallTime = time;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void OnNotify(Vector3 direction)
    {
        if (direction == Vector3.up)
            Rotate();
        else if (direction == Vector3.down)
            _moveFaster = true;
        else Move(direction);
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction;

        if(!IsValidMove())
        {
            transform.position -= direction;

            if (direction == Vector3.down)
                ReadyFalled?.Invoke();
        }
        else if(direction == Vector3.down)
        {
            transform.position += direction;
            bool isValidMove = IsValidMove();
            transform.position -= direction;

            if (!isValidMove) Falled?.Invoke();
        }

        /*if(direction == Vector3.down)
        {
            if(IsValidMove())
            {
                transform.position += direction;
                bool isValidMove = IsValidMove();
                transform.position -= direction;

                if (!isValidMove) Falled?.Invoke();
            }
            else Falled?.Invoke();
        }
        else if (!IsValidMove())
            transform.position -= direction;*/
    }

    private void Rotate()
    {
        Rotate(90);

        if (!IsValidMove())
            Rotate(-90);

        /*if(!IsValidMove())
        {
            Rotate(-180);

            if (!IsValidMove()) Rotate(90);
        }*/
    }

    private void Rotate(int angle)
    {
        transform.RotateAround(transform.TransformPoint(_rotationPoint), Vector3.forward, angle);
    }

    private bool IsValidMove()
    {
        for (int i = 0; i < _lengthChilds; i++)
        {
            if ((bool)!CanMove?.Invoke(_childPieces[i].Position))
                return false;
        }

        return true;
    }

    #region Coroutines

    private IEnumerator MoveDown()
    {
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
        float time_elapsed = 0;

        while (gameObject.activeSelf)
        {
            while (time_elapsed < (_moveFaster ? (_fallTime / 10) : _fallTime))
            {
                time_elapsed += Time.deltaTime;
                yield return wfeof;
            }

            Move(Vector3.down);
            _moveFaster = false;

            time_elapsed = 0;
        }
    }

    #endregion

}
