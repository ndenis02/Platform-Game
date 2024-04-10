using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint1 : MonoBehaviour
{
    private BoxCollider2D checkpointCollider;

    void Awake()
    {
        checkpointCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ICheckpointObserver[] observers = FindObjectsOfType<MonoBehaviour>() as ICheckpointObserver[];
            foreach (ICheckpointObserver observer in observers)
            {
                observer.OnCheckpointReached(gameObject);
            }

            checkpointCollider.enabled = false;
        }
    }
}