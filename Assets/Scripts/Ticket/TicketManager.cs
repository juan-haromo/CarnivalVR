using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TicketManager : MonoBehaviour
{
    #region Singleton
    public static TicketManager Instance { get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            UpdateDisplay();
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    #region Ticket Data
    private int ticketCount = 10;
    public int TicketCount { get { return ticketCount; } }

    

    public void AddTickets(int amount)
    {
        ticketCount += amount;
        UpdateDisplay();
    }

    public bool SpendTickets(int amount)
    {
        if (ticketCount < amount)
        {
            return false;
        }
        ticketCount -= amount;
        UpdateDisplay();
        return true;
    }
    #endregion

    #region  UI
    [Header("UI")]
    [SerializeField] TextMeshProUGUI lblTicketDisplay;
    [SerializeField] Transform ticketMenu;
    void UpdateDisplay()
    {
        lblTicketDisplay.text = ticketCount.ToString();
    }
    #endregion

    #region Input
    [Header("Input")]
    [SerializeField] private InputActionReference openTicketMenuAction;
    void OnEnable()
    {
        openTicketMenuAction.action.performed += OpenTicketMenu;
        openTicketMenuAction.action.canceled += CloseTicketMenu;
        ticketMenu.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        openTicketMenuAction.action.performed -= OpenTicketMenu;
        openTicketMenuAction.action.canceled -= CloseTicketMenu;
        ticketMenu.gameObject.SetActive(false);
    }
    
    private void OpenTicketMenu(InputAction.CallbackContext context)
    {
        ticketMenu.gameObject.SetActive(true);
    }
    
    private void CloseTicketMenu(InputAction.CallbackContext context)
    {
        ticketMenu.gameObject.SetActive(false);
    }
    #endregion
}
