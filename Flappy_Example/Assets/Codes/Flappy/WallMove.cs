using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @fvoneuw
public class WallMove : MonoBehaviour
{
    public float speedWall;
    public int deathZone;

    private Vector3 _directionMove;

    // Start is called before the first frame update
    void Start()
    {
        // set the direction of the wall Vector3(-1,0,0)
        _directionMove = Vector3.zero;
        _directionMove.x = -speedWall;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move the object in the direction _directionMove
        transform.Translate(_directionMove);

        // when the wall is far away the screen, delete it
        if (transform.position.x <= deathZone)
        {
            Destroy(this.gameObject);
        }
    }
}
