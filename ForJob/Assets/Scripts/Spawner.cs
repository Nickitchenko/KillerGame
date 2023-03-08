using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTimer;
    
    [SerializeField]
    private List<Transform> points;

    public GameObject zombiePrefab;

    private void Start()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            points.Add(transform.GetChild(i));
        }

        StartCoroutine("Timer");
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(spawnTimer);

        Vector3 spawnPosition = points[Random.Range(0, points.Count)].position;
        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity, null);

        StartCoroutine("Timer");
    }


}
