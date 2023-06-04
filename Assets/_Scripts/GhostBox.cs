using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBox : MonoBehaviour
{
    public SceneGhostt ghost;
    public ParticleSystem particles;

    public static bool everRan = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    public void entered()
    {
        if (!everRan)
        {
            StartCoroutine(ghostMode());
            everRan = true;
        }
    }

    IEnumerator ghostMode()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().frozen = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().stopMoving();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().nav.enabled = false;
        VolumeController.main.ghostmode(true);
        particles.Play();
        yield return new WaitForEndOfFrame();
        StartCoroutine(ghost.go());
    }
}
