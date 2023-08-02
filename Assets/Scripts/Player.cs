using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _lives = 3;
    private bool _frozen = false;

    [SerializeField] private GameObject laserPrefab;
    private float _reloadTime = 0.25f;
    private bool _reloading = false;
    private float _reloadingCounter = 0.0f;

    [SerializeField] private GameObject tripleShotPrefab;
    private bool _tripleShotActive = false;
    private float _tripleShotActiveTime = 5.0f;
    private IEnumerator _tripleShotDeactivationRoutine;

    private float _speedUpSpeed = 10.0f;
    private float _speedUpActiveTime = 5.0f;
    private IEnumerator _speedUpDeactivationRoutine;

    private bool _shieldsActive = false;
    private IEnumerator _shieldsDeactivationRoutine;

    private float _defaultSpeed = 6.5f;
    private float _speed;
    private float upperBound = 0f;
    private float lowerBound = -4.0f;
    private float leftBound = -10f;
    private float rightBound = 10f;

    private SpawnManager _spawnManager;
    [SerializeField] private GameObject _shieldVisualizer;
    private int _score = 0;
    private UIManager _uiManager;

    void Start()
    {
        _speed = _defaultSpeed;
        DeactivateShields();

        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null) {
            Debug.LogError("spawnManager is Null.");
        }

        _uiManager = FindAnyObjectByType<Canvas>().GetComponent<UIManager>();
        if (_uiManager == null) {
            Debug.LogError("uiManager is Null.");
        } else {
            _uiManager.UpdateScore(_score);
        };
    }

    void Update()
    {
        CalculateMovement();
        ToggleTripleShot();
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

    void ToggleTripleShot() {
        if (Input.GetKeyDown(KeyCode.T)) {
            _tripleShotActive = !_tripleShotActive;
        }
    }

    void FireLaser() {
        float fireInput = Input.GetAxis("Jump");
        
        if (fireInput > 0.1 && !_reloading) {
            _reloading = true;
            
            if (_tripleShotActive) {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            } else {
                Instantiate(laserPrefab, transform.position + (Vector3.up * 0.8f), Quaternion.identity);
            }
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
        if (_shieldsActive) {
            Debug.Log("Protected by Shields");
            DeactivateShields();
            return;
        }
        
        DecreaseLives();
        if (_lives <= 0) {
            _frozen = true;
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void IncreaseScore(int val) {
        _score += val;
        _uiManager.UpdateScore(_score);
    }

    public void DecreaseLives() {
        _lives--;
        _uiManager.UpdateLives(_lives);
    }

    public void ActivateTripleShot() {
        _tripleShotActive = true;

        // if any current routine is running to stop tripleshot, disable it
        // so that it doesn't prematurely stop the new activation:
        if (_tripleShotDeactivationRoutine != null) {
            StopCoroutine(_tripleShotDeactivationRoutine);
        }
        
        // start (new) coroutine to disable triple shot after allotted time:
        _tripleShotDeactivationRoutine = DeactivateTripleShot();
        StartCoroutine(_tripleShotDeactivationRoutine);
    }

    IEnumerator DeactivateTripleShot() {
        yield return new WaitForSeconds(_tripleShotActiveTime);
        _tripleShotActive = false;
    }

    public void ActivateSpeedUp() {
        _speed = _speedUpSpeed;

        if (_speedUpDeactivationRoutine != null) {
            StopCoroutine(_speedUpDeactivationRoutine);
        }

        _speedUpDeactivationRoutine = DeactivateSpeedUp();
        StartCoroutine(_speedUpDeactivationRoutine);
    }

    IEnumerator DeactivateSpeedUp() {
        yield return new WaitForSeconds(_speedUpActiveTime);
        _speed = _defaultSpeed;
    }

    public void ActivateShields() {
        _shieldsActive = true;
        _shieldVisualizer.SetActive(true);

// 
        // if (_shieldsDeactivationRoutine != null) {
        //     StopCoroutine(_tripleShotDeactivationRoutine);
        // }

        // _shieldsDeactivationRoutine = DeactivateShields();
        // StartCoroutine(_shieldsDeactivationRoutine);
    }

    // IEnumerator DeactivateShields() {
    //     yield return new WaitForSeconds(_shieldsActiveTime);
    //     _shieldsActive = false;
    // }
    
    void DeactivateShields() {
        _shieldsActive = false;
        _shieldVisualizer.SetActive(false);
        }
    }