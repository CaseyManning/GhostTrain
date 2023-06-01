using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DartController : MonoBehaviour
{
    public GameObject dartBoard;
    public GameObject dartPrefab;

    public Slider horizontalSlider;
    public Slider verticalSlider;

    public float maxHoldTime = 5.0f;
    public float throwForce = 400;

    private int dartCount = 10000;
    private int currentDart = 0;
    private GameObject dart;

    public float totalScore = 0;
    public float scoreToBeat = 1000000000000;

    //bounded by the dartboard
    public Vector3 throwDirection;

    // Oscillation bounds
    public float maxHorizontalAngle = 30.0f;
    public float maxVerticalAngle = 15.0f;

    //checks whether throwing or not
    private bool isThrowing = false;
    public float horizontalAngle;

    private DartScoreManager scoreManager;

    //dart prefab needs have a scripts that calls Calculate score when it hits the dartboard
    //dart baord, dart prefab, player, main camera needs to be set in the inspector
    //rigidBody needs to attached to the dart prefab for throwDart to wrok

    //Mathf.PingPong gives the oscialltion 
    void Start()
    {


        // playerInPosition();
        ZoomIn();
        //one prefab at a time
        Vector3 dartStartPosition = dartBoard.transform.position + dartBoard.transform.forward * 2;//+ dartBoard.transform.up;
        Quaternion dartStartRotation = Quaternion.LookRotation(dartBoard.transform.position - dartStartPosition);


        dart = Instantiate(dartPrefab, dartStartPosition, dartStartRotation);
        dart.transform.Rotate(0, 90, 0);
        dart.GetComponent<Rigidbody>().isKinematic = true;
        scoreManager = GameObject.Find("Canvas").GetComponent<DartScoreManager>();
    }

    void Update()
    {
        horizontalSlider.value = (horizontalAngle + 30) / 60;
        //verticalSlider.value = verticalAngle;

        Debug.Log("Update started");

        if (dart == null)
        {
            Debug.Log("Dart is null");
        }

        if (dartBoard == null)
        {
            Debug.Log("Dartboard is null");
        }



        if (!isThrowing)
        {

            // Oscillate the direction of the dart throw from left to right

            //   throwDirection = new Vector3(0, 0, Mathf.PingPong(Time.time, -1));

            //dart.transform.LookAt(dartBoard.transform.position + throwDirection);
            horizontalAngle = maxHorizontalAngle * (Mathf.PingPong(Time.time, 1) - 0.5f) * 2;


            throwDirection = new Vector3(0, horizontalAngle - 90, 0);

            // Rotate the dart according to the throwDirection
            dart.transform.rotation = Quaternion.Euler(throwDirection);


        }

        if (Input.GetMouseButtonDown(0) && currentDart < dartCount)
        {
            isThrowing = true;
        }

        if (Input.GetMouseButton(0) && isThrowing)
        {
            float verticalAngle = maxVerticalAngle * (Mathf.PingPong(Time.time, 1) - 0.5f) * 2;
            throwDirection = new Vector3(0, horizontalAngle - 90, verticalAngle);

            // Rotate the dart according to the throwDirection
            dart.transform.rotation = Quaternion.Euler(throwDirection);
            verticalSlider.value = (verticalAngle + 15) / 30;
        }

        if (Input.GetMouseButtonUp(0) && isThrowing)
        {
            ThrowDart();
            currentDart++;

            if (currentDart < dartCount)
            {
                Invoke("InstantiateDart", 1f); // Delay of 1 second

            }

            isThrowing = false;
        }

        //if the player has used up all the max darts = 3
        if (currentDart == dartCount || scoreToBeat < totalScore)
        {
            //exit the game
            //turn off this dartplayer and then enable the normal players
            this.gameObject.SetActive(false);

            //has been beaten 
            if (scoreToBeat < totalScore)
            {
                //then set the state variable to completed gameplay in the inventory manager

            }
            //ran out of darts -> should refill the darts at the player position
            else
            {

            }

        }

    }

    //when the player clicks on the dart board, the player is moved to the throwing position
    // no need to move the player directly, the player positions is fixed
    // void playerInPosition()
    //{
    // player.transform.position = new Vector3(dartBoard.transform.position.x, player.transform.position.y, dartBoard.transform.position.z - 2);
    // player.transform.LookAt(dartBoard.transform.position);
    // playerStartRotation = player.transform.rotation;
    //}

    //zooms into the dart board and the player by the camera controller
    void ZoomIn()
    {
        if (dart == null)
        {
            Debug.Log("Dart is null");
        }

        if (dartBoard == null)
        {
            Debug.Log("Dartboard is null");
        }
        //CameraController.main.talkother = dartBoard;
        //CameraController.main.talkzoom();
        if (dart == null)
        {
            Debug.Log("Dart is null");
        }

        if (dartBoard == null)
        {
            Debug.Log("Dartboard is null");
        }
    }

    void InstantiateDart()
    {
        Vector3 dartStartPosition = dartBoard.transform.position + dartBoard.transform.forward * 2;
        Quaternion dartStartRotation = Quaternion.LookRotation(dartBoard.transform.position - dartStartPosition);
        dart = Instantiate(dartPrefab, dartStartPosition, dartStartRotation);
        dart.transform.Rotate(0, 90, 0);
        dart.GetComponent<Rigidbody>().isKinematic = true;
    }

    void ThrowDart()
    {
        Rigidbody rb = dart.GetComponent<Rigidbody>();
        rb.isKinematic = false;  // Enable Rigidbody
                                 //  Vector3 throwDirectionVector = Quaternion.Euler(throwDirection) * Vector3.forward;
                                 //    throwDirectionVector.z = -Mathf.Abs(throwDirectionVector.z);

        Vector3 forceDirection = Vector3.back;
        rb.AddForce(-dart.transform.right * throwForce);
        //  rb.AddForce(throwDirection * throwForce);
    }


    public void AddScore(float score)
    {
        totalScore += score;
        scoreManager.score = totalScore;
    }
}


