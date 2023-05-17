using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pancake : MonoBehaviour
{
    Transform righthand;
    bool flying = false;
    Rigidbody rb;

    Vector3 startPos_local;
    Quaternion startRot_local;

    float launchTime = 0f;

    Vector3 launchForce = new Vector3(0, 30, 0);
    // Start is called before the first frame update
    void Start()
    {
        righthand = transform.parent;
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = false;
        startPos_local = transform.localPosition;
        startRot_local = transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        if(!flying && Input.GetKeyUp(KeyCode.Space) && FlipperController.main.position > FlipperController.main.launchthreshold)
        {
            transform.SetParent(null);
            flying = true;
            rb.isKinematic = false;
            rb.detectCollisions = true;
            float posforce = (FlipperController.main.position + 0.5f) * (FlipperController.main.position + 0.5f);
            rb.AddForce(launchForce * posforce);
            rb.angularVelocity = new Vector3(5, 0, 0);
            launchTime = 0;
        }
        if(flying)
        {
            launchTime += Time.deltaTime;
        }

        if (flying && launchTime > 0.3f && Vector3.Distance(righthand.TransformPoint(startPos_local), transform.position) < 0.1f)
        {
            transform.SetParent(righthand);
            transform.localPosition = startPos_local;
            transform.localRotation = startRot_local;
            flying = false;
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }

    
}
