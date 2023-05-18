using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainDoor : MonoBehaviour
{
    public bool right;
    int s_layer;
    bool inside = false;

    // Start is called before the first frame update
    void Start()
    {
        s_layer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && inside && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 0.5f)
        {
            entered();
            inside = false;
        }
    }

    public void entered()
    {
        SceneController.main.change(right);
    }

    private void OnMouseEnter()
    {
        gameObject.layer = 9;
        inside = true;
        print("hi");
    }

    private void OnMouseExit()
    {
        gameObject.layer = s_layer;
        inside = false;
    }
}
