using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject particuleTouch;

    [SerializeField]
    private bool destroyBallInTouch=true;

    [SerializeField]
    private int lifeLoosed = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // the ball collision with a monster
        if (collision.CompareTag("Monster"))
        {
            Instantiate(particuleTouch, transform.position, Quaternion.identity);
            collision.GetComponent<MonsterShooter>().setLifePoints(lifeLoosed);

            if (destroyBallInTouch)
            {
                // the ball is destroyed when it is touching a monster
                Destroy(this.gameObject);
            }
        }
    }
}
