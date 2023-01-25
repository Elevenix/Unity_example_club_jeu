using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket: MonoBehaviour
{
    public Rigidbody2D leftPropulsor;
    public Rigidbody2D rightPropulsor;
    public Color rightColor;
    public Color leftColor;
    public ParticleSystem leftParticule;
    public ParticleSystem rightParticule;
    public int nbrParticule;

    public float speedForce;
    public float maxVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftPropulsor.GetComponent<SpriteRenderer>().color = leftColor;
            addForcePropulsor(leftPropulsor, leftParticule);
        }
        else
        {
            leftPropulsor.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rightPropulsor.GetComponent<SpriteRenderer>().color = rightColor;
            addForcePropulsor(rightPropulsor, rightParticule);
        }
        else
        {
            rightPropulsor.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void addForcePropulsor(Rigidbody2D rb, ParticleSystem ps)
    {
        if(rb.velocity.magnitude < maxVelocity)
        {
            ps.Emit(nbrParticule);
            Vector2 direction = rb.gameObject.transform.up;
            rb.AddForce(direction*speedForce);
            ps.Play();
        }
    }
}
