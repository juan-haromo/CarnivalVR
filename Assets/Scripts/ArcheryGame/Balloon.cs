using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Balloon : MonoBehaviour
{
    [SerializeField] float minMovementRange;
    [SerializeField] float maxMovementRange;
    float movementRange;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] SoundPlayer soundPlayer;
    [SerializeField] GameObject model;
    [SerializeField] ColorRandomizer colorRandomizer;
    [SerializeField] Collider balloonCollider;
    public GameObject Model => model;
    Coroutine movement;
    [SerializeField] BallonGame ballonGame;
    
    float speed;

    Vector3 initialPosition;
    float yMovement;
    float xMovement;



    void Start()
    {
        initialPosition = transform.position;
    }

    public void TurnOn()
    {
        if(movement != null){StopCoroutine(movement);}
        colorRandomizer.ChangeColor();
        transform.position = initialPosition;
        model.SetActive(true);
        balloonCollider.enabled = true;
        speed = Random.Range(minSpeed,maxSpeed);
        movementRange = Random.Range(minMovementRange,maxMovementRange);


        xMovement = Random.Range(-1,2);
        yMovement = Random.Range(-1,2);
    }
    public void Initialize()
    {
        movement = StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while(true){
            Vector3 moveDirection = new Vector3(xMovement,yMovement,0);

            transform.Translate(speed * Time.deltaTime * moveDirection);

            if(movementRange < Vector3.Distance(transform.position, initialPosition))
            {
                xMovement *= -1;
                yMovement *= -1;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void Pop()
    {
        soundPlayer.PlaySound();
        if(movement != null){StopCoroutine(movement);}
        model.SetActive(false);
        balloonCollider.enabled = false;
        ballonGame.PopBalloon();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            Destroy(other.gameObject);
            Pop();
        }
    }
}
