using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    [SerializeField] Transform[] keyPositions;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        transform.position = keyPositions[Random.RandomRange(0, keyPositions.Length)].position;
    }
}
