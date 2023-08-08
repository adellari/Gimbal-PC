using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InteractionObject - Class residing on the object being controlled, with references to the gimbal tools that govern it.
//Its function is to correctly orient the gimbal tools relative to its transform.
public class InteractionObject : MonoBehaviour
{
    
    //keep track of which scope we're controling our object in | right now only 'local' is implemented
    public interactScope scope;
    //list of our gimbal rings
    public List<InteractionAxis> interactions;

    public bool changedScope = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //do this so all interaction axis objects ignore ZTest 
        //this is useful when the interactionObject is really big 
        //and we dont want the gimbal to get occluded.
        foreach(InteractionAxis ring in interactions)
        {
            
            Material mat;
            mat = ring.transform.GetComponent<MeshRenderer>().material;
            mat.SetInt("unity_GUIZTestMode", (int)UnityEngine.Rendering.CompareFunction.Always);
            mat = ring.transform.GetChild(0).GetComponent<MeshRenderer>().material;
            mat.SetInt("unity_GUIZTestMode", (int)UnityEngine.Rendering.CompareFunction.Always);
            mat = ring.transform.GetChild(1).GetComponent<MeshRenderer>().material;
            mat.SetInt("unity_GUIZTestMode", (int)UnityEngine.Rendering.CompareFunction.Always);
        }
    }

    //Update Orientation - Updates the orientations of our gimbal tools (rings, balls, cones)
    //Logic - We use the fact that our rings are always circled about the xz plane, and 
    //what where we know our rings should always face (relative to our interactObject (this)) to keep a consistent gimbal.
    //We update the rest of our tools accordingly, and move them to the right offsets along the appropriate axes.
    void UpdateOrientation()
    {
        // For the X Ring (Pitch) its forward is always interactObject's backwards direction
        Vector3 xRingForward = -transform.forward;
        Vector3 xRingUp = transform.right;
        var obj = interactions[0].transform;

        //make sure our x ring is correctly oriented relative to our interactObject per the above
        obj.rotation = Quaternion.LookRotation(xRingForward, xRingUp);

        //move the cone tool to the appropriate direction and distance
        obj.GetChild(0).position = obj.position + obj.up * 1.065f;
        //move the ball tool to the appropriate direction and distance
        obj.GetChild(1).position = obj.position + obj.up * 1.265f;

        // For the Y Ring (Yaw) its forward is always interactObject's forward direction
        Vector3 yRingForward = transform.forward;
        Vector3 yRingUp = transform.up;
        obj = interactions[1].transform;

        //make sure our y ring is correctly oriented relative to our interactObject per the above
        obj.rotation = Quaternion.LookRotation(yRingForward, yRingUp);

        //move the cone tool to the appropriate direction and distance
        obj.GetChild(0).position = obj.position + obj.up * 1.065f;
        //move the ball tool to the appropriate direction and distance
        obj.GetChild(1).position = obj.position + obj.up * 1.265f;

        // For the Z Ring (Roll) its forward is always interactObject's down direction
        Vector3 zRingForward = -transform.up;
        Vector3 zRingUp = transform.forward;
        obj = interactions[2].transform;

         //make sure our z ring is correctly oriented relative to our interactObject per the above
        obj.rotation = Quaternion.LookRotation(zRingForward, zRingUp);


        //move the cone tool to the appropriate direction and distance
        obj.GetChild(0).position = obj.position + obj.up * 1.065f;
        //move the ball tool to the appropriate direction and distance
        obj.GetChild(1).position = obj.position + obj.up * 1.265f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check which scope we're in and update tool orientations
        switch(scope){
            //local scope
            case interactScope.local:
            UpdateOrientation();
            break;

            //world scope - not implemented
            case interactScope.world:
            break;
        }

    }
}
