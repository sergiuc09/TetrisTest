using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPieceView
{
    Vector3 Position { get; }
    void Activate(bool flag);
    void SetPosition(Vector3 position);

}

public interface IColorChangeable
{
    void SetColor(Color color);
}

public interface IColorGetable
{
    Color GetColor();
}

public interface ISpriteChangeable
{
    void SetSprite(Sprite sprite);
}

public interface ISpriteGetable
{
    Sprite GetSprite();
}
