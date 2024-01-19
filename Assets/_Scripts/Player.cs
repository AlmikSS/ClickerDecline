using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action ElementDestroyed;
    public static event Action PauseButtonPressed;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider.CompareTag("Element"))
            {
                ElementDestroyed?.Invoke();
                Destroy(hit.collider.gameObject);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    public void Pause()
    {
        PauseButtonPressed?.Invoke();
    }
}