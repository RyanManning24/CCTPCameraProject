using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{

    public Transform thirdPersonCamBase;
    public Transform firstPersonCamBase;
    public Transform topDownCamBase;
    public Transform mainCamera;

    public GameObject cam;
    public GameObject player;
    public GameObject topOfPlayer;

    public bool onThirdPersonCam = true;
    public bool onFirstPersonCam = false;
    public bool onTopDownCam = false;
    public bool onRailscam = false;
    public bool cutscene = false;

    public bool transistion = false;

    public float time = 0;

    public OnRailsCam onRailsCamScript;
    public Cutscenes cutscenesScript;

    public ActiveCamPositions activeCamPositions;
    public bool stopCam = false;
    private bool moveTo = true;


    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        
        
        if(!onFirstPersonCam && onThirdPersonCam && !onRailscam && !onTopDownCam && !cutscene && transistion)
        {
            //transition into first person camera
            if(time <= 1)
            {
                time =+ 1f;
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, thirdPersonCamBase.position, time);
                transform.LookAt(topOfPlayer.transform.position);
                
            }
            

        }
        else if(onFirstPersonCam && !onThirdPersonCam && !onRailscam && !onTopDownCam && !cutscene && transistion)
        {
            if (time <= 1)
            {
                time =+ 1f;
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, firstPersonCamBase.position, time);

            }
        }
        else if(!onFirstPersonCam && !onThirdPersonCam && onTopDownCam && !onRailscam  && !cutscene  && transistion)
        {
            if (time <= 1)
            {
                time =+ 1f;
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, topDownCamBase.position, time);
                transform.LookAt(player.transform.position);
            }
        }
        else if(!onFirstPersonCam && !onThirdPersonCam && !onTopDownCam && !cutscene && onRailscam && transistion)
        {
            if(!stopCam)
            {
                onRailsCamScript.OnRailsStart();
            }
            else if(stopCam)
            {
                ResetCamState();
                onThirdPersonCam = true;
            }
            
        }
        else if(!onFirstPersonCam && !onThirdPersonCam && !onTopDownCam && !onRailscam && cutscene && transistion)
        {
            if(cutscenesScript.playCutscene())
            {
                //returns true in a cutscene is done 
                //return camera to favoured state
                
            }
            else if(!cutscenesScript.playCutscene())
            {
                //returns false then cutscene failed
                //return camera to favoured state
                
                
            }
            
        }

        cameraCurrentState();

    }

    public void cameraCurrentState()
    {
        activeCamPositions.onFirstPersonCam = onFirstPersonCam;
        activeCamPositions.onThirdPersonCam = onThirdPersonCam;
        activeCamPositions.onTopDownCam = onTopDownCam;
        activeCamPositions.onRailsCam = onRailscam;
        activeCamPositions.cutscene = cutscene;
    }

    public void ResetCamState()
    {
        onFirstPersonCam = false;
        onRailscam = false;
        onThirdPersonCam = false;
        onTopDownCam = false;
        cutscene = false;
    }

    public struct ActiveCamPositions
    {
        public bool onThirdPersonCam;
        public bool onFirstPersonCam;
        public bool onTopDownCam;
        public bool onRailsCam;
        public bool cutscene;

        
    };

}
