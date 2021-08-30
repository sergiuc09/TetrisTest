using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector3 rotationPoint;
    public float fallTime = 0.8f;

    private float _prevTime;

    private Transform[] _childs;
    private GameManager _manager;

    private int _childCount = 0;

    private void Awake()
    {
        _childCount = transform.childCount;
        _childs = new Transform[_childCount];
        for (int i = 0; i < _childCount; i++)
        {
            _childs[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        if (PlayerInput.LeftPressed())
        {
            if(CanMove(Vector3.left))
            {
                AudioManager.Instance.PlaySelection();
                transform.position += Vector3.left;
            }
        }
        else if (PlayerInput.RightPressed())
        {
            if (CanMove(Vector3.right))
            {
                AudioManager.Instance.PlaySelection();
                transform.position += Vector3.right;
            }
        }
        else if (PlayerInput.UpPressed())
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!CanMove(Vector3.zero))
            {
                //transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -180);

                if (!CanMove(Vector3.zero))
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
            else
                AudioManager.Instance.PlayBlockRotate();
        }

        //
        if (Time.time - _prevTime > (PlayerInput.DownPressing() ? fallTime / 10 : fallTime))
        {
            if (CanMove(Vector3.down))
            {
                transform.position += Vector3.down;

                if(!CanMove(Vector3.down))
                    AudioManager.Instance.PlayBlockFalled();
            }
            else
            {
                AddToGrid();
                _manager.CheckForLines();

                enabled = false;
                _manager.CreateNewBlock();

                _manager.ScoreIncrease();
            }

            _prevTime = Time.time;
        }
    }

    #region Public

    public bool Initialization(GameManager manager, Color color, int level)
    {
        _manager = manager;

        for(int i = 0; i < _childCount; i++)
        {
            _childs[i].GetComponent<SpriteRenderer>().color = color;
        }

        fallTime -= level * 0.02f;
        if (fallTime < 0.2f) fallTime = 0.2f;

        return CanMove(Vector3.zero);
    }

    #endregion

    #region Private

    private bool CanMove(Vector3 direction)
    {
        for (int i = 0; i < _childCount; i++)
        {
            Vector3 pos = _childs[i].position;
            if(direction != Vector3.zero)
                pos += direction;

            int pos_x = Mathf.RoundToInt(pos.x);
            int pos_y = Mathf.RoundToInt(pos.y);

            if (pos_x < 0 || pos_x >= _manager.Width || pos_y < 0) // || pos_y > _manager.Height)
            {
                return false;
            }

            if (pos_y < _manager.Height && _manager.Grid[pos_x, pos_y] != null)
                return false;
        }

        return true;
    }

    private void AddToGrid()
    {
        for (int i = 0; i < _childCount; i++)
        {
            Vector3 pos = _childs[i].position;
            int pos_x = Mathf.RoundToInt(pos.x);
            int pos_y = Mathf.RoundToInt(pos.y);

            _manager.Grid[pos_x, pos_y] = _childs[i];
        }
    }

    #endregion

}
