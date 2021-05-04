using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour
{

    public GameObject[] cutscenePoints;
    public Transform mainCam;
    public GameObject Target;

    public GameObject camStart;

    public bool startCutscene = false;
    public float cutsceneSpeed = 1f;

    public int currentCamPosition = 0;
    [Range(0,1)]
    public float lerpPercent = 0.01f;
    int numOfPoints;

    public bool firstLerp = true;

    private void Start()
    {
        //lerpPercent = 0;
        numOfPoints = cutscenePoints.Length;

        StartCoroutine(ResetCamPoints());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool playCutscene()
    {
        if (cutscenePoints.Length <= 0)
        {
            return false;
        }
        else
        {
            if (startCutscene)
            {
                if(currentCamPosition == 0)
                {
                    lerpPercent += Time.deltaTime * cutsceneSpeed;
                    mainCam.transform.position = Vector3.Lerp(camStart.transform.position, cutscenePoints[currentCamPosition].transform.position, lerpPercent);
                    if(lerpPercent >= 1)
                    {
                        lerpPercent = 0;
                        currentCamPosition += 1;
                        
                    }
                }
                else if (currentCamPosition >= 1)
                {
                    lerpPercent += Time.deltaTime * cutsceneSpeed;
                    mainCam.transform.position = Vector3.Lerp(cutscenePoints[currentCamPosition - 1].transform.position, cutscenePoints[currentCamPosition].transform.position, lerpPercent);
                    if (lerpPercent >= 1)
                    {
                        lerpPercent = 0;
                        currentCamPosition += 1;
                        if(currentCamPosition >= cutscenePoints.Length)
                        {
                            startCutscene = false;
                            return true;
                        }

                    }
                }
            }
            return false;
        }
    }
    void LookAtTarget()
    {
        mainCam.LookAt(Target.transform.position);
    }

    IEnumerator ResetCamPoints()
    {
        while(true)
        {
            if (currentCamPosition == cutscenePoints.Length)
            {
                //Wait then reset camera

                yield return new WaitForSecondsRealtime(1);

                currentCamPosition = 0;
                startCutscene = false;
            }
            yield return null;
        }
      
    }

}
