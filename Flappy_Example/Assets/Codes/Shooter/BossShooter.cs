using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField]
    private Vector2 sizeField;
    [SerializeField]
    private int lifePoints = 100;
    [SerializeField]
    private Animator animBoss;
    [SerializeField]
    private float timeEachPhase = 2.5f;

    [Header("Attack Roll")]

    [SerializeField]
    private float speedBoss = 1f;
    [SerializeField]
    private int nbrPoint = 5;
    [SerializeField]
    private float timeEnterMoving = 1f;

    [Header("Attack Monsters")]

    [SerializeField]
    private GameObject monster;
    [SerializeField]
    private int nbrMonster;
    [SerializeField]
    private float timeDoNothing = 5f;

    [Header("Attack Ground")]

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private int nbrWaves;
    [SerializeField]
    private float separationTime = 1.5f;

    private Vector3 _originalPos;
    private Vector3 _nextPoint;
    private bool _canRoll = false;
    private int _nbrPointWent = 0;
    // Start is called before the first frame update
    void Start()
    {
        _originalPos = this.transform.position;
        _nextPoint = _originalPos;

        StartCoroutine(attackRoll());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canRoll)
        {
            if (!nearNextPoint(_nextPoint))
            {
                transform.Translate(((_nextPoint - this.transform.position).normalized) * Time.deltaTime * speedBoss);
            }
            else
            {
                _canRoll = false;
                StartCoroutine(attackRoll());
            }
        }
    }

    IEnumerator attackRoll()
    {
        yield return new WaitForSeconds(timeEnterMoving);
        if(_nbrPointWent > nbrPoint)
        {
            _nbrPointWent = 0;
        }
        else if(_nbrPointWent < nbrPoint)
        {
            _nextPoint = new Vector3(Random.Range(-sizeField.x, sizeField.x), Random.Range(-sizeField.y, sizeField.y));
            _canRoll = true;
            _nbrPointWent++;
        }
        else
        {
            _nextPoint = _originalPos;
            _canRoll = true;
            _nbrPointWent++;
        }
    }

    private bool nearNextPoint(Vector3 point)
    {
        Vector3 distVector = point - this.transform.position;
        return (distVector.x < 0.5f && distVector.x > -0.5f) && (distVector.y < 0.5f && distVector.y > -0.5f);
    }

}
