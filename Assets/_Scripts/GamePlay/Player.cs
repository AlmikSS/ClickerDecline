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
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Element element))
                {
                    if (GameManager.IsPlaying)
                    {
                        ElementDestroyed?.Invoke();
                        element.Burn();
                    }
                }
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