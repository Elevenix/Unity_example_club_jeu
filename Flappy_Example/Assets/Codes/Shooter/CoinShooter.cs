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
        if(_moving)
        {
            this.transform.Translate((_endPos) * distCoin * Time.deltaTime);
        }
    }

    IEnumerator delayToStop()
    {
        yield return new WaitForSeconds(delayStop);
        _moving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.GetComponent<Collider2D>().enabled = false;
            animCoin.SetTrigger("recolt");
            Destroy(this.gameObject,0.5f);
        }
    }
}
