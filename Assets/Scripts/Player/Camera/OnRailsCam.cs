using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnRailsCam : MonoBehaviour
{

    public CameraFollow cameraFollowScript;
    public Transitions transitionsScript;
    public GameObject target;
    public GameObject player;
    public GameObject mainCam;
    public GameObject camStart;
    public GameObject playerStart;


    public int noCamPositions = 4;
    public int currentCamPos = 0;

    public onRailsPos[] transitionPoints;



    [System.Serializable]
    public struct onRailsPos
    {
        public GameObject playerPosition;
        public GameObject cameraPosition;


    };

    private void Start()
    {
        
    }




    // Update is called once per frame
    void Update()
    {
        CheckNumOfPositionsLeft();
        
    }

    public void OnRailsStart()
    {

        //Check if their are any points for the camera to move for
            
        if(transitionPoints.Length <= 0)
        {
                //no positions set flag something to the user
        }
        else
        {
            //move to all the cameras positions when the player moves (change control scheme)            
            startCamera();

            
        }


        
    }
    void startCamera()
    {
        if(!transitionsScript.stopCam)
        {
            //calculate the total distance to the next point
            float totalDistPlayer = Vector3.Distance(playerStart.transform.position, transitionPoints[currentCamPos].playerPosition.transform.position);
            float totalDistCam = Vector3.Distance(camStart.transform.position, transitionPoints[currentCamPos].cameraPosition.transform.position);

            //calculate the distance from the player/camera to the next point 
            float playerDist = Vector3.Distance(transitionPoints[currentCamPos].playerPosition.transform.position, player.transform.position);
            float CameraDist = Vector3.Distance(transitionPoints[currentCamPos].cameraPosition.transform.position, mainCam.transform.position);

            //work out where this puts them on the lerp value
            float lerpPlayer = totalDistPlayer - playerDist;
            float lerpCam = totalDistCam - CameraDist;

            // turn lerpCam to a value between 0 and 1
            lerpCam /= totalDistCam;
            lerpPlayer /= totalDistPlayer;

            if (currentCamPos == 0)
            {
                Debug.Log(lerpPlayer);
                mainCam.transform.position = Vector3.Lerp(camStart.transform.position, transitionPoints[currentCamPos].cameraPosition.transform.position, lerpPlayer);
                mainCam.transform.LookAt(player.transform.position, Vector3.up);
                if (lerpPlayer >= 0.9)
                {
                    //plus camera count up and move to the next point 
                    currentCamPos++;
                }
                else if (lerpPlayer <= 0.1)
                {
                    //minus cam down
                    currentCamPos--;
                }
            }
            else if (currentCamPos >= 1)
            {
                Debug.Log(lerpPlayer);
                mainCam.transform.position = Vector3.Lerp(transitionPoints[currentCamPos - 1].cameraPosition.transform.position, transitionPoints[currentCamPos].cameraPosition.transform.position, lerpPlayer);
                mainCam.transform.LookAt(player.transform.position, Vector3.up);
                if (lerpPlayer >= 0.9)
                {
                    //plus camera count up and move to the next point 
                    currentCamPos++;
                }
                else if (lerpPlayer <= 0.1)
                {
                    //minus cam down
                    currentCamPos--;
                }
            }
        }
        
       
        
    }

    void CheckNumOfPositionsLeft()
    {
        if(transitionPoints.Length == currentCamPos)
        {
            //return player out of the cam
            transitionsScript.stopCam = true;
           
        }
    }
}

