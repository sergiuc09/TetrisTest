using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockView : MonoBehaviour
{
    protected int _lengthChilds;
    protected IPieceView[] _childPieces;

    protected Quaternion _rotation;

    protected virtual void Awake()
    {
        _rotation = transform.rotation;

        _lengthChilds = transform.childCount;
        _childPieces = new IPieceView[_lengthChilds];
        for (int i = 0; i < _lengthChilds; i++)
        {
            _childPieces[i] = (IPieceView)transform.GetChild(i).GetComponent<PieceView>();
        }
    }
}
