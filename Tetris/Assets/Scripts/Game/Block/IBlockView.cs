using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBlockView
{
    event Func<Vector3, bool> CanMove;
    event Action Falled;
    event Action ReadyFalled;

    IPieceView[] Pieces { get; }

    void Activate(bool flag);
    void SetFallTime(float time);
    void SetPosition(Vector3 position);
}
