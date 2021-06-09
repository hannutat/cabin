using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uicode : MonoBehaviour
{
    public string interactTarget;
    public bool showInteract;

    private GameObject interactText;
    private GameObject actionText;
    private bool msgVisible;
    private float timer;
    private float msgTimer;

    void Start()
    {
        //initialize variables and assign necessary gameobjects
        interactTarget = "";
        showInteract = false;
        interactText = GameObject.Find("interactText");
        actionText = GameObject.Find("actionText");
        timer = 0f;
        msgVisible = false;
    }

    void Update()
    {
        //timer to remove actionmessage from screen after specified time
        if (msgVisible)
        {
            timer += Time.deltaTime * 1f;

            if (timer > msgTimer)
            {
                actionText.GetComponent<Text>().enabled = false;
                msgVisible = false;
            }
        }

        //visibility of interactText is toggled depending on showInteract-boolean state
        if (showInteract)
        {
            interactText.GetComponent<Text>().text = "Press E to interact (" + interactTarget + ")";
            interactText.GetComponent<Text>().enabled = true;
        }
        else
        {
            interactText.GetComponent<Text>().enabled = false;
        }
        
    }

    //method to show messages on screen, 1st parameter=message string, 2nd parameter=timer in seconds
    public void showActionMsg(string message, float newMsgTimer)
    {
        //message is set and enabled
        actionText.GetComponent<Text>().text = message;
        actionText.GetComponent<Text>().enabled = true;

        //timer is initialized
        timer = 0f;
        msgTimer = newMsgTimer;
        msgVisible = true;
    }
}
