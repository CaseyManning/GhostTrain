using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otherguy : MonoBehaviour
{

    public static bool go;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(go)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.position += Vector3.left * Time.deltaTime * 1.5f;
        }

    }
}
