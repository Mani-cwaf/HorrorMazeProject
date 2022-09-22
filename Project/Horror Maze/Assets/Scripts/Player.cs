using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject wallPrefab;
    public Rigidbody2D rb;
    Vector2 movement;
    float speed = 40;
    public float difficulty = 1;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
    }
    void FixedUpdate()
    {
        movement = new Vector2(Input.GetAxis("Horizontal") / speed / difficulty, Input.GetAxis("Vertical") / speed / difficulty);
        rb.MovePosition(rb.position + movement);
        movement = Vector2.zero;

        cameraObject = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
        cameraObject.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Light Item"))
        {
            Destroy(collision.gameObject);
            gameObject.GetComponentInChildren<Light2D>().pointLightOuterRadius += 0.5f;
        }
        if (collision.gameObject.CompareTag("Speed Item"))
        {
            Destroy(collision.gameObject);
            speed -= 2.5f * (speed / 35);
        }
        if (collision.gameObject.CompareTag("See Through Walls Item"))
        {
            Destroy(collision.gameObject);
            wallPrefab.GetComponentsInChildren<ShadowCaster2D>().ToList().ForEach(a => a.enabled = false);
        }
    }
    public void LightPowerDecreaseTick()
    {
        if (gameObject.GetComponentInChildren<Light2D>().pointLightOuterRadius > 0.8f)
        {
            gameObject.GetComponentInChildren<Light2D>().pointLightOuterRadius -= 0.1f;
        }
        else
        {
            gameObject.GetComponentInChildren<Light2D>().pointLightOuterRadius = 0;
            Death("Ran Out Of Light");
        }
    }
    public void Death(string deathMessage)
    {
    }
}