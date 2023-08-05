using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

public class InteractionDriver : MonoBehaviour
{
    [System.Serializable]
    public class AxisInteractionEvent : UnityEvent<Vector2> { }

    public AxisInteractionEvent onUpdateInteraction = new AxisInteractionEvent();
    bool inInteraction = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            //if we're not in an interaction, hit-test

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                var interaction = hit.transform.GetComponent<InteractionAxis>();
                if (interaction)
                {
                    onUpdateInteraction.AddListener(interaction.Invoke);
                }
                //Debug.Log(hit.transform.name);
                Debug.Log("hit");
            }
        }
        else if(Input.GetMouseButtonUp(0)){
            //exited a mouse event
            inInteraction = false;
            //calculate momentum
            onUpdateInteraction.RemoveAllListeners();
        }
        else if (Input.GetMouseButton(0)){
            onUpdateInteraction.Invoke(new Vector2(0f, 0f));
        }
    }


}
