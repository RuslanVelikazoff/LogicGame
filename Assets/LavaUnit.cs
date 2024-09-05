using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaUnit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Gold"))
        {
            Destroy(collision.gameObject);
        }
        if(collision.collider.CompareTag("Player"))
        {
            GameUI.Instance.OnLose();
        }
    }
}
