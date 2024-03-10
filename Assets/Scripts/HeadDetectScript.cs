using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetectScript : MonoBehaviour
{
    GameObject Enemy;
    public string targetTag = "Player"; // Specify the tag you want to detect collisions with

    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag)) // Check if the collided object has the specified tag
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
            GetComponent<Collider2D>().enabled = false;
            Enemy.GetComponent<SpriteRenderer>().flipY = true;
            Enemy.GetComponent<Collider2D>().enabled = false;
            Vector3 movement = new Vector3(Random.Range(40, 70), Random.Range(-40, 40), 0f);
            Enemy.transform.position += movement * Time.deltaTime;
        }
    }
}
