using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioClip explosionSFX;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            Debug.LogError("No AudioSource Component found on Explosion.");
        }

        audioSource.PlayOneShot(explosionSFX, 0.6f);
        Destroy(this.gameObject, 2.4f);
    }

}
