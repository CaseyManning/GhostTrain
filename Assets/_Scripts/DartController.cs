using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DartController : MonoBehaviour
{
    public GameObject dartBoard;
    public GameObject dartPrefab;

    
    public Slider horizontalSlider;
    public Slider verticalSlider;

    public float maxHoldTime = 5.0f;
    public float throwForce = 400;

    public int dartCount = 5;
    public float currentDart = 0;
    private GameObject dart;

    public float totalScore = 0;
    public float priorDartScore;
    public float scoreToBeat = 100;

    //bounded by the dartboard
    public Vector3 throwDirection;

    // Oscillation bounds
    public float maxHorizontalAngle = 30.0f;
    public float maxVerticalAngle = 15.0f;

    //checks whether throwing or not
    private bool isThrowing = false;
    public float horizontalAngle;
    public bool userIsReady = false;
    private bool runItOnce = true;

    public bool playedTwice = false;

    public DartScoreManager scoreManager;
    public Canvas myCanvas;

    private List<GameObject> instantiatedDarts = new List<GameObject>();


    //dart prefab needs have a scripts that calls Calculate score when it hits the dartboard
    //dart baord, dart prefab, player, main camera needs to be set in the inspector
    //rigidBody needs to attached to the dart prefab for throwDart to wrok

    //Mathf.PingPong gives the oscialltion
    

    void Start()
    {

        // Invoke("ShowCanvas", 1.5f);

        // playerInPosition();
        runItOnce = true;
       ZoomIn();
        //one prefab at a time
        
       
        userIsReady = false;
       // scoreManager = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<DartScoreManager>();
    }

    void Update()
    {
        if (!DialogueManager.main.talking)
        {

           // if (myCanvas.transform.GetChild(0).transform.Find("Instructions").gameObject.active)
            //{
                
            //}
            horizontalSlider.value = (horizontalAngle + 30) / 60;

            Debug.Log("Update started");

            if (!isThrowing)
            {
                horizontalAngle = maxHorizontalAngle * (Mathf.PingPong(Time.time * (currentDart/5 + 1), 1) - 0.5f) * 2;
                throwDirection = new Vector3(0, horizontalAngle - 90, 0);
                if (!dart.GetComponent<dart>().thrown)
                {
                    dart.transform.rotation = Quaternion.Euler(throwDirection);
                }
            }

             if (Input.GetMouseButtonDown(0) && currentDart < dartCount )
            {
                Debug.Log(userIsReady.ToString());
                isThrowing = true;
            }
            else
            if (Input.GetMouseButton(0) && isThrowing )
            {
                Debug.Log(userIsReady.ToString());
                isThrowing = true;
                float verticalAngle = maxVerticalAngle * (Mathf.PingPong(Time.time * (currentDart/5 + 1), 1) - 0.5f) * 2;
                throwDirection = new Vector3(0, horizontalAngle - 90, verticalAngle);
                if (!dart.GetComponent<dart>().thrown)
                {
                    dart.transform.rotation = Quaternion.Euler(throwDirection);
                }
                verticalSlider.value = (verticalAngle + 15) / 30;
            }

            if (Input.GetMouseButtonUp(0) && isThrowing )
            {
                ThrowDart();
                currentDart++;

                if (currentDart < dartCount)
                {
                    Invoke("InstantiateDart", 1.0f); // Delay of 1.5 seconds

                }
                isThrowing = false;
            }

            //if the player has used up all the max darts = 5
            //only call this function once
            if (runItOnce)
            {
                if (currentDart == dartCount)
                {
                    runItOnce = false;
                    if (!playedTwice)
                    {
                        //exit the game
                        //turn off this dartplayer and then enable the normal players
                        Debug.Log("game not updated1");
                        Invoke("updateGame", 1.0f);
                        

                    }
                    else
                    {
                        Invoke("endGame", 1.0f);
                       
                    }
                }
            }
        }

    }

    private void OnEnable()
    {
        ResetGame();
        Vector3 dartStartPosition = dartBoard.transform.position + dartBoard.transform.forward * 2;//+ dartBoard.transform.up;
        Quaternion dartStartRotation = Quaternion.LookRotation(dartBoard.transform.position - dartStartPosition);
        dart = Instantiate(dartPrefab, dartStartPosition, dartStartRotation);
        instantiatedDarts.Add(dart);
        dart.transform.Rotate(0, 90, 0);
        dart.GetComponent<Rigidbody>().isKinematic = true;
        GameObject dartHolder = GameObject.Find("DartHolder");
        if (dartHolder != null)
        {
            dart.transform.SetParent(dartHolder.transform);
        }
        else
        {
            Debug.LogWarning("DartHolder GameObject could not be found in the scene.");
        }
       // scoreManager = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<DartScoreManager>();

    }

    void ResetGame()
    {
        clearDarts();
        totalScore = 0;
        currentDart = 0;
        isThrowing = false;
        userIsReady = false;
        priorDartScore = 0;
        Invoke("ShowCanvas", 1f);
        ZoomIn();
        runItOnce = true;

        Debug.Log("game not updated2");
    }

    void endGame()
    {
        myCanvas.transform.GetChild(0).gameObject.SetActive(false);
        //has been beaten 
        if (scoreToBeat <= totalScore)
        {
            //then set the state variable to completed gameplay in the inventory manager
            //start WIN dialogue on ink

            PlayerScript.player.SetActive(true);
            CameraController.main.talkother = GameObject.Find("bartender");
            DialogueManager.main.startConvo("dartWon1");
        }
        else
        {
            PlayerScript.player.SetActive(true);
            CameraController.main.talkother = GameObject.Find("bartender");
            DialogueManager.main.startConvo("dartLost2");
            //start LOST dialogue on ink
        }
        this.gameObject.SetActive(false);
    }

    void updateGame()
    {
       

        //turn off canvas
        myCanvas.transform.GetChild(0).gameObject.SetActive(false);
        Debug.Log(totalScore.ToString());
        Debug.Log("game not updated3");

        //has been beaten 
        if (scoreToBeat <= totalScore)
        {
            //then set the state variable to completed gameplay in the inventory manager
            //start WIN dialogue on ink

            PlayerScript.player.SetActive(true);
            CameraController.main.talkother = GameObject.Find("bartender");
            DialogueManager.main.startConvo("dartWon");
        }
        else
        {
            PlayerScript.player.SetActive(true);
            CameraController.main.talkother = GameObject.Find("bartender");
            DialogueManager.main.startConvo("dartLost");
            //start LOST dialogue on ink
        }
        this.gameObject.SetActive(false);
        playedTwice = true;
        
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

        GameObject dartHolder = GameObject.Find("DartHolder");
        if (dartHolder != null)
        {
            dart.transform.SetParent(dartHolder.transform);
        }
        else
        {
            Debug.LogWarning("DartHolder GameObject could not be found in the scene.");
        }

        instantiatedDarts.Add(dart);
    }

    void clearDarts()
    {
        foreach (GameObject dart in instantiatedDarts)
        {
            Destroy(dart);
        }
        // Clear the list
        instantiatedDarts.Clear();
    }

    void ShowCanvas()
    {
        myCanvas.transform.GetChild(0).gameObject.SetActive(true);
        myCanvas.transform.GetChild(0).Find("Instructions").gameObject.SetActive(true);
        //turn on instructions if off
    }

    void ThrowDart()
    {
        Rigidbody rb = dart.GetComponent<Rigidbody>();
        rb.isKinematic = false;  // Enable Rigidbody
                                 //  Vector3 throwDirectionVector = Quaternion.Euler(throwDirection) * Vector3.forward;
                                 //    throwDirectionVector.z = -Mathf.Abs(throwDirectionVector.z);

        Vector3 forceDirection = Vector3.back;
        rb.AddForce(-dart.transform.right * throwForce);
        //rb.freezeRotation = true;
        dart.GetComponent<dart>().thrown = true;
        //  rb.AddForce(throwDirection * throwForce);
    }


    public void AddScore(float score)
    {
        totalScore += score;
        scoreManager.score = totalScore;
        priorDartScore = score;
    }
}


