using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>(); 
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if(hit.collider.CompareTag("MovablePlatform"))
                {
                    Destroy(hit.collider.gameObject);
                    GameUI.Instance.IncreaseBreakCount();
                }
            }
        }
    }


}
