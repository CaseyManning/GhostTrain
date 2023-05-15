using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public static Music main;

    // Start is called before the first frame update
    void Start()
    {
        if(main != null)
        {
            Destroy(gameObject);
        }
        main = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
