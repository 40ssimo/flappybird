using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public Text space;

    void Start()
    {
        scoreText.text = "0";
        gameStart();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int score)
    {
        playerScore += score;
        scoreText.text = playerScore.ToString();
        FindObjectOfType<SoundManager>().Play("Score");
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        FindObjectOfType<SoundManager>().Play("BirdDeath");

    }

    public void gameStart()
    {
        gameOverScreen.SetActive(false);
    }

    public void destroyPressSpace()
    {
        Destroy(space);
    }
}
