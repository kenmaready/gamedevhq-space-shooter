using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _powerupPrefab;

    private float _spawnTime = 4.0f;
    private float _minXPosition = -10.0f;
    private float _maxXPosition = 10.0f;
    private float _upperBound = 8.0f;

    private bool _shouldSpawn = true;
    private float _powerUpPercentage = 0.2f;

    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine() {
        
        while (_shouldSpawn) {
            if (_shouldSpawn) {
                SpawnEnemy();
                if (Random.Range(0.0f, 1.0f) < _powerUpPercentage) {
                    SpawnPowerUp();
                }
            }
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    void SpawnEnemy() {
        Vector3 spawnPOS = new Vector3(Random.Range(_minXPosition, _maxXPosition),  _upperBound, 0.0f);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPOS, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
    }

    void SpawnPowerUp() {
        Vector3 spawnPOS = new Vector3(Random.Range(_minXPosition, _maxXPosition), _upperBound, 0.0f);
        GameObject newPowerUp = Instantiate(_powerupPrefab, spawnPOS, Quaternion.identity);
    }

    public void OnPlayerDeath() {
        _shouldSpawn = false;
    }
}
