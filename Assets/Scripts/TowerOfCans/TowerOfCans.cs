using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerOfCans : MonoBehaviour
{
    [Header("Cans")]
    [SerializeField] private List<Transform> canPositions;
    private List<Rigidbody> canInstances;
    [SerializeField] private Rigidbody canPrefab;

    [Header("Balls")]
    [SerializeField] List<Transform> ballSpawnPoints;
    [SerializeField] Rigidbody ballPrefab;
    List<Rigidbody>ballsinstances;
    [SerializeField] TowerOfCansPointSystem pointSystem;
    [Header("Tickets")]
    [SerializeField] TicketMachine ticketMachine;
    [SerializeField] TicketTable ticketTable;
    bool isPlaying = true;
    

    void Start()
    {
        pointSystem.Game = this;
        canInstances = new List<Rigidbody>();
        foreach(Transform t in canPositions)
        {
            GameObject can = Instantiate(canPrefab.gameObject,t.position,canPrefab.transform.rotation);
            canInstances.Add(can.GetComponent<Rigidbody>());
            pointSystem.cans.Add(new Cans{canTransform = can.transform, startPosition = t.position});
        }
        ballsinstances = new List<Rigidbody>();
        foreach(Transform t in ballSpawnPoints)
        {
            GameObject ball = Instantiate(ballPrefab.gameObject,t.position,ballPrefab.transform.rotation);
            ballsinstances.Add(ball.GetComponent<Rigidbody>());
            pointSystem.balls.Add(ball);
        }
        pointSystem.RestartCans();
    }

    public void EndGame()
    {
        ticketMachine.DispenseTicket(ticketTable.GetTicketAmountForScore(pointSystem.DroppedCans));
        isPlaying = false;
    }
    
    public void RestartGame()
    {
        if(isPlaying){return;}
        isPlaying = true;
        Quaternion rotation = canPrefab.transform.rotation;
        for(int i = 0; i < canInstances.Count; i++)
        {
            Rigidbody instance = canInstances[i];
            instance.gameObject.SetActive(false);
            instance.linearVelocity  = Vector3.zero;
            instance.angularVelocity  = Vector3.zero;
            instance.transform.SetPositionAndRotation(canPositions[i].position, rotation);
            instance.gameObject.SetActive(true);
        }

        for(int i = 0; i < ballsinstances.Count; i++)
        {
            Rigidbody instance = ballsinstances[i];
            instance.gameObject.SetActive(false);
            instance.linearVelocity = Vector3.zero;
            instance.angularVelocity = Vector3.zero;
            instance.transform.SetPositionAndRotation(ballSpawnPoints[i].position,ballPrefab.transform.rotation);
            instance.gameObject.SetActive(true);  
        }

        pointSystem.RestartCans();
    }

}
