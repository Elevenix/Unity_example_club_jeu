using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPower : MonoBehaviour
{
    [SerializeField]
    private Animator stickAnim;
    [SerializeField]
    private int cost;

    private bool _firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _firstTime)
        {
            GameManagerShooter.GMS.messageActivation(true);

            if (Input.GetKey(KeyCode.Space) && GameManagerShooter.GMS.getMoney() >= cost)
            {
                GameManagerShooter.GMS.setMoney(cost);
                GameManagerShooter.GMS.towerActivation();

                stickAnim.SetTrigger("activation");
                _firstTime = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManagerShooter.GMS.messageActivation(false);
        }
    }
}
