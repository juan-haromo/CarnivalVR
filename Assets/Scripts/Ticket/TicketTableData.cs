using UnityEngine;

[CreateAssetMenu(fileName = "New Ticket Table Data", menuName = "ScriptableObjects/TicketTableData")]
public class TicketTableData : ScriptableObject
{
    public string GameName;
    public System.Collections.Generic.List<TicketTableEntry> entries;
}

[System.Serializable]
public struct TicketTableEntry
{
    public int ticketAmount;
    public int scoreThreshold;
}