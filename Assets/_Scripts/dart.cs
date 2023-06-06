using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dart : MonoBehaviour
{
    private Rigidbody rb;
    private DartController dartController;
    private Collider collider;
    public bool collided = false;
    public bool thrown = false;

    private void Start()
    {
        // Get references to the Rigidbody, Collider and DartController
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        dartController = GameObject.FindObjectOfType<DartController>();
        collided = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        collided = true;
        if (collision.gameObject.name == "dartboard")
        {
            // Calculate score based on distance from center of dartboard
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 dartBoardCenter = collision.gameObject.transform.position;

            float distanceFromCenter = Vector3.Distance(hitPoint, dartBoardCenter);

            // calculate score - this is just an example, you'll need to replace this with your own scoring logic
            float rawScore= 40 - (distanceFromCenter * 50);  // each unit of distance from the center deducts 10 points

            // you might want to cap this at zero so you don't get negative scores
            rawScore = Mathf.Max(rawScore, 0);


            int score = Mathf.RoundToInt(rawScore);
            // Notify the DartController of the score
            Debug.Log("Score added successfullly");
            dartController.AddScore(score);

            // Stop all movement of the dart
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Disable the collider of the dart
            collider.enabled = false;
        }
        else if (collision.gameObject.name != "dart")
        {
            dartController.AddScore(0f);
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
