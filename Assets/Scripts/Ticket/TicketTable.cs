using TMPro;
using UnityEngine;

public class TicketTable : MonoBehaviour
{
    [SerializeField] TicketTableData ticketTableData;

    
    public int GetTicketAmountForScore(int score)
    {
        int ticketAmount = 0;
        for(int i = 0; i < ticketTableData.entries.Count; i++)
        {
            if(ticketTableData.entries[i].scoreThreshold <= score)
            {
                ticketAmount = ticketTableData.entries[i].ticketAmount;
                return ticketAmount;
            }
        }
        return ticketAmount;
    }
    void TestTable()
    {
        for(int i = 0; i <= ticketTableData.entries[0].scoreThreshold; i++)
        {
            Debug.Log(i + " = " + GetTicketAmountForScore(i));
        }
    }

    void Start()
    {
        GenerateTable();
        TestTable();
    }

    [SerializeField] TextMeshProUGUI lblGameName;   
    [SerializeField] TextMeshProUGUI lblTableDisplay;   
    void GenerateTable()
    {
        lblGameName.text = ticketTableData.GameName;
        string displayText;
        displayText = ticketTableData.entries[0].scoreThreshold.ToString("D2") + "      = " + ticketTableData.entries[0].ticketAmount + "\n";
        for(int i = 1; i < ticketTableData.entries.Count - 1; i++)
        {
            displayText += (ticketTableData.entries[i+1].scoreThreshold - 1).ToString("D2")   
                + " - " + ticketTableData.entries[i].scoreThreshold.ToString("D2") + " = " + ticketTableData.entries[i].ticketAmount + "\n";
        }
        displayText += "00 - " + ticketTableData.entries[^1].scoreThreshold.ToString("D2") + " = " + ticketTableData.entries[^1].ticketAmount;
        lblTableDisplay.text = displayText;
    }
}
