using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private int life = 3;
    [SerializeField]
    private float ejectionTime;
    [SerializeField]
    private float ejectionForce;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float sliderSpeedLoading;


    private Rigidbody2D _rb;
    private Vector2 _dir;
    private bool _ejecting = false;
    private float _percentageSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Start in the center of the screen
        this.transform.position = Vector3.zero;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.y = Input.GetAxis("Vertical");


        if(_rb.velocity.x > 0.1f 
            || _rb.velocity.x  <-0.1f 
            || _rb.velocity.y > 0.1f 
            || _rb.velocity.y < -0.1f)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }

    private void FixedUpdate()
    {
        if (!_ejecting)
        {
            _rb.velocity = _dir * speed * _percentageSpeed;
        }
    }

    public void takeDamage(Transform t)
    {
        life--;
        anim.SetTrigger("damage");
        GameManagerShooter.GMS.looseHeart(life);

        if (life <= 0)
        {
            GameManagerShooter.GMS.StopGameShooter();
            // désactive la collision pour pas créer de bug si un ennemi nous attaque en étant mort
            this.GetComponent<Collider2D>().enabled = false;
        }
        StartCoroutine(ejectDelay(t));
    }

    IEnumerator ejectDelay(Transform t)
    {
        _ejecting = true;
        _rb.AddForce((this.transform.position - t.position).normalized * ejectionForce);
        yield return new WaitForSeconds(ejectionTime);
        _ejecting = false;
    }

    public void slowMovementPlayer()
    {
        _percentageSpeed = sliderSpeedLoading;
    }

    public void normalMovementPlayer()
    {
        _percentageSpeed = 1;
    }
}
