using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRocket : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerRocket>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 posCam = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        transform.position = posCam;
    }
}
