using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    [Header("Slime stats")]
    public float health = 100f;

    void Update() {
        if(health <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Bullet") {
            float bulletDamage = other.gameObject.GetComponent<Bullet>().damage;
            health -= bulletDamage;
        }
    }
}
