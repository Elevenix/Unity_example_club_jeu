using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShooter : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float dist;

    [SerializeField]
    private int lifePoints;

    [SerializeField]
    private GameObject sword;

    [SerializeField]
    private Animator animSword;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject particuleDie;

    [SerializeField]
    private Transform centerBody;

    [SerializeField]
    private LayerMask playerMask;

    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private float attackRange = 0.5f;

    private Vector2 _dir;
    private GameObject _target;
    private bool _attacking;

    // Start is called before the first frame update
    void Start()
    {
        _target = FindObjectOfType<PlayerShooter>().gameObject;
        _attacking = false;
    }

    private void Update()
    {
        if(_target == null)
        {
            _target = this.gameObject;
        }
        _dir = _target.transform.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sword.transform.up = _dir;

        if (Vector2.Distance(this.transform.position, _target.transform.position) > dist && !_attacking)
        {
            anim.SetBool("run", true);
            animSword.SetBool("attack", false);
            transform.Translate((_target.transform.position - this.transform.position).normalized * speed);
        }
        else
        {
            anim.SetBool("run", false);
            animSword.SetBool("attack", true);
        }
    }

    public void setLifePoints(int i)
    {
        lifePoints -= i;
        anim.SetTrigger("damage");
        if (lifePoints <= 0)
        {
            Instantiate(particuleDie, centerBody.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }

    public void attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerMask);
        foreach(Collider2D c in hitEnemies)
        {
            c.GetComponent<PlayerShooter>().takeDamage(this.transform);
        }
        StartCoroutine(attackDelay());
    }

    private IEnumerator attackDelay()
    {
        _attacking = true;
        yield return new WaitForSeconds(1);
        _attacking = false;
    
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
