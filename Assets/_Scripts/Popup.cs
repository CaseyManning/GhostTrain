using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public GameObject bodyText;

    public static Popup main;

    public static bool popping = false;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
    }

    public void open(string text)
    {
        bodyText.GetComponent<TMP_Text>().text = text;
        gameObject.SetActive(true);
        popping = true;
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
