using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerspawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
