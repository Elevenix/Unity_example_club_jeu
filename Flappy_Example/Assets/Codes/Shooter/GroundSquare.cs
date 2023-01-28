using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSquare : MonoBehaviour
{
    [SerializeField]
    private GameObject square;

    [SerializeField]
    private GameObject squareLittle;

    [SerializeField]
    private float spaceBetween = 4.5f;

    [SerializeField]
    private int nbrLine;

    // Start is called before the first frame update
    void Start()
    {
        spawnSquares(square, spaceBetween, spaceBetween);
        spawnSquares(squareLittle, spaceBetween, spaceBetween*1.5f);
    }

    private void spawnSquares(GameObject go, float d, float start)
    {
        float dist = start;
        for (int i = 0; i < nbrLine; i++)
        {
            Instantiate(go, new Vector3(dist, 0, 0), Quaternion.identity);
            Instantiate(go, new Vector3(0, dist, 0), Quaternion.identity);
            Instantiate(go, new Vector3(-dist, 0, 0), Quaternion.identity);
            Instantiate(go, new Vector3(0, -dist, 0), Quaternion.identity);

            Instantiate(go, new Vector3(1, 1, 0).normalized * dist, Quaternion.identity);
            Instantiate(go, new Vector3(-1, 1, 0).normalized * dist, Quaternion.identity);
            Instantiate(go, new Vector3(1, -1, 0).normalized * dist, Quaternion.identity);
            Instantiate(go, new Vector3(-1, -1, 0).normalized * dist, Quaternion.identity);

            dist += d;
        }
    }

}
