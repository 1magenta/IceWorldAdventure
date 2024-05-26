using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI messageOverlay;
    public TextMeshProUGUI timerText;
    public Button menuButton;
    public Button quitButton;

    public TextMeshProUGUI scoreText;
    public int score = 0;
    public static UIManager instance{ get; private set; }
    void Awake()
    {
        instance = this;
        messageOverlay.enabled = false;
        UpdateScore(0);
    }
    public Image healthBar;

    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

    public void UpdateReadyMessage(int count)
    {
        messageOverlay.text = "Get Ready! " + count;
        messageOverlay.enabled = true;
    }

    public void GameOverMessage()
    {
        messageOverlay.text = "You Died, Game Over";
        messageOverlay.enabled = true;
    }

    public void TimesUpMessage()
    {
        messageOverlay.text = "Time's up! Game Over.";
        messageOverlay.enabled = true;
    }

    public void YouWinMessage()
    {
        messageOverlay.text = "Congrats! You successfully explored the whole world!";
        messageOverlay.enabled = true;
    }

    public void UpdateScore(int a)
    {
        score += a;
        scoreText.text = "Score: " + score;
    }

    public void UpdateTimerUI(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void ShowButton()
    {
        if (menuButton != null)
        {
            menuButton.gameObject.SetActive(true);
        }
           
        if (quitButton != null)
        {
            quitButton.gameObject.SetActive(true);
        }
            
    }

}
