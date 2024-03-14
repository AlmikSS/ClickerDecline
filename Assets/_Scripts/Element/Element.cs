using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] private GameObject _fireObj;

    public void Burn()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _fireObj.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 0.25f);
    }

    private void Update()
    {
        if (!GameManager.IsPlaying)
            Destroy(gameObject);
    }
}