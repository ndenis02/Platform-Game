using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class enemyMov : MonoBehaviour
{
    //if Red enemy true;
    public bool smartEnemy;
    private float Enemyspeed;
    
    bool switchD = false;
    // Start is called before the first frame update
    void Start()
    {

        Enemyspeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (smartEnemy)
        {
            /* if (switchD)
             {
                 transform.Translate(new Vector2(0.005f, 0));
             }
             else
             {
                 transform.Translate(new Vector2(-0.005f, 0));
             }
         }
         else
         {
             transform.Translate(new Vector2(-0.005f, 0));
         }*/
            if (switchD)
            {
                transform.Translate(new Vector2(Enemyspeed * Time.deltaTime, 0));
            }
            else
            {
                transform.Translate(new Vector2(-Enemyspeed * Time.deltaTime, 0));
            }
        }
        else
        {
            transform.Translate(new Vector2(-Enemyspeed * Time.deltaTime, 0));
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "enemyBoundary")
        {
            switchD = !switchD;
        }
    }
 
}
