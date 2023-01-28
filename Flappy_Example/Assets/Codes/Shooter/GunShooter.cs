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
    private float timeShake = 1;
    [SerializeField]
    private float magnitudeShake = 1;

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
        // calcul to follow the mouse
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
        // follow the mouse
        gun.transform.up = dir;
    }

    // shoot a ball
    IEnumerator shoot(float timeLoad)
    {
        // player recover his speed
        _player.normalMovementPlayer();

        animGun.SetBool("loading", false);
        animGun.SetBool("finish", false);
            
        // look if you load the bigBall
        GameObject b = ball;
        if (timeLoad>timeLoadBigBall)
        {
            b = bigBall;
            // camera shake when you shoot a big ball
            StartCoroutine(CameraFollow.cam.Shake(timeShake, magnitudeShake));
        }
        _timeLoading = 0;
        b = Instantiate(b, spawnPoint.transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().AddForce(dir.normalized * force);
        // destroy the ball 5 seconds after his creation (if it doesn't touch a monster)
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

        // the bigBall has charged
        if(_timeLoading >= timeLoadBigBall)
        {
            animGun.SetBool("finish", true);
        }
    }
}
