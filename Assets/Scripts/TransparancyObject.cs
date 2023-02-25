using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparancyObject : MonoBehaviour
{
    private bool insideObject;
    private Transform tranformPlayer;


    private void Start()
    {
        tranformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            insideObject = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            insideObject = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            insideObject = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            insideObject = false;
    }

    private void Update()
    {
        if (insideObject)
        {
            if (transform.position.y >= tranformPlayer.GetComponent<BoxCollider2D>().bounds.min.y)
            {
                gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("ObjBottom");
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Top");
            }
        }
    }

}
