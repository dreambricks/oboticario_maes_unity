using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutService : MonoBehaviour
{

    private bool canPlay = true;
  
    void Update()
    {
        DisableEnable();
        
    }

    void DisableEnable()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            canPlay = !canPlay;
            gameObject.SetActive(!canPlay);
            
        }
    }
}
