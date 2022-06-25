using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun")]
public class GunSO : ScriptableObject {

    public string gunName;
    public string description;

    public float bulletDamage;
    public float bulletNumber;
    public float fireRate;
    public float bulletSpeed;
    public Vector2 firePoint;
    public Sprite gunSprite;

}
