using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TankUIManager : MonoBehaviour
{
    public GameObject startScreen;
    public TMP_Text startText;
    public GameObject roundOverText;
    public GameObject roundOverMenu;
    public TMP_Text goalText;

    private void OnEnable()
    {
        TankGameEvents.OnPreGameEvent += PreGame;
        TankGameEvents.OnGameStartedEvent += InGame;
        // TankGameEvents.OnRoundEnededEvent += PostRound;
        TankGameEvents.ShowEndScreenEvent += ShowGameEnd;
        TankGameEvents.OnGoalScoredEvent += GoalScored;
    }

    private void OnDisable()
    {
        TankGameEvents.OnPreGameEvent -= PreGame;
        TankGameEvents.OnGameStartedEvent -= InGame;
        // TankGameEvents.OnRoundEnededEvent -= PostRound;
        TankGameEvents.ShowEndScreenEvent -= ShowGameEnd;
        TankGameEvents.OnGoalScoredEvent -= GoalScored;

    }

    /// <summary>
    /// Called to display the pregame ui
    /// </summary>
    private void PreGame()
    {
        startScreen.SetActive(true);
        StartCoroutine(ReadyMessage());
        roundOverText.SetActive(false);
        roundOverMenu.SetActive(false);
    }

    /// <summary>
    /// Called to display the ingame ui
    /// </summary>
    private void InGame()
    {
        startScreen.SetActive(false);
    }

    /// <summary>
    /// Called when the round is over
    /// </summary>
    private void PostRound(PlayerNumber playerNumber)
    {
        startScreen.SetActive(false);
    }
    void ShowGameEnd(Team winningTeam)
    {
        roundOverText.SetActive(true);
        if (winningTeam == Team.None)
        {
            roundOverText.GetComponentInChildren<TMP_Text>().text = "Draw";
        }
        else
        {
            roundOverText.GetComponentInChildren<TMP_Text>().text = winningTeam.ToString() + " Wins!";
        }

        roundOverText.GetComponent<DOTweenAnimation>().DOPlayForward();
        StartCoroutine(ShowRoundOverMenu());
    }

    IEnumerator ReadyMessage()
    {
        yield return new WaitForSeconds(1);
        startText.text = "3";
        yield return new WaitForSeconds(1);
        startText.text = "2";
        yield return new WaitForSeconds(1);
        startText.text = "1";
        yield return new WaitForSeconds(1);
        startText.text = "GO!";
    }

    void GoalScored(Collider scorer)
    {
        var teamName = scorer.gameObject.GetComponent<Goal>().team;
        switch (teamName)
        {
            case Team.Red:
                goalText.text = "Blue team goal!";
                break;
            case Team.Blue:
                goalText.text = "Red team goal!";
                break;
            default:
                break;
        }
        goalText.GetComponent<DOTweenAnimation>().DOPlayForward();
    }
    public void ResetGoalUI(GameObject obj)
    {
        obj.GetComponent<DOTweenAnimation>().DORewind();
    }

    IEnumerator ShowRoundOverMenu()
    {

        yield return new WaitForSeconds(2);
        roundOverMenu.SetActive(true);
        roundOverMenu.GetComponent<DOTweenAnimation>().DOPlayForward();
    }
}


