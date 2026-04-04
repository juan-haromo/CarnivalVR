using TMPro;
using UnityEngine;

public class StickScore : MonoBehaviour
{
    private int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString("D2");
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString("D2");
    }   

    public int GetScore() => score;
}
