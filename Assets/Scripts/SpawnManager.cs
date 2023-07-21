using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;

    private float _spawnTime = 4.0f;
    private float _minXPosition = -10.0f;
    private float _maxXPosition = 10.0f;
    private float _upperBound = 8.0f;

    private bool _shouldSpawn = true;

    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine() {
        
        while (_shouldSpawn) {
            if (_shouldSpawn) SpawnEnemy();
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    void SpawnEnemy() {
        Vector3 spawnPOS = new Vector3(Random.Range(_minXPosition, _maxXPosition),  _upperBound, 0.0f);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPOS, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
    }

    public void OnPlayerDeath() {
        _shouldSpawn = false;
    }
}
