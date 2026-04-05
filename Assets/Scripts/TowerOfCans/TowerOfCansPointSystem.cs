using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerOfCansPointSystem : MonoBehaviour
{
    [HideInInspector] public List<Cans> cans = new List<Cans>();
    List<Cans> activeCans;
    [HideInInspector] public List<GameObject> balls = new List<GameObject>();
    List<GameObject> activeBalls = new List<GameObject>();
    int ballsThrown;
    [SerializeField] float distanceThreshold = 0.5f;
    public int DroppedCans { get; private set; }  
    public TowerOfCans Game{get; set;}  
    [SerializeField] TextMeshProUGUI lblScoreText;


    void OnDrawGizmosSelected()
    {
        if (cans == null) return;
        foreach (Cans can in cans)
        {
            Gizmos.color = Vector3.Distance(can.canTransform.position, can.startPosition) < distanceThreshold ? Color.green : Color.red;
            Gizmos.DrawWireSphere(can.startPosition, distanceThreshold);
        }
    }

    public void RestartCans()
    {
        DroppedCans = 0;
        ballsThrown = 0;
        UpdateScoreDisplay();
        activeCans = new List<Cans>(cans);
        activeBalls = new List<GameObject>(balls);
    }

    public bool AllCansDropped()
    {
        for(int i = activeCans.Count - 1; i >= 0; i--)
        {
            Cans can = activeCans[i];
            if(distanceThreshold < Vector3.Distance(can.canTransform.position, can.startPosition) )
            {
                can.canTransform.gameObject.SetActive(false);
                activeCans.Remove(can);
                DroppedCans++;
                UpdateScoreDisplay();
            }
        }
        return activeCans.Count == 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(activeBalls.Contains(other.gameObject))
        {
            activeBalls.Remove(other.gameObject);
            StartCoroutine(CheckCansRoutine(other.gameObject));    
        }
    }

    IEnumerator CheckCansRoutine(GameObject ball)
    {
        
        yield return new WaitForSeconds(1);
        ballsThrown++;
        ball.SetActive(false);
        if(AllCansDropped() || balls.Count <= ballsThrown)
        {
            Game.EndGame();  
        }   
    }

    public void UpdateScoreDisplay()
    {
        lblScoreText.text = DroppedCans.ToString("D2");
    }
}

public struct Cans
{
    public Transform canTransform;
    public Vector3 startPosition;
}