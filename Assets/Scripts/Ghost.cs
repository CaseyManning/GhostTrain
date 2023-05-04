using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = start + Vector3.up * Mathf.Sin(Time.time) * 0.1f;

        if(Time.time > 3)
        {

        }
    }

    public void appear()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Talkable>().enabled = true;
    }
}
