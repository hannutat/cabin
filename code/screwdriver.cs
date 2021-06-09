using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screwdriver : MonoBehaviour
{
    private bool triggerEntered;
    private GameObject gamecode;
    private GameObject sounds;

    void Start()
    {
        //initialize variables and assign necessary gameobjects
        triggerEntered = false;
        gamecode = GameObject.Find("gamecode");
        sounds = GameObject.Find("sounds");
    }

    
    void Update()
    {
        //if player is in trigger zone and presses E, message is shown, sound played, screwdriver-object destroyed and 
        //playerhasScrewdriver-boolean in gamecode set to true
        if (triggerEntered && Input.GetKeyDown(KeyCode.E))
        {
            gamecode.GetComponent<gamecode>().playerhasScrewdriver = true;
            sounds.GetComponents<AudioSource>()[6].Play();
            gamecode.GetComponent<uicode>().showInteract = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //if player enters trigger zone, boolean triggerEntered is set to true
        //interact target string is written to uicode, and interaction ui text boolean set to true
        if (collider.name.Equals("player"))
        {
            triggerEntered = true;
            gamecode.GetComponent<uicode>().interactTarget = "screwdriver";
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
