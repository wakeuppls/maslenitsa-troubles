using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float _speed;

    private float hInput;
    private float vInput;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        hInput = Input.GetAxis("Horizontal") * _speed;
        vInput = Input.GetAxis("Vertical") * _speed;
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            anim.Play("Player_walk");
        }
        else
        {
            anim.Play("Player_idle");
        }
        this.transform.Translate(Vector2.right * Time.fixedDeltaTime * hInput);
        this.transform.Translate(Vector2.up * Time.fixedDeltaTime * vInput);
    }
}
