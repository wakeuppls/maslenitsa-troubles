using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] FieldOfView fov;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        float x = Screen.width / 2 - mousePos.x, y = Screen.height / 2 - mousePos.y;
        float length = Mathf.Sqrt(x * x + y * y);
        float angle = -Mathf.Atan2(x, y) * 180 / Mathf.PI;

        fov.SetOrigin(transform.position);
        fov.SetAngle(angle);
    }
}
