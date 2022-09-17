using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public GameObject cameraObject;
    List<GameObject> wallPrefabs;
    public Rigidbody2D rb;
    Vector2 movement;
    private void Start()
    {
        wallPrefabs = GameObject.FindGameObjectsWithTag("Wall").ToList();
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector2.up / 30;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector2.left / 30;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector2.down / 30;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector2.right / 30;
        }
        rb.MovePosition(rb.position + movement);
        movement = Vector2.zero;
        cameraObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Light Item"))
        {
            Destroy(collision.gameObject);
            gameObject.GetComponentInChildren<Light2D>().pointLightOuterRadius += 2f / gameObject.GetComponentInChildren<Light2D>().pointLightOuterRadius;
        }
        if (collision.gameObject.CompareTag("See Through Walls Item"))
        {
            Destroy(collision.gameObject);
            wallPrefabs.ForEach(a => a.GetComponent<ShadowCaster2D>().enabled = false);
        }
    }
}