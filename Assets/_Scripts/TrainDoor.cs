using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainDoor : MonoBehaviour
{
    public bool right;
    int s_layer;
    bool inside = false;
    bool changed = false;

    // Start is called before the first frame update
    void Start()
    {
        s_layer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && inside && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 0.7f)
        {
            entered();
        }
    }

    public void entered()
    {
        if(changed)
        {
            return;
        }
        if(InventoryManager.main.inventory.Count > 0 && InventoryManager.main.inventory[0].name == "glasses")
        {
            Popup.main.open("You sense that there might be more to discover in this room if you use the Ghost Glasses", false);
            return;
        }
        changed = true;
        SceneController.main.change(right);
        CameraController.main.resetZoom();
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
