using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private float timeReload = 1;

    [SerializeField]
    private float force = 1;

    private Camera _cam;
    private Vector2 dir;
    private bool _canShoot;
    // Start is called before the first frame update
    void Start()
    {
        _cam = FindObjectOfType<Camera>();
        _canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        dir = _cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (Input.GetMouseButton(0) && _canShoot)
        {
            StartCoroutine(shoot());
        }
    }

    private void FixedUpdate()
    {
        gun.transform.up = dir;
    }

    IEnumerator shoot()
    {
        GameObject b = Instantiate(ball, spawnPoint.transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().AddForce(dir.normalized * force);
        Destroy(b, 5);
        _canShoot = false;
        yield return new WaitForSeconds(timeReload);
        _canShoot = true;
    }
}
