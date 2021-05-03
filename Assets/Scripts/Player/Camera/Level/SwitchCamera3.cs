using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera3 : MonoBehaviour
{

    public Collider colliderSwitch;
    private int numOfEnter = 0;
    public Transitions transitions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (numOfEnter == 0)
        {
            //Player is moving too the end of the level
            numOfEnter = 1;
            transitions.ResetCamState();
            transitions.onTopDownCam= true;
            transitions.transistion = true;
        }
        else if (numOfEnter == 1)
        {
            //Player is moving to the start
            numOfEnter = 0;
            transitions.ResetCamState();
            //should be onRailsCam
            transitions.onFirstPersonCam= true;
            transitions.transistion = true;
        }
    }

}
