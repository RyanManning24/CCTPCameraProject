using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public Collider colliderSwitch;

    private int numOfEnter = 0;

    public Transitions transitions;
    public Cutscenes cutscenesScript;
    public GameObject MainCam;

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
            cutscenesScript.cutsceneSpeed = 0.2f;
            MainCam.transform.rotation = Quaternion.Euler(0, 90, 0);
            cutscenesScript.startCutscene = true;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!cutscenesScript.startCutscene)
        {
            //Player is moving to the start
            numOfEnter = 0;
            transitions.ResetCamState();

            transitions.onThirdPersonCam = true;
            transitions.transistion = true;
        }
    }

}
