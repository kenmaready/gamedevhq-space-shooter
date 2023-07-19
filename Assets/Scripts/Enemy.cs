using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _xPosition;
    private float _minXPosition = -10.0f;
    private float _maxXPosition = 10.0f;
    private float _dropSpeed = 3.5f;

    private float _lowerBound = -4.0f;
    private float _upperBound = 8.0f;

    void Start()
    {
        transform.position = new Vector3(GetRandomX(), transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Drop();
    }

    void Drop() {
        transform.Translate(Vector3.down * _dropSpeed * Time.deltaTime);
        if (transform.position.y < _lowerBound) {
            Restart();
        }
    }

    void Restart() {
        transform.position = new Vector3(GetRandomX(), _upperBound, 0.0f);
    }

    float GetRandomX() {
        return Random.Range(_minXPosition, _maxXPosition);
    }
}
