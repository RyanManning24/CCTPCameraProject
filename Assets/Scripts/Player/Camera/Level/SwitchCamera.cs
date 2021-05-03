using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{

    public Collider colliderSwitch;
    private int numOfEnter = 0;
    public Transitions transitions;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(numOfEnter == 0)
        {
            //Player is moving too the end of the level
            numOfEnter = 1;
            transitions.ResetCamState();
            transitions.onRailscam = true;
            transitions.transistion = true;
        }
        else if(numOfEnter == 1)
        {
            //Player is moving to the start
            numOfEnter = 0;
            transitions.ResetCamState();
            transitions.onThirdPersonCam = true;
            transitions.transistion = true;
        }
    }


}
