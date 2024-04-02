using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyScript : MonoBehaviour
{
    private int direction = 1;
    private Vector3 movement;
    private Animator anim;
    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.1f;
    public float delay = 2f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(2 * direction, 0f, 0f);
        transform.position = transform.position + movement * Time.deltaTime;
        bool isRunning = Mathf.Abs(movement.x) > 0.01f;
        anim.SetBool("isRunning", isRunning);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBoundary"))
        {
            direction = direction * -1;
            FlipSprite();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collider with" + gameObject);
            //StartCoroutine(ScreenShake(shakeDuration, shakeAmount));
            //StartCoroutine(LoadSceneAfterDelay(delay));

        }
    }

    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //IEnumerator ScreenShake(float duration, float amount)
    //{
    //    Vector3 originalPos = Camera.main.transform.localPosition;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        float offsetX = Random.Range(-1f, 1f) * amount;
    //        float offsetY = Random.Range(-1f, 1f) * amount;

    //        Camera.main.transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0f);

    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }

    //    // Reset camera position
    //    Camera.main.transform.localPosition = originalPos;
    //}
    //IEnumerator LoadSceneAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    SceneManager.LoadScene("1");
    //}
}