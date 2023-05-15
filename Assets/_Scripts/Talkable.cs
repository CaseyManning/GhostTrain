using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Talkable : MonoBehaviour
{
    public string convoName = "test";
    int startlayer;

    public static Texture2D cursorTex;

    public Texture2D curs;

    // Start is called before the first frame update
    void Start()
    {
        startlayer = gameObject.layer;
        if(curs != null)
        {
            cursorTex = curs;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnMouseEnter()
    {
        //ScreenSpaceOutlines ss = (ScreenSpaceOutlines) CameraController.main.settings.rendererFeatures[1];
        //if(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < PlayerScript.talkDist)
        //{
        //    ss.outlineSettings.outlineColor = new Color(1f, 0.9f, 0.5f, 1f);
        //    print("yellow");
        //} else
        //{
        //    //ss.outlineSettings.outlineColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        //    print("gray");
        //}
        gameObject.layer = 6;
        Vector2 hotspot = new Vector2(cursorTex.width / 2, cursorTex.height / 2);
        Cursor.SetCursor(cursorTex, hotspot, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        gameObject.layer = startlayer;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
