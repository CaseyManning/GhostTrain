using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Popup : MonoBehaviour
{
    public GameObject bodyText;

    public static Popup main;

    public static bool popping = false;

    bool mouseover = false;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    // Start is called before the first frame update
    void Start()
    {
        main = this;

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponentInParent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    public void open(string text, bool usable)
    {
        bodyText.GetComponent<TMP_Text>().text = text;
        gameObject.SetActive(true);
        popping = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsClickOverPopup())
        {
            gameObject.SetActive(false);
            popping = false;
        }
    }

    private bool IsClickOverPopup()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == gameObject)
                return true;
        }

        return false;
    }

    public void yes()
    {
        InventoryManager.main.use();
        gameObject.SetActive(false);
        popping = false;
    }
    public void no()
    {
        gameObject.SetActive(false);
        popping = false;
    }
}
