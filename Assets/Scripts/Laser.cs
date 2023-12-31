using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private float _speed = 7f;

    private void Update() {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8.0) {
            Destroy(this.gameObject);
        }
    }

}
