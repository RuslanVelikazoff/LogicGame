using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    public bool isRun;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(isRun && transform.position.y < -3)
        {
            anim.SetBool("Run", true);
            rb.velocity = Vector2.right * 1.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Gold"))
        {
            GameUI.Instance.IncreaseScoreCount();
            Destroy(collision.gameObject);
        }
    }
}
