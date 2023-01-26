using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwordShooter : MonoBehaviour
{
    private MonsterShooter _ms;
    // Start is called before the first frame update
    void Start()
    {
        _ms = GetComponentInParent<MonsterShooter>();
    }

    public void attackPlayer()
    {
        _ms.attack();
    }
}
