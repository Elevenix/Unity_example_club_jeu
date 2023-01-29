using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ElevatorShooter : MonoBehaviour
{
    [SerializeField]
    private Animator elevatorAnim;

    [SerializeField]
    private int nbrTower = 3;

    [SerializeField]
    private Animator messageTowerAnim;

    [SerializeField]
    private float delayShake = 0.3f;

    [SerializeField]
    private float magnitudeShake = 0.3f;

    [SerializeField]
    private SpawnerMonster spawnerMonster;

    public static ElevatorShooter elevator;

    // Start is called before the first frame update
    void Awake()
    {
        // singleton of ElevatorShooter
        if(elevator == null)
        {
            elevator = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("ELEVATOR");
            spawnerMonster.allMonstersDestroyed();
            this.GetComponent<Collider2D>().enabled = false;

            // player and camera stop moving, player is on the elevator
            PlayerShooter ps = GameManagerShooter.GMS.getPlayerShooter();
            ps.transform.SetParent(this.transform);
            ps.getAnimatorPlayer().SetBool("run", false);
            GameManagerShooter.GMS.playerCanMove(false);
            CameraFollow.cam.stopFollow();

            this.GetComponent<SortingGroup>().sortingLayerName = "elevator";
            elevatorAnim.SetTrigger("goUp");
        }
    }

    // activation d'une tour
    public void towerActivation()
    {
        nbrTower--;
        StartCoroutine(shakeActivation());
        if (nbrTower <= 0)
        {
            StartCoroutine(activationElevator());
        }
    }

    // affichage du message des tours
    public void messageActivation(bool b)
    {
        messageTowerAnim.SetBool("activated", b);
    }

    public void messageShake()
    {
        messageTowerAnim.SetTrigger("notEnough");
    }

    // when all the towers are activated
    IEnumerator activationElevator()
    {
        yield return new WaitForSeconds(3);
        elevatorAnim.SetTrigger("activated");
        yield return new WaitForSeconds(1.5f);
        this.GetComponent<Collider2D>().enabled = true;
    }

    // shake when thunderbolt
    IEnumerator shakeActivation()
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(CameraFollow.cam.Shake(delayShake, magnitudeShake));
    }
}
