using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @fvoneuw
public class PlayerFlappy : MonoBehaviour
{
    public float forceJump;
    public Animator anim;
    public GameObject jumpEffect;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // attribute the actual rigidbody2D in the object
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // When you press Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // reset the velocity and add the force to jump
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0,forceJump));

            // launch the animation of "jump"
            anim.SetTrigger("jump");

            // create jump effect
            Instantiate(jumpEffect, this.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When there is a collision withe the walls
        if (collision.CompareTag("Wall"))
        {
            // launch the animation "dead"
            anim.SetTrigger("dead");

            // play the method "playerDead" from the script GameManagerFlappy to stop the game
            GameManagerFlappy.GMF.playerDead();
        }
    }
}
