using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireplace : MonoBehaviour
{

    private GameObject fire;
    private bool triggerEntered;
    private GameObject gamecode;
    private GameObject smoke;
    private GameObject sounds;

    void Start()
    {
        //initialize variables and assign necessary gameobjects
        fire = GameObject.Find("fireplace");
        smoke = GameObject.Find("Smoke");
        triggerEntered = false;
        gamecode = GameObject.Find("gamecode");
        sounds = GameObject.Find("sounds");

        //fire and smoke are off by default
        fire.SetActive(false);
        smoke.SetActive(false);
        
    }

    
    void Update()
    {
        //if player is in trigger zone and presses E, fire and smoke is either activated or deactivated
        if (triggerEntered && Input.GetKeyDown(KeyCode.E))
        {
            if (fire.activeSelf)
            {
                fire.SetActive(false);
                smoke.SetActive(false);
            }   
            else if (!fire.activeSelf)
            {
                fire.SetActive(true);
                smoke.SetActive(true);
                sounds.GetComponents<AudioSource>()[7].Play();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //if player enters trigger zone, boolean triggerEntered is set to true
        //interact target string is written to uicode, and interaction ui text boolean set to true
        if (collider.name.Equals("player"))
        {
            triggerEntered = true;
            gamecode.GetComponent<uicode>().interactTarget = "fireplace";
            gamecode.GetComponent<uicode>().showInteract = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //if player exits trigger zone, boolean triggerEntered is set to false
        //interaction ui text boolean set to false
        if (collider.name.Equals("player"))
        {
            triggerEntered = false;
            gamecode.GetComponent<uicode>().showInteract = false;
        }
    }

}
