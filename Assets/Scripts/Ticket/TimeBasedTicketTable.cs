using UnityEngine;

public class TimeBasedTicketTable : MonoBehaviour
{
    [SerializeField] TicketTableData ticketTableData;

    public int GetTicketAmountForScore(int score)
    {
        int ticketAmount = 0;
        for(int i = 0; i < ticketTableData.entries.Count; i++)
        {
            if(score <= ticketTableData.entries[i].scoreThreshold)
            {
                ticketAmount = ticketTableData.entries[i].ticketAmount;
                return ticketAmount;
            }
        }
        return ticketAmount;
    }

    void Start()
    {
        GenerateTable();
    }


    [SerializeField] TMPro.TextMeshProUGUI lblGameName;
    [SerializeField] TMPro.TextMeshProUGUI lblTableDisplay;
    void GenerateTable()
    {
        lblGameName.text = ticketTableData.GameName;
        string displayText;
        displayText = "00:00 - " + IntToTime(ticketTableData.entries[0].scoreThreshold) + " = " + ticketTableData.entries[0].ticketAmount + "\n";
        for(int i = 1; i < ticketTableData.entries.Count; i++)
        {
            displayText += IntToTime(ticketTableData.entries[i - 1].scoreThreshold + 1) 
                + " - " +  IntToTime(ticketTableData.entries[i].scoreThreshold) + " = " + ticketTableData.entries[i].ticketAmount + "\n";
        }
        displayText += IntToTime(ticketTableData.entries[^1].scoreThreshold + 1) + " - 99:99 = 0";
        lblTableDisplay.text = displayText;
    }

    string IntToTime(int time)
    {
        int minutes = time / 60;
        minutes = Mathf.Clamp(minutes, 0, 99);
        int seconds = time % 60;
        return minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
}