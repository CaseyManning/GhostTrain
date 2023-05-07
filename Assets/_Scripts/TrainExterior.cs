using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainExterior : MonoBehaviour
{

    static float trainSpeed = 4f;
    static float offThresh = 12f;

    static float trainWidth = 6.1f; //5.81f for cars;

    static int n_cars;

    // Start is called before the first frame update
    void Start()
    {
        //trainWidth = 2*GetComponent<BoxCollider>().bounds.extents.x;
        n_cars += 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * trainSpeed * Time.deltaTime;
        if(transform.position.x > offThresh)
        {
            transform.position -= new Vector3(n_cars * trainWidth, 0, 0);
        }
    }
}
