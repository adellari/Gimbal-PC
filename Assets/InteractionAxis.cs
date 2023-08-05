using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum axis{
    x,
    y,
    z
};

public enum interactType{
    translate,
    rotate
}

public class InteractionAxis : MonoBehaviour
{

    public axis _axis;
    public interactType _type;
    public Transform interactObject;

    public float momentumThreshold = 10000f;
    //current interaction momentum
    private float momentum;
    //the starting interaction point
    private Vector2 sPoint;
    //the current interaction point
    private Vector2 cPoint;
    //are we manipulating the interaction object
    private bool isInteract;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //InteractionDriver.onUpdateInteraction.AddListener(Invoke);
    //InteractionDriver.onUpdateInteraction.RemoveListener(Invoke);
    public void Invoke (Vector2 input)
    {
        Debug.Log("invoking...");
        cPoint = input;
    }

    //tell us when we're entering/exiting direct interaction
    public void stateUpdate(bool state)
    {
        if (state)
        {
            isInteract = true;
            sPoint = cPoint;
        } 
        else if(!state && momentum < momentumThreshold)
         isInteract = false;
         else
         //call momentum residual function
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
