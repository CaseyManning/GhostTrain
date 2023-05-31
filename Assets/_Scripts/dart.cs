using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dart : MonoBehaviour
{
    private Rigidbody rb;
    private DartController dartController;

    private void Start()
    {
        // Get references to the Rigidbody and DartController
        rb = GetComponent<Rigidbody>();
        dartController = GameObject.FindObjectOfType<DartController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "dartboard")
        {
            // Calculate score based on distance from center of dartboard
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 dartBoardCenter = collision.gameObject.transform.position;

            float distanceFromCenter = Vector3.Distance(hitPoint, dartBoardCenter);

            // calculate score - this is just an example, you'll need to replace this with your own scoring logic
            float score = 100 - distanceFromCenter * 10;  // each unit of distance from the center deducts 10 points

            // you might want to cap this at zero so you don't get negative scores
            score = Mathf.Max(score, 0);

            // Notify the DartController of the score
            dartController.AddScore(score);

            // Stop all movement of the dart
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Make the dart a child of the dartboard
            transform.SetParent(collision.transform, true);

        }
    }
}