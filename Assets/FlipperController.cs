using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{

    public Animation flipAnim;

    public float position = 0.5f;

    public float launchthreshold = 0.35f;

    float movespeed = 1.5f;

    public static FlipperController main;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        flipAnim = GetComponent<Animation>();
        
    }

    // Update is called once per frame
    void Update()
    {
        flipAnim["pancake"].time = position;
        flipAnim["pancake"].speed = 0.0f;
        flipAnim.Play("pancake");

        if(Input.GetKey(KeyCode.Space))
        {
            position += Time.deltaTime * movespeed;
        } else
        {
            position -= Time.deltaTime * movespeed;
        }

        position = Mathf.Clamp(position, 0, 0.8f);
    }

}
