using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{

    public float talkDist = 1f;

    NavMeshAgent nav;
    Animator anim;

    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (nav.isActiveAndEnabled && !DialogueManager.main.talking && !Popup.popping)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 3))
                {
                    nav.SetDestination(hit.point);
                    //anim.SetTrigger("Walk");
                }
            }
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitt, 100))
            {
                if (hitt.collider.gameObject.TryGetComponent<Talkable>(out Talkable t))
                {
                    if (Vector3.Distance(hitt.collider.gameObject.transform.position, transform.position) < talkDist)
                    {
                        CameraController.main.talkother = hitt.collider.gameObject;
                        DialogueManager.main.startConvo(t.convoName);
                    }
                }
            }
        } 
        if(nav.velocity.magnitude > 0.12f)
        {
            Quaternion orig = transform.rotation;
            transform.LookAt(nav.destination, Vector3.up);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(orig, transform.rotation, 0.25f);
            anim.ResetTrigger("Idle");
            anim.SetTrigger("Walk");
        } else
        {
            anim.ResetTrigger("Walk");
            anim.SetTrigger("Idle");
        }


        lastPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            other.gameObject.GetComponent<TrainDoor>().entered();
        }
    }

    public void stopMoving()
    {
        nav.destination = transform.position;
    }
}
