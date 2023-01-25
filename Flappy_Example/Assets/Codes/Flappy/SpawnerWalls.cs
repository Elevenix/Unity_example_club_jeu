using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @fvoneuw
public class SpawnerWalls : MonoBehaviour
{
    public GameObject[] walls;
    public float delaySpawn = 1;
    public float rangeY;

    // Start is called before the first frame update
    void Start()
    {
        // play once in the start because the method loop in
        StartCoroutine(spawnDelay());
    }

    // IEnumerator --> permits to wait x seconds or frames
    IEnumerator spawnDelay()
    {
        // loop when WaitForSeconds is finished (so the delay) --> !!! warning, if there is no delay = crash
        while (true)
        {
            // choose the object to spawn randomly
            GameObject randomWall = walls[Random.Range(0, walls.Length)];

            // choose the position in Y randomly
            float randomY = Random.Range(-rangeY, rangeY);
            Vector3 posSpawn = new Vector3(this.transform.position.x, randomY, this.transform.position.z);

            // create the object
            Instantiate(randomWall, posSpawn, Quaternion.identity);

            // obligatory when you use an IEnumerator method
            yield return new WaitForSeconds(delaySpawn);
        }
    }
}
