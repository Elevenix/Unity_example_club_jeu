using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSquare : MonoBehaviour
{
    public bool activateSquare = true;

    [SerializeField]
    private GameObject square;

    [SerializeField]
    private GameObject squareLittle;

    [SerializeField]
    private float spaceBetween = 4.5f;

    [SerializeField]
    private int nbrLine;

    public bool activatePlants = true;

    [SerializeField]
    private GameObject[] groundObjects;

    [SerializeField]
    private int nbrObjects;

    [SerializeField]
    private float sizeGround;

    // Start is called before the first frame update
    void Start()
    {
        if (activateSquare)
        {
            spawnSquares(square, spaceBetween, spaceBetween);
            spawnSquares(squareLittle, spaceBetween, spaceBetween * 1.5f);
        }

        if (activatePlants)
        {
            spawnGroundObjects();
        }
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

    // Spawn plants radomly on the ground
    private void spawnGroundObjects()
    {
        for (int i = 0; i < nbrObjects; i++)
        {
            GameObject randObject = groundObjects[Random.Range(0, groundObjects.Length)];
            Vector3 randPos = new Vector3(Random.Range(-sizeGround, sizeGround), Random.Range(-sizeGround, sizeGround), 0);
            Instantiate(randObject, randPos, Quaternion.identity);
        }
    }

}
