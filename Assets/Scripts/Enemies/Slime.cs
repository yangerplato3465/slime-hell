using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    [Header("Slime stats")]
    public float health = 100f;
    public float damage = 10f; 

    void Update() {
        if(health <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Bullet") {
            health -= 50f;
        }
    }
}
