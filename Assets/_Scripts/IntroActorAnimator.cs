using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroActorAnimator : MonoBehaviour
{

    Animator anim;

    Vector3 lastpos;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lastpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, lastpos) > 0.001f)
        {
            anim.SetBool("Walking", true);
        } else
        {
            anim.SetBool("Walking", false);
        }
        lastpos = transform.position;
    }
}
