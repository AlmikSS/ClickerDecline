using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _elements = new();
    
    public IEnumerator SpawnElements()
    {
        Vector3 pos = new Vector3(Random.Range(-2.7f, 2.7f), transform.position.y, 0);
        int i = Random.Range(0, _elements.Count);

        Instantiate(_elements[i], pos, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(SpawnElements());
    }
}