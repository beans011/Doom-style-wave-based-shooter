using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaceBloodSplatter : MonoBehaviour
{
    public GameObject[] bloodSplatters;
    public int numberOfObjects = 10; 
    public float placementRadius = 10f; 

    void Start()
    {
        PlaceObjects();
    }

    void PlaceObjects()
    {
        NavMeshHit hit;

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * placementRadius + transform.position;

            if (NavMesh.SamplePosition(randomPosition, out hit, placementRadius, NavMesh.AllAreas))
            {
                GameObject objectPrefab = bloodSplatters[Random.Range(0, bloodSplatters.Length)];
                Instantiate(objectPrefab, hit.position, Quaternion.identity);
            }
        }
    }
}
