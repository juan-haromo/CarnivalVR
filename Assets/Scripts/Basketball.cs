using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    int score = 0;
    int highScore;
    bool isGamePlaying;
    [SerializeField,Tooltip("Max game time in seconds")]
    int  maxTime;
    [SerializeField] TextMeshPro timer;
    [SerializeField] TextMeshPro lblHighScore;
    [SerializeField] TextMeshPro lblCurrentScore;
    static string HIGH_SCORE_KEY = "highScore";

    public List<Transform> BallStartPoints;
    public List<Rigidbody> Balls;
    [SerializeField] SoundPlayer scoreSound;
    [SerializeField] SoundPlayer finishGameSound;
    [SerializeField] TicketMachine ticketMachine;
    [SerializeField] TicketTable ticketTable;

    void Awake()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY,0);
        lblHighScore.text = highScore.ToString("D2");
        score = 0;
        lblCurrentScore.text = score.ToString("D2");
        isGamePlaying = false;
        UpdateBalls(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(!isGamePlaying){return;}
        if (other.CompareTag("Basketball"))
        {
            score++;
            scoreSound.PlaySound();
            lblCurrentScore.text = score.ToString("D2");
        }
    }

    public void StartGame()
    {
        if(isGamePlaying){return;}
        UpdateBalls(true);
        score = 0;
        lblCurrentScore.text = score.ToString("D2");
        isGamePlaying = true;
        StartCoroutine(Timer());
    }

    void EndGame()
    {
        UpdateBalls(false);
        finishGameSound.PlaySound();
        isGamePlaying = false;
        if(highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY,highScore);
            lblHighScore.text = highScore.ToString("D2");
        }
        ticketMachine.DispenseTicket(ticketTable.GetTicketAmountForScore(score));
    }
    
    IEnumerator Timer()
    {
        for(int i = maxTime; 0<=i; i--)
        {
            yield return new WaitForSeconds(1);
            timer.text = i.ToString("D2");
        }
        timer.text = "00";
        EndGame();
    }

    void UpdateBalls(bool state)
    {
        for(int i = 0; i<Balls.Count; i++)
        {
            Balls[i].gameObject.SetActive(false);
            Balls[i].useGravity = state;
            Balls[i].linearVelocity = Vector3.zero;
            Balls[i].angularVelocity = Vector3.zero;
            Balls[i].transform.position = BallStartPoints[i].position;
            Balls[i].gameObject.SetActive(state);
        }
    }
}
