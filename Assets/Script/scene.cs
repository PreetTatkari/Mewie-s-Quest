using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextscene : MonoBehaviour
{
    // Start is called before the first frame update
   public string scenename;

   void OnTriggerEnter(Collider other)
    {
      if(other.CompareTag("Player"))  {
    

    // Update is called once per frame
   SceneManager.LoadScene(scenename);
      }
        
    }
}