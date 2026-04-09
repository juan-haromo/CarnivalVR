using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BuyDome : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform dome;
    [SerializeField] Collider domeCollider;
    [SerializeField] Transform productSpawnPoint;
    [SerializeField] GameObjectPool productPool;
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

        if (TicketManager.Instance.SpendTickets(domeCost))
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

    void Start()
    {
        GenerateProduct();
        costTag.text = "x " + domeCost.ToString();
    }

    XRInteractablePooledObject productInstance;
    void GenerateProduct()
    {
        productInstance = productPool.GetPooledObject() as XRInteractablePooledObject;
        productInstance.transform.SetPositionAndRotation(productSpawnPoint.position, productSpawnPoint.rotation);
        productInstance.Interactable.selectEntered.AddListener(RestockProduct);
        productInstance.SetInteractable(false);
        productInstance.gameObject.SetActive(true);
    }

    private void RestockProduct(SelectEnterEventArgs arg0)
    {
        arg0.interactableObject.selectEntered.RemoveListener(RestockProduct);
        StartCoroutine(CloseDone());
    }

    IEnumerator OpenDome()
    {
        domeCollider.enabled = false;
        while (0.01f < Vector3.Distance(dome.position, openPoint.position))
        {
            dome.position = Vector3.MoveTowards(dome.position, openPoint.position, Time.deltaTime * openCloseSpeed);
            yield return new WaitForEndOfFrame();
        }
        productInstance.SetInteractable(true);
    }

    [SerializeField] float closeWaitTime = 5;
    IEnumerator CloseDone()
    {
        yield return new WaitForSeconds(closeWaitTime);
        while(0.01f < Vector3.Distance( dome.position, closePoint.position))
        {
            dome.position = Vector3.MoveTowards(dome.position, closePoint.position, Time.deltaTime * openCloseSpeed);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2);
        domeCollider.enabled = true;
        GenerateProduct();
        hasProduct = true;
    }
}