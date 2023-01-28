using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinShooter : MonoBehaviour
{
    [SerializeField]
    private float delayStop = 1.2f;

    [SerializeField]
    private float distCoin;

    [SerializeField]
    private Animator animCoin;

    [SerializeField]
    private GameObject particuleRecolt;

    private Vector3 _endPos;
    private bool _moving=true;
    // Start is called before the first frame update
    void Start()
    {
        _endPos = Random.insideUnitCircle.normalized;

        if(_endPos.x > 0)
        {
            this.transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
        }

        StartCoroutine(delayToStop());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // moving on the start
        if(_moving)
        {
            this.transform.Translate((_endPos) * distCoin * Time.deltaTime);
        }
    }

    // time they are moving in the start
    IEnumerator delayToStop()
    {
        yield return new WaitForSeconds(delayStop);
        _moving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collect the coin
        if (collision.CompareTag("Player"))
        {
            this.GetComponent<Collider2D>().enabled = false;
            animCoin.SetTrigger("recolt");

            // add the coin in the money pocket
            GameManagerShooter.GMS.addMoney();

            Instantiate(particuleRecolt, animCoin.transform.position, Quaternion.identity);
            Destroy(this.gameObject,0.5f);

        }
    }
}
