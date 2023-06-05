using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parentl : MonoBehaviour
{

    public static Parentl main;
    public GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change()
    {
        box.SetActive(true);
        Destroy(this);
    }
}
