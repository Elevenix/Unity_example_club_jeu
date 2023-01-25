using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// @fvoneuw
public class GameManagerFlappy : MonoBehaviour
{
    public Text scoreText;
    public GameObject titleObject;
    public GameObject bestScoreObject;  
    public Text bestScoreText;
    public ParticleSystem backgroundParticule;

    public static GameManagerFlappy GMF;
    private bool _isPause;
    private bool _isDead;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        // Permit to use GMF and his functions anywhere (singleton)
        if(GMF == null)
        {
            GMF = this;
        }

        // Save the best score
        if (!PlayerPrefs.HasKey("bestScore"))
        {
            PlayerPrefs.SetInt("bestScore", 0);
        }

        // show the best score
        bestScoreText.text = PlayerPrefs.GetInt("bestScore").ToString();
        bestScoreObject.SetActive(true);

        titleObject.SetActive(true);
        score = 0;
        _isDead = false;
        pausedGame();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && _isPause && !_isDead)
        {
            resumeGame();
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isDead)
        {
            SceneManager.LoadScene("Flappy");
        }

    }

    // Stop the game
    private void pausedGame()
    {
        _isPause = true;
        Time.timeScale = 0;
    }

    // resume the game
    private void resumeGame()
    { 
        _isPause = false;
        Time.timeScale = 1;

        // hide the title and bestScore text (invisible when you play)
        bestScoreObject.SetActive(false);
        titleObject.SetActive(false);

        // start the count of the score
        StartCoroutine(scoreDelay());
    }

    // when the player is dead (called in PlayerFlappy script)
    public void playerDead()
    {
        pausedGame();
        _isDead = true;

        // change the bestScore if the actual score is bigger
        if(score > PlayerPrefs.GetInt("bestScore"))
        {
            PlayerPrefs.SetInt("bestScore", score);
        }

        // show the bestScore text
        bestScoreText.text = PlayerPrefs.GetInt("bestScore").ToString();
        bestScoreObject.SetActive(true);

        // paused the background particules
        backgroundParticule.Pause();
    }

    // Add 1 point in the score all the 0.7 seconds
    IEnumerator scoreDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.7f);
            score++;

            // update the score text when it changes
            scoreText.text = score.ToString();
        }
    }

}
