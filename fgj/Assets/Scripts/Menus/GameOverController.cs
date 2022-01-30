using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public MonsterHealthComponent monsterHealthComponent;
    public PlayerHealthComponent playerHealthComponent;
    public Canvas GameOverCanvas;
    public Text scoreText;
    public Text gameOverText;
    public InputField ScoreInputField;
    public Button SendButton;
    // Start is called before the first frame update
    void Start()
    {
        GameOverCanvas.enabled = false;
        scoreText.enabled = false;
        ScoreInputField.enabled = false;
        SendButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealthComponent.currentHealth < 0)
        {
            Lose();
        }
        if (monsterHealthComponent.currentHealth < 0)
        {
            Win();
        }
    }

    public void Win()
    {
        Time.timeScale = 0f;
        GameOverCanvas.enabled = true;
        gameOverText.text = "Congratulations, you got the wine!";
        scoreText.enabled  = true;
        scoreText.text = "Score: " + ScoreManager.Instance.GetScore().ToString();
        ScoreInputField.enabled = true;
        SendButton.enabled = true;

    }

    public void Lose()
    {
        Time.timeScale = 0f;
        GameOverCanvas.enabled = true;
        gameOverText.text = "Game over, you didn't get the wine.";
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SceneElias");
    }
}
