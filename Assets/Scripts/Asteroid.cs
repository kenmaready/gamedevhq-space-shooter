using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private float _rotationSpeed = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -_rotationSpeed * Time.deltaTime));
    }
}
