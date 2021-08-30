using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageScript : MonoBehaviour
{
    public TMP_Text TxtMessage;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetGameover()
    {
        SetMessage("GAME OVER");
        gameObject.SetActive(true);
    }

    public void SetPause()
    {
        SetMessage("PAUSE");
        gameObject.SetActive(true);
    }

    private void SetMessage(string msg)
    {
        TxtMessage.text = msg;
    }
}
