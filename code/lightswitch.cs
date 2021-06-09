using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightswitch : MonoBehaviour
{

    public GameObject mainlight;
    public GameObject bulb;
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
        //if player is in trigger zone and presses E, light components are switched on or off
        if (triggerEntered && Input.GetKeyDown(KeyCode.E))
        {
            sounds.GetComponents<AudioSource>()[6].Play();
            mainlight.GetComponent<Light>().enabled = !mainlight.GetComponent<Light>().enabled;
            bulb.GetComponent<Light>().enabled = !bulb.GetComponent<Light>().enabled;
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        //if player enters trigger zone, boolean triggerEntered is set to true
        //interact target string is written to uicode, and interaction ui text boolean set to true
        if (collider.name.Equals("player"))
        {
            triggerEntered = true;
            gamecode.GetComponent<uicode>().interactTarget = "light switch";
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
