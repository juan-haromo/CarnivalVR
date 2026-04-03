using UnityEngine;

public class TicketManager : MonoBehaviour
{
    public static TicketManager Instance { get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private int ticketCount = 10;
    public int TicketCount { get { return ticketCount; } }

    public void AddTickets(int amount)
    {
        ticketCount += amount;
    }

    public bool SpendTickets(int amount)
    {
        if (ticketCount < amount)
        {
            return false;
        }
        ticketCount -= amount;
        return true;
    }
}
