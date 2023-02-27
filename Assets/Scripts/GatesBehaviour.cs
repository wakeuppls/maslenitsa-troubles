using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GatesBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float timer;
    [SerializeField] private float maxTimer = 1.5f;
    private bool isOpening = false;

    [SerializeField] private string textString = "Сначала нужно найти ключ от ворот";
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text tmpText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.GetComponent<PlayerBehaviour>().haveKey)
            {
                GetComponent<Animator>().Play("gates");
                isOpening = true;
            }
            else
            {
                dialogPanel.SetActive(true);
                tmpText.text = "";
                for (int i = 0; i < textString.Length; i++)
                {
                    tmpText.text += textString[i];
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isOpening && timer < maxTimer)
        {
            timer += Time.fixedDeltaTime;
        }
        else if (isOpening && timer >= maxTimer)
        {
            SceneManager.LoadScene("EndGame");
            //Debug.Log("LoadScene");
        }
    }
}
