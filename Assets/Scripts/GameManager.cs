using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S; // define the singleton
    public PlayerController playerController;

    public TimerScript timer;
    void Awake()
    {
        if (GameManager.S)
        {
            Destroy(this.gameObject);
        }
        else
        {
            S = this;
        }
    }

    /*    public void StartGame()
        {
            timer.StartTimer(4);
        }*/

    void Start()
    {
        StartCoroutine(ReadyCountdown());
    }
    IEnumerator ReadyCountdown()
    {
        // Set player isAlive to false during countdown
      
        playerController.isAlive = false;

        int count = 3;
        while (count > 0)
        {
            UIManager.instance.UpdateReadyMessage(count);
            yield return new WaitForSeconds(1);
            count--;
        }

        // Hide the message and start the game
        UIManager.instance.messageOverlay.enabled = false;
        playerController.isAlive = true;
        // Add any additional logic to officially start the game
    }

    public void EndGame()
    {

        timer.StopTimer();

    }
}
