using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishpoint3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadGame();
            
        }
    }

    public void LoadGame()
    {
        
      SceneManager.LoadScene("NextLevel2");
        
    }
   
}
