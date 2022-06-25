using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class Gun : MonoBehaviour {


    [Header("Scriptable Object")]
    public GunSO gun;

    [Header("others")]
    public Transform player;
    public Transform firePoint;
    public SpriteRenderer gunSprite;
    public GameObject bulletPrefab;
    public bool canShoot = true;
    public Camera cam;
    public Rigidbody2D rb;
    public int bulletPoolSize;
    private ObjectPool bulletPool;
    private float followSpeed = 20f;
    private Vector2 mousePos;

    private void Awake() {
        gunSprite.sprite = gun.gunSprite;
        bulletPool = GetComponent<ObjectPool>();
    }

    private void Start() {
        bulletPool.InitPools(bulletPrefab, bulletPoolSize);
    }

    void Update() {
        Vector2 playerPos = new Vector3(player.position.x, player.position.y);
        transform.position = Vector2.MoveTowards(transform.position, playerPos, followSpeed * Time.deltaTime);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1")) {
            Shoot();
        }
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
        bulletRb.velocity = lookDir * gun.bulletSpeed;
        
        canShoot = false;
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown() {
        float currCountdownValue = 1f;
        while (currCountdownValue > 0) {
            yield return new WaitForSeconds(1 / gun.fireRate);
            currCountdownValue--;
        }
        canShoot = true;
    }
}
