using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShooter : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private int lifePoints;
    [SerializeField]
    private Animator anim;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerShooter>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate((target.transform.position - this.transform.position).normalized * speed);
    }

    public void setLifePoints()
    {
        if (lifePoints > 1)
        {
            lifePoints--;
            anim.SetTrigger("damage");
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
