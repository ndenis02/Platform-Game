using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectMov : MonoBehaviour
{
    public float speed;
    public float height;
    Vector2 pos;

    private void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        transform.position = new Vector2(transform.position.x, newY);
    }
}
