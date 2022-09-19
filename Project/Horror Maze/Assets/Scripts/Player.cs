using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject wallPrefab;
    public Rigidbody2D rb;
    Vector2 movement;
    float speed = 40;
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal") / speed, Input.GetAxis("Vertical") / speed);
        rb.MovePosition(rb.position + movement);
        movement = Vector2.zero;

        cameraObject.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Light Item"))
        {
            Destroy(collision.gameObject);
            gameObject.GetComponentInChildren<Light2D>().pointLightOuterRadius += 0.5f;
        }
        if (collision.gameObject.CompareTag("Speed Item"))
        {
            Destroy(collision.gameObject);
            speed -= 2.5f;
        }
        if (collision.gameObject.CompareTag("See Through Walls Item"))
        {
            Destroy(collision.gameObject);
            wallPrefab.GetComponentsInChildren<ShadowCaster2D>().ToList().ForEach(a => a.enabled = false);
        }
    }
}