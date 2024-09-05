using UnityEngine;

public class WaterUnit : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Lava"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
