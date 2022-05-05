
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text redTeamScoreText, blueTeamScoreText;
    private Team winningTeam;

    [SerializeField]
    public int redTeamScore, blueTeamScore;
    public int RedScore
    {
        get { return redTeamScore; }
        set { redTeamScore = value; }
    }

    public int BlueScore
    {
        get { return blueTeamScore; }
        set { blueTeamScore = value; }
    }

    private void Start()
    {
        redTeamScoreText.text = RedScore.ToString();
        blueTeamScoreText.text = BlueScore.ToString();
    }


    //  1.09 | Events assigned 
    // 1.10 | Event listeners
    private void OnEnable()
    {
        TankGameEvents.OnGoalScoredEvent += UpdateGoalScore;
        TankGameEvents.OnGameEndedEvent += GameOver;
    }

    private void OnDisable()
    {
        TankGameEvents.OnGoalScoredEvent -= UpdateGoalScore;
        TankGameEvents.OnGameEndedEvent -= GameOver;
    }

    // OnGoalScoredEvent, we need to update the UI with the new scores 
    // and show the goal score message in the UI
    void UpdateGoalScore(Collider goalCollider)
    {
        switch (goalCollider.gameObject.GetComponent<Goal>().team)
        {
            case Team.Red:
                Debug.Log("Blue Scored");
                BlueScore++;
                UpdateUI(BlueScore, blueTeamScoreText);
                break;
            case Team.Blue:
                Debug.Log("Red Scored");
                RedScore++;
                UpdateUI(RedScore, redTeamScoreText);
                break;
            default:
                break;
        }
    }

    // What do you think this does? Lol
    void UpdateUI(int value, TMP_Text textToUpdate)
    {
        textToUpdate.text = value.ToString();
    }

    // This tells the UI who won the game then invokes the show end screen event
    void GameOver()
    {

        if (RedScore > BlueScore)
        {
            winningTeam = Team.Red;
        }
        else if (BlueScore > RedScore)
        {
            winningTeam = Team.Blue;
        }
        else
        {
            winningTeam = Team.None;
        }

        TankGameEvents.ShowEndScreenEvent?.Invoke(winningTeam);
    }


}
