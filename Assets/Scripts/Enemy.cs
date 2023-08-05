using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _dropSpeed = 3.5f;

    private float _lowerBound = -4.0f;
    private float _upperBound = 8.0f;

    private int _pointValue = 10;
    private bool isDestroyed = false;
    private Player player;
    private Animator _explosionAnim;

    [SerializeField] private AudioClip explosionSFX;
    private AudioSource audioSource;

    [SerializeField] private GameObject enemyLaserPrefab;
    [SerializeField] private AudioClip enemyLaserSFX;
    private Coroutine runningRandomFireRoutine;


    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) {
            Debug.LogError("player object not found.");
        }

        _explosionAnim = GetComponent<Animator>();
        if (_explosionAnim == null) {
            Debug.LogError("_explosionAnim (Animator) is null.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            Debug.LogError("AudioSource Component not located on Enemy GameObject.");
        }

        runningRandomFireRoutine = StartCoroutine(RandomFireRoutine());
    }

    void Update()
    {
        Drop();
    }

    void Drop() {
        if (isDestroyed) return;

        transform.Translate(Vector3.down * _dropSpeed * Time.deltaTime);
        if (transform.position.y < _lowerBound) {
            transform.position = new Vector3(transform.position.x, _upperBound, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isDestroyed) return;
        
        if (other.tag == "Player") {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage();
            }
            this.Explode();
        } else if (other.tag == "Laser") {
            Destroy(other.gameObject);
            this.Explode();
        }
    }

    private void Explode() {
        isDestroyed = true;
        StopCoroutine(runningRandomFireRoutine);
        _explosionAnim.SetTrigger("OnEnemyHit");
        if (player != null) {
            player.IncreaseScore(_pointValue);
        }
        audioSource.PlayOneShot(explosionSFX, 0.3f);
        Destroy(gameObject, 2.8f);
    }

    IEnumerator RandomFireRoutine() {
        yield return new WaitForSeconds(0.5f);

        while (!isDestroyed) {
            yield return new WaitForSeconds(Random.Range(0.4f, 7.0f));
            FireLaser();
        }
    }

    private void FireLaser() {
        Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(enemyLaserSFX, 0.4f);
    }
}
