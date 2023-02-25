using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    public bool isRunning = false;

    private float hInput;
    private float vInput;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            hInput = Input.GetAxis("Horizontal") * _runSpeed;
            vInput = Input.GetAxis("Vertical") * _runSpeed;
            isRunning = true;
        }
        else
        {
            hInput = Input.GetAxis("Horizontal") * _walkSpeed;
            vInput = Input.GetAxis("Vertical") * _walkSpeed;
            isRunning = false;
        }
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

    public float GetRunningAngle()
    {
         return Mathf.Atan2(hInput, -vInput) * 180 / Mathf.PI;
    }
}
