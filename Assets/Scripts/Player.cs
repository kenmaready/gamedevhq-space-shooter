using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _lives = 5;
    private bool _frozen = false;

    [SerializeField] private GameObject laserPrefab;
    private float _reloadTime = 0.25f;
    private bool _reloading = false;
    private float _reloadingCounter = 0.0f;

    private float _speed = 6.5f;
    private float upperBound = 0f;
    private float lowerBound = -4.0f;
    private float leftBound = -10f;
    private float rightBound = 10f;

    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null) {
            Debug.LogError("spawnManager is Null.");
        }
    }

    void Update()
    {
        CalculateMovement();
        FireLaser();
    }

    void CalculateMovement() {
        if (_frozen) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        // transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y > upperBound) {
            transform.position = new Vector3(transform.position.x, upperBound, transform.position.z);
        } else if (transform.position.y < lowerBound) {
            transform.position = new Vector3(transform.position.x, lowerBound, transform.position.z);
        }

        if (transform.position.x < leftBound) {
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        } else if (transform.position.x > rightBound) {
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        }
    }

    void FireLaser() {
        float fireInput = Input.GetAxis("Jump");
        
        if (fireInput > 0.1 && !_reloading) {
            _reloading = true;
            Instantiate(laserPrefab, transform.position + (Vector3.up * 0.8f), Quaternion.identity);
        }

        if (_reloading) {
            _reloadingCounter += Time.deltaTime;
            if (_reloadingCounter > _reloadTime) {
                _reloading = false;
                _reloadingCounter = 0.0f;
            }
        }
    }

    public void TakeDamage() {
        _lives--;
        if (_lives <= 0) {
            _frozen = true;
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}