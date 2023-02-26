using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource filip;
    [SerializeField] private AudioSource crow;

    [SerializeField] private float filipMaxTime = 90f;
    [SerializeField] private float crowMaxTime = 250f;

    private float filipTimer = 0f;
    private float crowTimer = 0f;

    private void Update()
    {
        if (filipTimer < filipMaxTime)
        {
            filipTimer += Time.deltaTime;
        }
        else
        {
            filipTimer = 0;
            filip.Play();
        }

        if (crowTimer < crowMaxTime)
        {
            crowTimer += Time.deltaTime;
        }
        else
        {
            crowTimer = 0;
            crow.Play();
        }
    }
}
