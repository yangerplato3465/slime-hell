using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private const string WALL = "Wall";
    private const string ENEMY = "Enemy";

    void OnCollisionEnter2D(Collision2D other){
        switch(other.gameObject.tag) {
            case WALL:
                // Destroy(gameObject);
                gameObject.SetActive(false);
                break;
            
            case ENEMY:
                Destroy(gameObject);
                break;
        }
   }
}
