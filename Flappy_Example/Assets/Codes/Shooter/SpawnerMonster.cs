using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMonster : MonoBehaviour
{
    [SerializeField]
    private GameObject monster;

    [SerializeField]
    private float minTimeSpawn;

    [SerializeField]
    private float maxTimeSpawn;

    [SerializeField]
    private float distSpawn = 5;

    public static List<GameObject> allMonsters = new List<GameObject>();

    private PlayerShooter _player;
    private bool _spawning = true;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameManagerShooter.GMS.getPlayerShooter();
        StartCoroutine(spawnMonsters());
    }

    private IEnumerator spawnMonsters()
    {
        while (_spawning)
        {
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
            // set there spawn position around the player
            Vector3 v = Random.insideUnitCircle.normalized * distSpawn;
            if (_spawning)
            {
                allMonsters.Add(Instantiate(monster, _player.transform.position + v, Quaternion.identity));
            }
        }
    }
    
    // destroy all the monsters in the level
    public void allMonstersDestroyed()
    {
        _spawning = false;
        foreach (GameObject o in allMonsters)
        {
            if(o != null)
            {
                o.GetComponent<MonsterShooter>().monsterDie();
            }
        }
        allMonsters.Clear();
    }

}
