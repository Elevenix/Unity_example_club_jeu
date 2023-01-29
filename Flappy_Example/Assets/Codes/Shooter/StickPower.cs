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
    private ElevatorShooter elevator;

    // Start is called before the first frame update
    void Start()
    {
        elevator = ElevatorShooter.elevator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _firstTime)
        {
            elevator.messageActivation(true);

            if (Input.GetKey(KeyCode.Space))
            {
                if(GameManagerShooter.GMS.getMoney() >= cost)
                {
                    elevator.messageActivation(false);

                    GameManagerShooter.GMS.setMoney(cost);
                    elevator.towerActivation();

                    stickAnim.SetTrigger("activation");
                    _firstTime = false;
                }
                else
                {
                    elevator.messageShake();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            elevator.messageActivation(false);
        }
    }
}
