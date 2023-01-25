using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator anim;
    private Rigidbody2D _rb;

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
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        _rb.velocity = new Vector2(x, y) * speed * Time.deltaTime;

        if(x > 0.1f || x<-0.1f || y>0.1f || y < -0.1f)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }
}
