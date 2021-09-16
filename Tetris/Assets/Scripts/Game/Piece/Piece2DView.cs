using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ColorRGB45
{

}

public class Piece2DView : PieceView, IPieceView, IColorChangeable, IColorGetable
{
    private SpriteRenderer _spriteRenderer;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Test(Color.red);
    }

    private void Test<T>(T data)
    {
        Color color = (Color)System.Convert.ChangeType(data, typeof(Color));
    }

    public void Activate(bool flag)
    {
        gameObject.SetActive(flag);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public Color GetColor()
    {
        return _spriteRenderer.color;
    }

}
