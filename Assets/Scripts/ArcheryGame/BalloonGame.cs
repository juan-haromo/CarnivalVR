using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class BallonGame : MonoBehaviour
{
    [SerializeField] private List<Balloon> balloons;

    [SerializeField] Transform bowStartPoint;
    [SerializeField] Rigidbody bow;
    bool isPlaying = false;
    int totalBallons;

    void Awake()
    {
        foreach(Balloon b in balloons)
        {
            b.Model.SetActive(false);
        }
        bow.gameObject.SetActive(false);
        LoadScore();
        ResetCurrentScore();
    }

    public void TurnOnGame()
    {
        bow.gameObject.SetActive(false);
        bow.linearVelocity = Vector3.zero;
        bow.angularVelocity = Vector3.zero;
        bow.transform.SetPositionAndRotation(bowStartPoint.position,bowStartPoint.rotation);
        bow.gameObject.SetActive(true);
        if(isPlaying){return;}
        totalBallons = 0;
        foreach(Balloon b in balloons)
        {
            b.TurnOn();
            totalBallons++;
        }
    }

    Coroutine timerRoutine;
    public void StartGame() 
    {
        if(isPlaying){return;}
        isPlaying = true;
        foreach(Balloon b in balloons)
        {
            b.Initialize();
        }

        if(timerRoutine != null){StopCoroutine(timerRoutine);}
        timerRoutine = StartCoroutine(Timer());

        ResetCurrentScore();
    }

    public void PopBalloon()
    {
        totalBallons--;
        if(totalBallons <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        bow.linearVelocity = Vector3.zero;
        bow.angularVelocity = Vector3.zero;
        bow.transform.SetPositionAndRotation(bowStartPoint.position,bowStartPoint.rotation);
        bow.gameObject.SetActive(false);  
        RegisterHighScore();
        isPlaying = false; 
        DispenseTickets();
    }
    #region  Tickets
    [Header("Tickets")]
    [SerializeField] TimeBasedTicketTable ticketTable;
    [SerializeField] TicketMachine ticketMachine;

    void DispenseTickets()
    {
        ticketMachine.DispenseTicket(ticketTable.GetTicketAmountForScore(currentScore.time));
    }
    #endregion

    #region Score
    ArcheryScore currentScore;
    [Header("Score")]
    [SerializeField] ArcheryScoreDisplay currentScoreDisplay;
    ArcheryScore highScore;
    [SerializeField] ArcheryScoreDisplay highScoreDisplay;
    const string TIME_KEY = "archeryTime";
    const string ARROWS_KEY = "archeryArrows";


    void LoadScore()
    {
        highScore = new ArcheryScore
        {
            time = PlayerPrefs.GetInt(TIME_KEY,3599),
            arrows = PlayerPrefs.GetInt(ARROWS_KEY,999)
        };
        highScoreDisplay.UpdateDisplay(highScore);
    }

    void ResetCurrentScore()
    {
        currentScore.arrows = 0;
        currentScore.time = 0;
        currentScoreDisplay.UpdateDisplay(currentScore);   
    }
    
    void RegisterHighScore()
    {
        if(highScore.time < currentScore.time){return;}
        if(highScore.time == currentScore.time && highScore.arrows < currentScore.arrows){return;}

        highScore.time = currentScore.time;
        highScore.arrows = currentScore.arrows;

        PlayerPrefs.SetInt(TIME_KEY,highScore.time);
        PlayerPrefs.SetInt(ARROWS_KEY,highScore.arrows);

        highScoreDisplay.UpdateDisplay(highScore);
    }

    public void ShootArrow()
    {
        currentScore.arrows++;
        currentScoreDisplay.UpdateArrows(currentScore.arrows);
    }

    IEnumerator Timer()
    {
        while (isPlaying)
        {
            currentScore.time++; 
            currentScoreDisplay.UpdateTime(currentScore.time);
            yield return new WaitForSeconds(1);
            if(120 <currentScore.time){EndGame(); yield break;} // failsafe to end game after 2 minutes
        } 
    }
    #endregion
}