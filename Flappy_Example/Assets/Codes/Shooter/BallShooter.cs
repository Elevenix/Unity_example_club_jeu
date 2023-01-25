using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject particuleStart;

    // Start is called before the first frame update
    void Start()
    {
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Instantiate(particuleStart, transform.position, Quaternion.identity);
            collision.GetComponent<MonsterShooter>().setLifePoints();
            Destroy(this.gameObject);
        }
    }
}
