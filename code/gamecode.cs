using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamecode : MonoBehaviour
{

    public bool playerhasScrewdriver;
    public bool playerOpenedDrawer;

    public bool playerhasTape;
    public bool playerOpenedChest;

    public bool playerhasCoffee;
    public bool playerhasKey;

    private bool startMessage;
    private bool completed;
    private float endTimer;
    private GameObject sounds;

    void Start()
    {
        //initialize variables and assign necessary gameobjects
        startMessage = true;
        endTimer = 0f;
        sounds = GameObject.Find("sounds");

        //booleans defining game state
        playerhasCoffee = false;
        playerhasKey = false;
        playerhasScrewdriver = false;
        playerhasTape = false;
        completed = false;
    }

    
    void Update()
    {
        //assign Esc to quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //startmessage is shown using showActionMsg-method at the beginning
        if (startMessage)
        {
            this.GetComponent<uicode>().showActionMsg("What a beautiful night out here, but unfortunately my night shift is about to start. I have to get going.", 5f);
            startMessage = false;
        }

        //if completed is true, a timer of 5 seconds runs, then the scene is changed to the ending scene
        if (completed)
        {
            endTimer = endTimer + (Time.deltaTime * 1f);

            if (endTimer > 5f)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    //method for using the left drawer inside the cabin
    public void useDrawer() 
    {
        //assign necessary gameobjects
        GameObject drawer = GameObject.Find("drawer1");
        GameObject keys = GameObject.Find("keys");

        //do different things depending on game state booleans
        //if no screwdriver, only message is displayed and sound played
        if (!playerhasScrewdriver && !playerOpenedDrawer)
        {
            this.GetComponent<uicode>().showActionMsg("Stuck... I need something to pry this open.", 3f);
            sounds.GetComponents<AudioSource>()[1].Play();
        }
        //if screwdriver found but drawer not opened, drawer animation plays and game state booleans set
        //interaction target change to keys
        else if (playerhasScrewdriver && !playerOpenedDrawer)
        {
            this.GetComponent<uicode>().showActionMsg("There... got it.", 3f);
            sounds.GetComponents<AudioSource>()[4].PlayDelayed(0.5f);
            drawer.GetComponent<Animator>().SetBool("openstate", !drawer.GetComponent<Animator>().GetBool("openstate"));
            playerOpenedDrawer = true;

            drawer.GetComponent<drawer>().interactTargetName = "keys";
            drawer.GetComponents<BoxCollider>()[0].enabled = false;
            drawer.GetComponents<BoxCollider>()[0].enabled = true;

        }
        //if drawer opened but keys not picked up, keys are destroyed and game state booleans set
        //interaction target change back to drawer
        else if (playerOpenedDrawer && !playerhasKey)
        {
            this.GetComponent<uicode>().showActionMsg("So here's the key.", 3f);
            sounds.GetComponents<AudioSource>()[6].Play();
            playerhasKey = true;
            Destroy(keys);

            drawer.GetComponent<drawer>().interactTargetName = "drawer";
            drawer.GetComponents<BoxCollider>()[0].enabled = false;
            drawer.GetComponents<BoxCollider>()[0].enabled = true;
        }
        //when all of the above is done, different drawer animations and sounds are played depending on animator state
        else
        {
            drawer.GetComponent<Animator>().SetBool("openstate", !drawer.GetComponent<Animator>().GetBool("openstate"));

            if (drawer.GetComponent<Animator>().GetBool("openstate"))
            {
                sounds.GetComponents<AudioSource>()[4].PlayDelayed(0.5f);
            }
            else
            {
                sounds.GetComponents<AudioSource>()[5].PlayDelayed(1f);
            }
            
        }
    }

    //method for using the chest on the pier
    public void useChest()
    {
        //assign necessary gameobjects
        GameObject chest = GameObject.Find("chest");
        GameObject tape = GameObject.Find("ducttape");

        //do different things depending on game state booleans
        //if no key, only message is displayed and sound played
        if (!playerhasKey && !playerOpenedChest)
        {
            this.GetComponent<uicode>().showActionMsg("Locked. Now where did I put the key?", 3f);
            sounds.GetComponents<AudioSource>()[1].Play();
        }
        //if key found but chest not opened, chest animation plays and game state booleans set
        //interaction target change to duct tape
        else if (playerhasKey && !playerOpenedChest)
        {
            this.GetComponent<uicode>().showActionMsg("That worked.", 3f);
            chest.GetComponent<Animator>().SetBool("openstate", !chest.GetComponent<Animator>().GetBool("openstate"));
            sounds.GetComponents<AudioSource>()[4].PlayDelayed(0.5f);
            playerOpenedChest = true;

            chest.GetComponent<chest>().interactTargetName = "duct tape";
            chest.GetComponent<BoxCollider>().enabled = false;
            chest.GetComponent<BoxCollider>().enabled = true;
        }
        //if chest opened but tape not picked up, tape is destroyed and game state booleans set
        //interaction target change back to chest
        else if (playerOpenedChest && !playerhasTape)
        {
            this.GetComponent<uicode>().showActionMsg("Nice! A roll of duct tape.", 3f);
            sounds.GetComponents<AudioSource>()[6].Play();
            playerhasTape = true;
            Destroy(tape);

            chest.GetComponent<chest>().interactTargetName = "chest";
            chest.GetComponent<BoxCollider>().enabled = false;
            chest.GetComponent<BoxCollider>().enabled = true;
        }
        //when all of the above is done, different chest animations and sounds are played depending on animator state
        else
        {
            chest.GetComponent<Animator>().SetBool("openstate", !chest.GetComponent<Animator>().GetBool("openstate"));

            if (chest.GetComponent<Animator>().GetBool("openstate"))
            {
                sounds.GetComponents<AudioSource>()[4].PlayDelayed(0.5f);
            }
            else
            {
                sounds.GetComponents<AudioSource>()[5].PlayDelayed(0.3f);
            }
        }
    }

    //method for using the car
    public void useCar()
    {
        //if player hasn't interacted with coffee, a message is shown
        if (!playerhasCoffee)
        {
            this.GetComponent<uicode>().showActionMsg("I need something to keep me awake through the night.", 3f);
            return;
        }

        //if player does not have duct tape, a message is shown and sound played
        if (!playerhasTape)
        {
            this.GetComponent<uicode>().showActionMsg("Oh man... doesn't start. Some duct tape might do the trick.", 3f);
            sounds.GetComponents<AudioSource>()[8].Play();
            return;
        }

        //if player has interacted with coffee and has duct tape, sound is played and completed-boolean set to true
        sounds.GetComponents<AudioSource>()[10].Play();
        this.GetComponent<uicode>().showInteract = false;
        completed = true;
    }


}
