using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rb;
    public float rangeFromPlayer;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        rangeFromPlayer = 5 / player.GetComponentInChildren<Light2D>().pointLightOuterRadius;

        Vector3 pos = gameObject.transform.position;
        Vector3 dis = (player.gameObject.transform.position - pos);
        if (dis.x < rangeFromPlayer && dis.y < rangeFromPlayer)
        {
            var circleCast = Physics2D.OverlapCircle(pos, rangeFromPlayer);
            if (circleCast && circleCast.gameObject.CompareTag("PlayerTag"))
            {
                print($"{gameObject.name}'s raycast hits {circleCast.gameObject.name} with tag {circleCast.gameObject.tag}");
                rb.MovePosition(pos + dis.normalized / 100);
            }
        }
    }
}