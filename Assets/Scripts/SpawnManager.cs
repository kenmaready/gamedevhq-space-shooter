using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerupPrefabs;

    private float _spawnTime = 4.0f;
    private float _minXPosition = -10.0f;
    private float _maxXPosition = 10.0f;
    private float _upperBound = 8.0f;

    private bool _shouldSpawn = true;

    void Start() {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine() {
        
        while (_shouldSpawn) {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    IEnumerator SpawnPowerupRoutine() {
        while (_shouldSpawn) {
            SpawnPowerUp();
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }

    void SpawnEnemy() {
        Vector3 spawnPOS = new Vector3(Random.Range(_minXPosition, _maxXPosition),  _upperBound, 0.0f);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPOS, Quaternion.identity);
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
