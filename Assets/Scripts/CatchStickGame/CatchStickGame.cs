
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatchStickGame : MonoBehaviour
{
    [SerializeField] Stick stickPrefab;
    List<Stick> sticks;
    [SerializeField] List<Transform> stickSpawnPoints;
    bool isPlaying = false;
    [SerializeField] StickScore score;
    [SerializeField] TextMeshProUGUI countDownText;

    void Start()
    {
        SpawnRods();   
        score.ResetScore(); 
    }

    void SpawnRods()
    {
        sticks = new();
        for(int i = 0; i<stickSpawnPoints.Count; i++)
        {
            Stick instance = Instantiate(stickPrefab);
            instance.Initialize(stickSpawnPoints[i],score);
            sticks.Add(instance);
        }
    }

    public void StartGame()
    {
        if(isPlaying) return;  
        isPlaying = true;
        score.ResetScore();  
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        List<Stick> remaining = new List<Stick>(sticks);
        for(int i = 3; 0<i; i--)
        {
            countDownText.text = i.ToString("D2");
            yield return new WaitForSeconds(1);
        }
        countDownText.text = "GO";
        while (0 < remaining.Count)
        {
            yield return new WaitForSeconds(Random.Range(1.0f,1.5f));
            int index =  Random.Range(0, remaining.Count);  
            remaining[index].Release();
            remaining.RemoveAt(index);
        }
        yield return new WaitForSeconds(2);
        EndGame();
    }

    void EndGame()
    {
        foreach(Stick stick in sticks)
        {
            stick.ReturnToStart();
            stick.gameObject.SetActive(true);
        }
        countDownText.text = "00";
        isPlaying = false;
    }
}
