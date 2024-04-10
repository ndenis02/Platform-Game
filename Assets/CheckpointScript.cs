using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private respawnscript respawn;

    private BoxCollider2D checkpointcollider;
    // Start is called before the first frame update

     void Awake()
    {
        checkpointcollider = GetComponent<BoxCollider2D>();
        respawn = GameObject.FindGameObjectWithTag("respawn").GetComponent<respawnscript>();   
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            respawn.respawnpoint = this.gameObject;
            checkpointcollider.enabled = false;
        }
    }
}
