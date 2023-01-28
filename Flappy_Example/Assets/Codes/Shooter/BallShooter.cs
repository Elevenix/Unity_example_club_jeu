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
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<MonsterShooter>().setLifePoints(lifeLoosed);
        }
        Instantiate(particuleTouch, transform.position, Quaternion.identity);

        if (destroyBallInTouch)
        {
            // the ball is destroyed when it is touching a monster
            Destroy(this.gameObject);
        }
    }
}
