using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{

    public static float talkDist = 1f;

    public NavMeshAgent nav;
    public Animator anim;

    public static int character_index = 0;
    public static string character_name = "";

    public List<GameObject> models;

    public bool frozen = false;

    public static GameObject player;

    public Talkable walkingTowards;

    private void Awake()
    {
        player = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SceneController.dontload)
        {
            transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
        }
        models[character_index].SetActive(true);
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(frozen)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (nav.isActiveAndEnabled && !DialogueManager.main.talking && !Popup.popping)
            {
                print("can move");
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 3))
                {
                    nav.SetDestination(hit.point);
                    walkingTowards = null;
                    print("setting dest");
                    //anim.SetTrigger("Walk");
                }
            }
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitt, 100))
            {
                if (hitt.collider.gameObject.TryGetComponent(out Talkable t))
                {
                    if (Vector3.Distance(hitt.collider.gameObject.transform.position, transform.position) < talkDist)
                    {
                        if (!DialogueManager.main.talking)
                        {
                            CameraController.main.talkother = hitt.collider.gameObject;
                            transform.LookAt(hitt.collider.gameObject.transform.position);
                            DialogueManager.main.startConvo(t.convoName);
                        }
                    } else
                    {
                        print("setting");
                        nav.SetDestination(hitt.point);
                        walkingTowards = t;
                    }
                }
            }
        }
        if(nav.velocity.magnitude > 0.12f)
        {
            Quaternion orig = transform.rotation;
            transform.LookAt(transform.position + nav.velocity, Vector3.up);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(orig, transform.rotation, 0.25f);
            anim.ResetTrigger("Idle");
            anim.SetTrigger("Walk");

            if (walkingTowards != null && Vector3.Distance(walkingTowards.gameObject.transform.position, transform.position) < talkDist)
            {
                CameraController.main.talkother = walkingTowards.gameObject;
                DialogueManager.main.startConvo(walkingTowards.convoName);
                walkingTowards = null;
            }

        } else
        {
            if (nav.enabled == true)
            {
                anim.ResetTrigger("Walk");
                anim.SetTrigger("Idle");
            }
        }

        if(Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(trip());
        }
    }

    IEnumerator trip()
    {
        anim.SetTrigger("Trip");
        yield return new WaitForSeconds(0.7f);
        Vector3 oldDest = new Vector3(nav.destination.x, nav.destination.y, nav.destination.z);
        nav.SetDestination(transform.position);
        yield return new WaitForSeconds(5f);
        nav.SetDestination(oldDest);
        anim.ResetTrigger("Trip");
        yield return new WaitForEndOfFrame();
        nav.SetDestination(oldDest);
        print("setting dest");


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            other.gameObject.GetComponent<TrainDoor>().entered();
        }

        if (other.gameObject.TryGetComponent<GhostBox>(out GhostBox box))
        {
            box.entered();
        }
        if (other.gameObject.TryGetComponent<gameender>(out gameender ender))
        {
            ender.entered();
        }
    }

    public void stopMoving()
    {
        if (nav.isActiveAndEnabled)
        {
            nav.destination = transform.position;
        }
        if (!CameraController.main.dontzoom && NarrativeManager.main.completedsequence)
        {
            anim.ResetTrigger("Walk");

            anim.SetTrigger("Idle");
        }
    }
}
