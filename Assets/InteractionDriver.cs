using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

//InteractionDriver - Class residing on maximum 1 object in the scene to sample mouse inputs, register listener events to the appropriate
//gimbal tools, and invoke interaction events, passing mouse coordinates as parameters.
public class InteractionDriver : MonoBehaviour
{
    [System.Serializable]
    public class AxisInteractionEvent : UnityEvent<Vector2> { }
    public class InteractionStateEvent : UnityEvent<bool> { }

    public AxisInteractionEvent onUpdateInteraction = new AxisInteractionEvent();
    public InteractionStateEvent onUpdateState = new InteractionStateEvent();
    bool inInteraction = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly check for mouse clicks
        if (Input.GetMouseButtonDown(0)){
            //if we're not in an interaction, hit-test

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                //check whether we clicked on an InteractionAxis (gimbal tool)
                var interaction = hit.transform.GetComponent<InteractionAxis>();
                if (interaction)
                {
                    //Add the gimbal tool as a listerner to our broadcasts
                    onUpdateInteraction.AddListener(interaction.Invoke);
                    //Initialize the interaction and save the gimbal tool states
                    interaction.sPoint = Input.mousePosition;
                    //interaction.stateUpdate(true);
                    //onUpdateInteraction.Invoke(Input.mousePosition); 

                    onUpdateState.AddListener(interaction.stateUpdate);
                    onUpdateState.Invoke(true);
                }
                //Debug.Log(hit.transform.name);
                //Debug.Log("hit");
            }
        }
        //Mouse release
        else if(Input.GetMouseButtonUp(0)){
            //exited a mouse event
            inInteraction = false;
            onUpdateState.Invoke(false);
            //calculate momentum - not implemented
            //Remove all InteractionAxis (gimbal tools) listerning
            onUpdateInteraction.RemoveAllListeners();
            onUpdateState.RemoveAllListeners();
            //Debug.Log("removed all listeners");
        }
        //Mouse held down
        else if (Input.GetMouseButton(0)){
            //Update our gimbal tool with the current mouse position
            onUpdateInteraction.Invoke(Input.mousePosition);
        }
    }


}
