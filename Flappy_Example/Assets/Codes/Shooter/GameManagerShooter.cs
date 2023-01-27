using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] groundObjects;

    [SerializeField]
    private int nbrObjects;

    [SerializeField]
    private float sizeGround;

    [SerializeField]
    private GameObject monster;

    [SerializeField]
    private float minTimeSpawn;

    [SerializeField]
    private float maxTimeSpawn;

    [SerializeField]
    private float distSpawn = 5;

    [SerializeField]
    private float timeEnd = 1;

    [SerializeField]
    private Animator[] heartAnims;

    [SerializeField]
    private GameObject particulePlayerDie;


    public static GameManagerShooter GMS;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        if(GMS == null)
        {
            GMS = this;
        }

        Time.timeScale = 1;

        _player = FindObjectOfType<PlayerShooter>().gameObject;
        spawnGroundObjects();

        StartCoroutine(spawnMonsters());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Shooter");
        }
        
    }

    public void StopGameShooter()
    {
        Time.timeScale = 0.5f;
        StartCoroutine(endGame());
    }

    private void spawnGroundObjects()
    {
        for(int i=0; i<nbrObjects; i++)
        {
            GameObject randObject = groundObjects[Random.Range(0, groundObjects.Length)];
            Vector3 randPos = new Vector3(Random.Range(-sizeGround, sizeGround), Random.Range(-sizeGround, sizeGround), 0);
            Instantiate(randObject, randPos, Quaternion.identity);
        }
    }

    private IEnumerator spawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
            Vector3 v = Random.insideUnitCircle.normalized * distSpawn;
            Instantiate(monster, _player.transform.position + v , Quaternion.identity);
        }
    }

    public void looseHeart(int i)
    {
        heartAnims[i].SetTrigger("loose");
    }

    IEnumerator endGame()
    {
        _player.GetComponent<PlayerShooter>().enabled = false;
        yield return new WaitForSeconds(timeEnd/2);
        Instantiate(particulePlayerDie, _player.transform.position, Quaternion.identity);
        Destroy(_player.gameObject);
        Time.timeScale = 0;
        StopAllCoroutines();
    }
}
