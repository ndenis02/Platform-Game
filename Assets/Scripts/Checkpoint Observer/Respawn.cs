using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour, ICheckpointObserver
{
    public GameObject player;
    private Vector3 respawnPoint; // Change from GameObject to Vector3

    void Start()
    {
        respawnPoint = transform.position; // Set initial respawn point
    }

    public void OnCheckpointReached(GameObject checkpoint)
    {
        respawnPoint = checkpoint.transform.position;
        player.transform.position = respawnPoint;
    }
}