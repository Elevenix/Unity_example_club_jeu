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

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Instantiate(particuleTouch, transform.position, Quaternion.identity);
            collision.GetComponent<MonsterShooter>().setLifePoints(lifeLoosed);
            if (destroyBallInTouch)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
