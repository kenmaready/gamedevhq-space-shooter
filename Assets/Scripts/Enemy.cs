using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _dropSpeed = 3.5f;

    private float _lowerBound = -4.0f;
    private float _upperBound = 8.0f;

    void Update()
    {
        Drop();
    }

    void Drop() {
        transform.Translate(Vector3.down * _dropSpeed * Time.deltaTime);
        if (transform.position.y < _lowerBound) {
            transform.position = new Vector3(transform.position.x, _upperBound, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage();
            }
            Destroy(gameObject);
        } else if (other.tag == "Laser") {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
