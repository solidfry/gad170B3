using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankUIManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject roundOverText;


    private void OnEnable()
    {
        TankGameEvents.OnPreGameEvent += PreGame;
        TankGameEvents.OnGameStartedEvent += InGame;
        // TankGameEvents.OnRoundEnededEvent += PostRound;
        TankGameEvents.ShowEndScreenEvent += ShowGameEnd;


    }

    private void OnDisable()
    {
        TankGameEvents.OnPreGameEvent -= PreGame;
        TankGameEvents.OnGameStartedEvent -= InGame;
        // TankGameEvents.OnRoundEnededEvent -= PostRound;
        TankGameEvents.ShowEndScreenEvent -= ShowGameEnd;

    }

    /// <summary>
    /// Called to display the pregame ui
    /// </summary>
    private void PreGame()
    {
        startScreen.SetActive(true);
        roundOverText.SetActive(false);
    }

    /// <summary>
    /// Called to display the ingame ui
    /// </summary>
    private void InGame()
    {
        startScreen.SetActive(false);
        roundOverText.SetActive(false);
    }


    /// <summary>
    /// Called when the round is over
    /// </summary>
    private void PostRound(PlayerNumber playerNumber)
    {
        startScreen.SetActive(false);

        // roundOverText.GetComponentInChildren<TMP_Text>().text = "Player " + playerNumber.ToString() + " Wins!";
    }
    void ShowGameEnd(Team winningTeam)
    {
        roundOverText.SetActive(true);
        roundOverText.GetComponentInChildren<TMP_Text>().text = winningTeam.ToString() + " Wins!";
    }
}


