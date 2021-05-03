using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public Collider colliderSwitch;

    private int numOfEnter = 0;

    public Transitions transitions;
    public Cutscenes cutscenesScript;

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
            transitions.cutscene = true;
            transitions.transistion = true;
            cutscenesScript.startCutscene = true;
        }
        else if (numOfEnter == 1)//dont think this is needed
        {
            //Player is moving to the start
            numOfEnter = 0;
            transitions.ResetCamState();
            //should be onRailsCam
            transitions.onThirdPersonCam = true;
            transitions.transistion = true;
        }
    }

}
