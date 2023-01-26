using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform following;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(following != null)
        {
            this.transform.position = new Vector3(following.position.x, following.position.y, transform.position.z);
        }
        
    }
}
