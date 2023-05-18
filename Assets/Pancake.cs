using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pancake : MonoBehaviour
{
    Transform righthand;
    bool flying = false;
    Rigidbody rb;

    Vector3 startPos_local;
    Vector3 startPos_global;
    Quaternion startRot_local;

    float launchTime = 0f;

    float radius;

    Vector3 endPos;

    float travelled = 0;

    Vector3 launchForce = new Vector3(0, 30, 0);
    // Start is called before the first frame update
    void Start()
    {
        righthand = transform.parent;
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = false;
        startPos_local = transform.localPosition;
        startRot_local = transform.localRotation;

        Vector3 p1 = new Vector3(FlipperController.main.transform.position.x, 0, FlipperController.main.transform.position.z);
        Vector3 p2 = new Vector3(transform.position.x, 0, transform.position.z);
        radius = Vector3.Distance(p1, p2);

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
            travelled = 0;
            startPos_global = transform.position;

            Vector3 p1 = FlipperController.main.transform.position;
            Vector3 p2 = transform.position;
            float angle = Mathf.Atan2((p2 - p1).x, (p2 - p1).z);

            float endAngle = angle + ((Random.value - 0.5f) * 60f);
            endPos = p1 + new Vector3(Mathf.Cos(endAngle), 0, Mathf.Sin(angle)).normalized * radius;
            //rb.AddForce((endPos - startPos_global).normalized * 2f, ForceMode.Force);

            print(endAngle - angle);

            Debug.DrawLine(p1, p2, Color.red, 20);
            Debug.DrawLine(p1, endPos, Color.green, 20);
        }
        if(flying)
        {
            launchTime += Time.deltaTime;
            if (travelled < Vector3.Distance(endPos, startPos_global))
            {
                float move = Time.deltaTime * 0.3f;
                travelled += move;
                transform.position += (endPos - startPos_global).normalized * move;
            }

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
