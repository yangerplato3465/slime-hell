using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [System.Serializable]
    public class Pool {
        public string key;
        public GameObject prefab;
        public int size;
    }

    [Header("Gun stats")]
    public float bulletForce = 20f;
    public float fireRate = 3f;
    public float damage = 50f;

    [Header("others")]
    public Transform player;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool canShoot = true;
    public Camera cam;
    public Rigidbody2D rb;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private float followSpeed = 10f;
    private Vector2 mousePos;

    private void Start() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        canShoot = false;
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown() {
        float currCountdownValue = 1f;
        while (currCountdownValue > 0) {
            yield return new WaitForSeconds(1 / fireRate);
            currCountdownValue--;
        }
        canShoot = true;
    }
}
