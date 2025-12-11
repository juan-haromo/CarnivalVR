using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
namespace BehaviourTrees
{
    public class IceCream : MonoBehaviour
    {
        BehaviourTree iceCream;
        [SerializeField] List<Transform> playerHands;
        [SerializeField] List<Transform> positions;
        [SerializeField]int pity = 5;
        [SerializeField] GameObject cone;
        bool grababble;
        
        float distanceTrigger = 10;
        int currentPositionIndex =0;
        [SerializeField]XRGrabInteractable interactable;


        private void Awake()
        {
            interactable.enabled = false;
            iceCream = new BehaviourTree("IceCream");
            var foo = new Condition(() =>CheckDistance());
            Sequence avoid = new Sequence("Avoid");
            Leaf Reflexes = new Leaf("Reflexes", foo);
            var escape = new ActionStrategy(Flee);
            Leaf Move = new Leaf("Move", escape);

            avoid.AddChild(Reflexes);
            avoid.AddChild(Move);
            iceCream.AddChild(avoid);
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            iceCream.Process();
            if (Vector3.Distance(gameObject.transform.position, positions[currentPositionIndex].position)<2)
            {
                gameObject.transform.position = new Vector3(Mathf.Lerp(gameObject.transform.position.x, positions[currentPositionIndex].position.x, 1), Mathf.Lerp(gameObject.transform.position.y, positions[currentPositionIndex].position.y, 1), 0);
            }
        }

        bool CheckDistance()
        {
            if (pity <= 0)
            {
                interactable.enabled = true;
                return false;
            }
            foreach (var item in playerHands)
            {
                if (Vector3.Distance(cone.transform.position, item.position) < distanceTrigger)
                {
                    
                    
                        //move icecream
                        return true;
                    
                }
            }
            
            return false;
        }

        void Flee() 
        {
            int randomDecision = Random.Range(1, 11);
            if (randomDecision <= 2)
            {
                cone.transform.Rotate(180, 0, 0);
            }
            else 
            {
                pity -= 1;
                currentPositionIndex = Random.Range(0, positions.Count);
            }
        }
    } 
}  

