using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] groups;

    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomGroup();
    }

    public void SpawnRandomGroup()
    {
        int i = Random.Range(0, groups.Length);

        Instantiate(groups[i], transform.position, Quaternion.identity);
    }
}
