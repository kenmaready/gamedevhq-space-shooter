using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private float _rotationSpeed = 10f;
    private int _pointValue = 5;
    private bool isDestroyed = false;

    private Player player;
    private SpawnManager _sm;

    [SerializeField] private GameObject _explosionPrefab;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) {
            Debug.LogError("player object not found.");
        }

        _sm = FindObjectOfType<SpawnManager>();
        if (_sm == null) {
            Debug.LogError("_sm (SpawnManager) is null.  No SpawnManager found.");
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -_rotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other) {
    if (isDestroyed) return;
    
    if (other.tag == "Laser") {
        Destroy(other.gameObject);
        if (player != null) {
            player.IncreaseScore(_pointValue);
        }
        this.Explode();
        }
    }

    private void Explode() {
        isDestroyed = true;
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        _sm.StartEnemyWave();
        Destroy(gameObject, 0.3f);
    }
}
