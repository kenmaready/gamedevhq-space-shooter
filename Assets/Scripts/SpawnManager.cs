using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerupPrefabs;

    private Asteroid _starterAsteroid;

    private float _spawnTime = 4.0f;
    private float _minXPosition = -10.0f;
    private float _maxXPosition = 10.0f;
    private float _upperBound = 8.0f;
    private float _lowerBound = 3.1f;

    private bool _shouldSpawn = true;

    void Start() {
        SpawnAsteroid();
    }

    void SpawnAsteroid() {
        Instantiate(_asteroidPrefab, new Vector3(Random.Range(_minXPosition, _maxXPosition - 0.5f), Random.Range(_lowerBound, _upperBound), 0.0f), Quaternion.identity);
    }

    public void StartEnemyWave() {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine() {
        yield return new WaitForSeconds(1.0f);

        while (_shouldSpawn) {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    IEnumerator SpawnPowerupRoutine() {
        yield return new WaitForSeconds(1.2f);

        while (_shouldSpawn) {
            SpawnPowerUp();
            yield return new WaitForSeconds(Random.Range(4.0f, 14.0f));
        }
    }

    void SpawnEnemy() {
        Vector3 spawnPOS = new Vector3(Random.Range(_minXPosition, _maxXPosition),  _upperBound, 0.0f);
        GameObject newEnemy = Instantiate(_enemyPrefab, spawnPOS, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
    }

    void SpawnPowerUp() {
        Vector3 spawnPOS = new Vector3(Random.Range(_minXPosition, _maxXPosition), _upperBound, 0.0f);
        int powerupSelector = Random.Range(0,_powerupPrefabs.Length);
        GameObject newPowerup = Instantiate(_powerupPrefabs[powerupSelector], spawnPOS, Quaternion.identity);
    }

    public void OnPlayerDeath() {
        _shouldSpawn = false;
    }
}
