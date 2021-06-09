using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    
    void Start()
    {
        //prevent destroying of gameobject containing music on scene change
        DontDestroyOnLoad(this.gameObject);
    }

    
    void Update()
    {
        
    }
}
