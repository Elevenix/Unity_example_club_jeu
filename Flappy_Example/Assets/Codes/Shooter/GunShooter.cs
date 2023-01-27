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
    private GameObject bigBall;

    [SerializeField]
    private float timeLoadBigBall = 2;

    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private float timeReload = 1;

    [SerializeField]
    private float force = 1;

    [SerializeField]
    private Animator animGun;

    private Camera _cam;
    private Vector2 dir;
    private bool _canShoot;
    private float _timeLoading=0;
    private PlayerShooter _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerShooter>();
        _cam = FindObjectOfType<Camera>();
        _canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        dir = _cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            StartCoroutine(startLoad());
            StartCoroutine(startSlowMovement());
        }

        if (Input.GetMouseButtonUp(0) && _canShoot)
        {
            StartCoroutine(shoot(_timeLoading));
        }
    }

    private void FixedUpdate()
    {
        gun.transform.up = dir;
    }

    IEnumerator shoot(float timeLoad)
    {
        _player.normalMovementPlayer();

        animGun.SetBool("loading", false);
        animGun.SetBool("finish", false);
            
        GameObject b = ball;
        if (timeLoad>timeLoadBigBall)
        {
            b = bigBall;
        }
        _timeLoading = 0;
        b = Instantiate(b, spawnPoint.transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().AddForce(dir.normalized * force);
        Destroy(b, 5);

        _canShoot = false;
        yield return new WaitForSeconds(timeReload);
        _player.normalMovementPlayer();
        _canShoot = true;

    }

    IEnumerator startSlowMovement()
    {
        yield return new WaitForSeconds(0.2f);
        if (_canShoot)
        {
            _player.slowMovementPlayer();
        }
    }

    IEnumerator startLoad()
    {
        while (_canShoot && _timeLoading <= timeLoadBigBall)
        {
            animGun.SetBool("loading", true);
            _timeLoading += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        if(_timeLoading >= timeLoadBigBall)
        {
            animGun.SetBool("finish", true);
        }
    }
}
