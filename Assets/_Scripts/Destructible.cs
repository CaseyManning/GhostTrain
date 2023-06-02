using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destructible : MonoBehaviour
{
    public static Dictionary<string, bool> destroyed;

    // Start is called before the first frame update
    void Start()
    {
        if(destroyed == null)
        {
            destroyed = new Dictionary<string, bool>();
        }
        string name = gameObject.name + gameObject.scene.name;
        if (destroyed.ContainsKey(name) && destroyed[name] == true)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            string name = gameObject.name + gameObject.scene.name;
            destroyed[name] = true;
        }
    }
}
