using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICheckpointObserver
{
    void OnCheckpointReached(GameObject checkpoint);
}
