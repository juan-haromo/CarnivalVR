using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BuyDome : MonoBehaviour
{
    [Header("References")]
    public TicketManager ticketManager;
    [SerializeField] Transform dome;
    [SerializeField] XRGrabInteractable product;
    [SerializeField] private int domeCost = 5;
    [SerializeField] private TextMeshProUGUI costTag;
    [Header("Dome Positions")]
    [SerializeField] Transform closePoint;
    [SerializeField] Transform openPoint;
    [SerializeField] private float openCloseSpeed = 0.5f;
    [Header("Audio")]
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] SoundContainer buySound;
    [SerializeField] SoundContainer unableToBuySound;
    bool hasProduct = true;

    public void AttemptPurchase()
    {
        if(!hasProduct){return;}

        if (ticketManager.SpendTickets(domeCost))
        {
            hasProduct = false;
            soundPlayer.PlaySound(buySound);
            StartCoroutine(OpenDome());
        }
        else
        {
            soundPlayer.PlaySound(unableToBuySound);
        }
    }

    void Awake()
    {
        GenerateProduct();
        costTag.text = "x " + domeCost.ToString();
    }

        XRGrabInteractable productInstance;
    void GenerateProduct()
    {
        
        productInstance = Instantiate(product, dome.position, product.transform.rotation).GetComponent<XRGrabInteractable>();
        productInstance.selectEntered.AddListener(RestockProduct);
        productInstance.enabled = false;
    }

    private void RestockProduct(SelectEnterEventArgs arg0)
    {
        arg0.interactableObject.selectEntered.RemoveListener(RestockProduct);
        StartCoroutine(CloseDone());
    }

    IEnumerator OpenDome()
    {
        while (0.01f < Vector3.Distance(dome.position, openPoint.position))
        {
            dome.position = Vector3.MoveTowards(dome.position, openPoint.position, Time.deltaTime * openCloseSpeed);
            yield return new WaitForEndOfFrame();
        }
        productInstance.enabled = true;
    }

    IEnumerator CloseDone()
    {
        while(0.01f < Vector3.Distance( dome.position, closePoint.position))
        {
            dome.position = Vector3.MoveTowards(dome.position, closePoint.position, Time.deltaTime * openCloseSpeed);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2);
        GenerateProduct();
        hasProduct = true;
    }
}