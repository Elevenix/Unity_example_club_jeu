using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerShooter : MonoBehaviour
{

    [SerializeField]
    private float timeEnd = 1;

    [SerializeField]
    private Animator[] heartAnims;

    [SerializeField]
    private GameObject particulePlayerDie;

    [SerializeField]
    private Animator moneyAnim;

    [SerializeField]
    private Text moneyText;

    [SerializeField]
    private Animator windowDeadAnim;

    [SerializeField]
    private Text bestMoneyText;

    public static GameManagerShooter GMS;

    private PlayerShooter _player;
    private int _money = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if(GMS == null)
        {
            GMS = this;
        }

        if (!PlayerPrefs.HasKey("shooterScore"))
        {
            PlayerPrefs.SetInt("shooterScore", 0);
        }

        Time.timeScale = 1;

        _player = FindObjectOfType<PlayerShooter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Shooter");
        }
        
    }

    // when the player is dead
    public void StopGameShooter()
    {
        // slow motion
        Time.timeScale = 0.5f;
        StartCoroutine(endGame());
    }


    public void looseHeart(int i)
    {
        heartAnims[i].SetTrigger("loose");
    }

    IEnumerator endGame()
    {
        // stop the possibility of the player to move
        playerCanMove(false);

        yield return new WaitForSeconds(timeEnd/2);

        // apply the new bestScore
        if (PlayerPrefs.GetInt("shooterScore") < _money)
        {
            PlayerPrefs.SetInt("shooterScore", _money);
        }
        bestMoneyText.text += PlayerPrefs.GetInt("shooterScore").ToString();
        bestMoneyText.text += " Your score : " + _money.ToString();

        // spawn dead player particules
        Instantiate(particulePlayerDie, _player.transform.position, Quaternion.identity);
        Destroy(_player.gameObject);
        Time.timeScale = 0;

        // show the window of dead with the best score
        windowDeadAnim.SetTrigger("dead");

        StopAllCoroutines();
    }

    public void addMoney()
    { 
        StartCoroutine(delayMoneyAnim(1));
    }

    private IEnumerator delayMoneyAnim(int add)
    {
        yield return new WaitForSeconds(0.2f);
        _money += add;
        moneyAnim.SetTrigger("addMoney");
        yield return new WaitForSeconds(0.05f);
        moneyText.text = "x " + _money.ToString();
    }

    public int getMoney()
    {
        return _money;
    }

    public void setMoney(int cost)
    {
        StartCoroutine(delayMoneyAnim(-cost));
    }

    public PlayerShooter getPlayerShooter()
    {
        return _player;
    }

    public void playerCanMove(bool moving)
    {
        _player.GetComponent<PlayerShooter>().enabled = moving;
        _player.GetComponent<GunShooter>().enabled = moving;
    }
}
