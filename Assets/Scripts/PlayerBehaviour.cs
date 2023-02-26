using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private AudioSource audioSource;

    [SerializeField] private GameObject viewSphere;
    [SerializeField] private float blinTimerUp = 5f;
    [SerializeField] private float blinTimerDown = 5f;
    private float blinFullTimer;

    private bool isBlinUp = false;
    private bool isBlinDown = false;

    private float[] sphereScale = new float[2];

    public bool isRunning = false;

    private float hInput;
    private float vInput;
    private Animator anim;

    public bool haveKey = false;

    private void Start()
    {
        sphereScale[0] = viewSphere.transform.localScale.x;
        sphereScale[1] = viewSphere.transform.localScale.y;
        anim = GetComponent<Animator>();
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            hInput = Input.GetAxis("Horizontal") * _runSpeed;
            vInput = Input.GetAxis("Vertical") * _runSpeed;
            isRunning = true;
            audioSource.pitch = 1.5f;
        }
        else
        {
            hInput = Input.GetAxis("Horizontal") * _walkSpeed;
            vInput = Input.GetAxis("Vertical") * _walkSpeed;
            isRunning = false;
            audioSource.pitch = 1f;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            anim.Play("Player_walk");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            anim.Play("Player_idle");
            audioSource.Stop();
        }

        this.transform.Translate(Vector2.right * Time.fixedDeltaTime * hInput);
        this.transform.Translate(Vector2.up * Time.fixedDeltaTime * vInput);

        if(isBlinUp)
        {
            if (blinFullTimer < blinTimerUp)
            {
                blinFullTimer += Time.deltaTime;
                viewSphere.transform.localScale += new Vector3(Time.deltaTime * 5, Time.deltaTime * 5);
            }
            else
            {
                isBlinUp = false;
                isBlinDown = true;
                blinFullTimer = 0;
            }
        }
        else if (isBlinDown)
        {
            if (blinFullTimer < blinTimerDown)
            {
                blinFullTimer += Time.deltaTime;
                viewSphere.transform.localScale -= new Vector3(Time.deltaTime * 5, Time.deltaTime * 5);
            }
            else
            {
                isBlinUp = false;
                isBlinDown = false;
                blinFullTimer = 0;
            }
        }
    }

    public float GetRunningAngle()
    {
         return Mathf.Atan2(hInput, -vInput) * 180 / Mathf.PI;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blin"))
        {
            viewSphere.transform.localScale = new Vector3(sphereScale[0], sphereScale[1]);
            Destroy(collision.gameObject);
            isBlinUp = true;
            blinFullTimer = 0;
        }
        else if (collision.CompareTag("Key"))
        {
            haveKey = true;
            Destroy(collision.gameObject);
        }
    }

    
}
