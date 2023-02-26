using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private string[] textString;

    private void Start()
    {
        dialogPanel.SetActive(true);
        tmpText.text = "";
        for (int i = 0; i < textString[0].Length; i++)
        {
            tmpText.text += textString[0][i];
            Thread.Sleep(200);
        }
    }

    public void closeDialog()
    {
        dialogPanel.SetActive(false);
    }


}
