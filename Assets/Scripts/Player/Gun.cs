using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectPool))]
public class Gun : MonoBehaviour {


    [Header("Scriptable Object")]
    public GunSO gunSO;

    [Header("others")]
    public Transform player;
    public Transform firePoint;
    public SpriteRenderer gunSprite;
    public GameObject bulletPrefab;
    public bool canShoot = true;
    public Camera cam;
    public Rigidbody2D rb;
    public int bulletPoolSize;
    public Image reloadTimer;
    private ObjectPool bulletPool;
    private float followSpeed = 20f;
    private Vector2 mousePos;
    private int magSize;
    private int currentAmmo;
    private float fireRate;
    private float bulletSpeed;
    public float bulletDamage;
    private float bulletNumberPerShot;
    private float reloadTime;
    private bool isReloading = false;

    private void Awake() {
        InitGun();
        bulletPool = GetComponent<ObjectPool>();
    }

    private void Start() {
        bulletPool.InitPools(bulletPrefab, bulletPoolSize);
    }

    void Update() {
        Vector2 playerPos = new Vector3(player.position.x, player.position.y);
        transform.position = Vector2.MoveTowards(transform.position, playerPos, followSpeed * Time.deltaTime);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (isReloading) return;
        if (currentAmmo <= 0) {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1")) {
            Shoot();
        }
    }

    private void InitGun() {
        gunSprite.sprite = gunSO.gunSprite;
        magSize = gunSO.magSize;
        fireRate = gunSO.fireRate;
        bulletSpeed = gunSO.bulletSpeed;
        bulletNumberPerShot = gunSO.bulletNumberPerShot;
        bulletDamage = gunSO.bulletDamage;
        reloadTime = gunSO.reloadTime;
        currentAmmo = magSize;
    }

    void FixedUpdate() {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = angle;
    }

    private void Shoot() {
        if(!canShoot) return;
        Vector2 lookDir = (mousePos - rb.position);
        lookDir.Normalize();
        GameObject bullet = bulletPool.CreateObject();
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = firePoint.position;
        bullet.transform.localRotation = transform.rotation;
        bulletRb.velocity = lookDir * bulletSpeed;
        
        canShoot = false;
        currentAmmo--;
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown() {
        yield return new WaitForSeconds(1 / fireRate);
        canShoot = true;
    }

    public IEnumerator Reload() {
        canShoot = false;
        isReloading = true;
        Debug.Log("REloading");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magSize;
        canShoot = true;
        isReloading = false;
    }
}
