using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _elementLayer;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _elementLayer);

            if (hit != null)
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}