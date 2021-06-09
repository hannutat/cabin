using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffee : MonoBehaviour
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
        //if player is in trigger zone, presses E, and has not interacted with coffee before, 
        //message is shown, sound played and playerhasCoffee-boolean in gamecode set to true 
        if (triggerEntered && Input.GetKeyDown(KeyCode.E))
        {
            if (!gamecode.GetComponent<gamecode>().playerhasCoffee) 
            {
                gamecode.GetComponent<uicode>().showActionMsg("Ahh.. as black as the sky on a moonless night..", 3f);
                sounds.GetComponents<AudioSource>()[9].Play();
                gamecode.GetComponent<gamecode>().playerhasCoffee = true;
                gamecode.GetComponent<uicode>().showInteract = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //if player enters trigger zone, and has not interacted with coffee before, boolean triggerEntered is set to true
        //interact target string is written to uicode, and interaction ui text boolean set to true
        if (!gamecode.GetComponent<gamecode>().playerhasCoffee && collider.name.Equals("player"))
        {
            triggerEntered = true;
            gamecode.GetComponent<uicode>().interactTarget = "coffee maker";
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
