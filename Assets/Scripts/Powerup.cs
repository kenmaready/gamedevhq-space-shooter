using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    private float _dropSpeed = 3.0f;
    private float _lowerBound = -4.0f;
    private float _upperBound = 8.0f;

    [SerializeField] private int _powerupID;

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
            Player player = other.transform.GetComponent<Player>();
            if (player != null) {
                switch(_powerupID) {
                    case 0: 
                        player.ActivateTripleShot();
                        break;
                    case 1:
                        player.ActivateSpeedUp();
                        break;
                    case 2:
                        player.ActivateShields();
                        break;
                    default:
                        player.ActivateTripleShot();
                        break;
                }

            }
            Destroy(gameObject);
        }
    }
}
