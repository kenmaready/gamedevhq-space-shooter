using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerup : MonoBehaviour
{

    private float _dropSpeed = 3.0f;

    private float _lowerBound = -4.0f;
    private float _upperBound = 8.0f;

    void Start()
    {
        
    }

    void Update()
    {
        Drop();
    }

    void Drop() {
        transform.Translate(Vector3.down * _dropSpeed * Time.deltaTime);
        if (transform.position.y < _lowerBound) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Debug.Log("TripleShot Collected!!");
            Destroy(gameObject);
        }
    }
}
