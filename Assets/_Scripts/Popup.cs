using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public GameObject bodyText;

    public static Popup main;

    public static bool popping = false;

    bool mouseover = false;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
    }

    public void open(string text, bool usable)
    {
        bodyText.GetComponent<TMP_Text>().text = text;
        gameObject.SetActive(true);
        popping = true;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && !mouseover)
        {
            gameObject.SetActive(false);
            popping = false;
        }
    }

    private void OnMouseEnter()
    {
        mouseover = true;
    }
    private void OnMouseExit()
    {
        mouseover = false;
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
