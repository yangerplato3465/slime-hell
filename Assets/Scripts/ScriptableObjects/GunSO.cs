using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun")]
public class GunSO : ScriptableObject {

    public string gunName;
    public string description;

    public int magSize;
    public float bulletDamage;
    public float bulletNumberPerShot;
    public float fireRate;
    public float bulletSpeed;
    public float reloadTime;
    public Vector2 firePoint;
    public Sprite gunSprite;

}
