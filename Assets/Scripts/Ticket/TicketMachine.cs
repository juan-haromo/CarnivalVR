using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TicketMachine : MonoBehaviour
{
    [SerializeField] private GameObjectPool ticketPool;
    [SerializeField] private Transform ticketSpawnPoint;
    [SerializeField] private float ticketDelta = 0.1f;
    [SerializeField] private float grabTicketDelay = 0.1f;
    [SerializeField] SoundPlayer soundPlayer;
    [SerializeField] SoundContainer grabTicketSound;
    [SerializeField] SoundContainer dispenseTicketSound;
    private int totalTickets;

    Stack<PooledObject> activeTickets;


    void Start()
    {
        activeTickets = new Stack<PooledObject>();
    }

    public void DispenseTicket(int ticketCount = 1)
    {
        soundPlayer.PlaySound(dispenseTicketSound);
        for (int i = 0; i < ticketCount; i++)
        {
            SpawnTicket();
            totalTickets++;
        }
    }

    void SpawnTicket()
    {
        PooledObject ticket = ticketPool.GetPooledObject();
        Vector3 spawnPosition = ticketSpawnPoint.position;
        spawnPosition.y += totalTickets * ticketDelta; // Stack tickets slightly above each other
        ticket.transform.SetPositionAndRotation(spawnPosition, ticketSpawnPoint.rotation);
        ticket.gameObject.SetActive(true);
        activeTickets.Push(ticket);
    }

    public void GrabTickets()
    {
        StartCoroutine(GrabTicketsCoroutine());
    }

    IEnumerator GrabTicketsCoroutine()
    {
        while (activeTickets.Count > 0)
        {
            soundPlayer.PlaySound(grabTicketSound);
            activeTickets.Pop().ReturnToPool();
            yield return new WaitForSeconds(grabTicketDelay); // Delay between grabbing each ticket
        }
        totalTickets = 0;
    }

    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            DispenseTicket();   
        }

        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            GrabTickets();
        }
    }
}
