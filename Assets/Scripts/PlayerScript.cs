using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{

    public float talkDist = 1f;

    NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
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
