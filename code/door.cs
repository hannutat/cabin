using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    private bool triggerEntered;
    private GameObject gamecode;
    private GameObject sounds;

    public string interactTargetName;

    void Start()
    {
        //initialize variables and assign necessary gameobjects
        triggerEntered = false;
        gamecode = GameObject.Find("gamecode");
        sounds = GameObject.Find("sounds");
    }

    void Update()
    {
        //if player is in trigger zone and presses E, animator parameter is set to open or close
        if (triggerEntered && Input.GetKeyDown(KeyCode.E))
        {
            this.GetComponent<Animator>().SetBool("openstate", !this.GetComponent<Animator>().GetBool("openstate"));

            //based on what the interactTargetName is and what the status of animator boolean is, different sounds are played
            if (interactTargetName.Equals("door"))
            {
                if (this.GetComponent<Animator>().GetBool("openstate"))
                {
                    sounds.GetComponents<AudioSource>()[2].Play();
                }
                else
                {
                    sounds.GetComponents<AudioSource>()[3].PlayDelayed(1f);
                }
            }

            //based on what the interactTargetName is and what the status of animator boolean is, different sounds are played
            if (interactTargetName.Equals("drawer"))
            {
                if (this.GetComponent<Animator>().GetBool("openstate"))
                {
                    sounds.GetComponents<AudioSource>()[4].PlayDelayed(0.5f);
                }
                else
                {
                    sounds.GetComponents<AudioSource>()[5].PlayDelayed(1f);
                }
            }
            
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        //if player enters trigger zone, boolean triggerEntered is set to true
        //interact target string is written to uicode, and interaction ui text boolean set to true
        if (collider.name.Equals("player"))
        {
            triggerEntered = true;
            gamecode.GetComponent<uicode>().interactTarget = interactTargetName;
            gamecode.GetComponent<uicode>().showInteract = true;
        }
    }

    void OnTriggerExit(Collider collider)
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
