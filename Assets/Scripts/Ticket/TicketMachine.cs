using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TicketMachine : MonoBehaviour
{
    [SerializeField] private GameObjectPool ticketPool;
    [SerializeField] private Transform ticketSpawnPoint;
    [SerializeField] private float ticketDelta = 0.1f;
    [SerializeField] private float grabTicketDelay = 0.05f;
    [SerializeField] SoundPlayer soundPlayer;
    [SerializeField] SoundContainer grabTicketSound;
    [SerializeField] SoundContainer dispenseTicketSound;
    [SerializeField] XRSimpleInteractable interactable;
    [SerializeField] Transform grabDisplay;
    private int totalTickets;

    Stack<PooledObject> activeTickets;


    void Start()
    {
        activeTickets = new Stack<PooledObject>();
        SetInteractable(false);
    }

    private void SetInteractable(bool state)
    {
        interactable.enabled = state;
        grabDisplay.gameObject.SetActive(state);
    }

    public void DispenseTicket(int ticketCount = 1)
    {
        SetInteractable(true);
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
        SetInteractable(false);
        StartCoroutine(GrabTicketsCoroutine());
    }

    IEnumerator GrabTicketsCoroutine()
    {
        while (activeTickets.Count > 0)
        {
            soundPlayer.PlaySound(grabTicketSound);
            activeTickets.Pop().ReturnToPool();
            TicketManager.Instance.AddTickets(1);
            yield return new WaitForSeconds(grabTicketDelay); // Delay between grabbing each ticket
        }
        totalTickets = 0;
    }
}
