using UnityEngine;
using UnityEngine.UI;

public class NextBlockHUD : BaseGameHUD, IColorChangeable
{
    [SerializeField] private GameObject[] _blocksUI;

    private int _prevIndex = 0;


    private void Awake()
    {
        int length = _blocksUI.Length;
        for (int i = 1; i < length; i++)
        {
            _blocksUI[i].SetActive(false);
        }
    }

    public override void SetValue(int value)
    {
        _blocksUI[_prevIndex].SetActive(false);
        _blocksUI[value].SetActive(true);
        _prevIndex = value;
    }

    public void SetColor(Color color)
    {
        for (int i = 0; i < _blocksUI.Length; i++)
        {
            foreach (Transform child in _blocksUI[i].transform)
            {
                Image img = child.GetComponent<Image>();
                if (img != null)
                    img.color = color;
            }
        }
    }
}
