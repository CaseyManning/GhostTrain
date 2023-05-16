using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBox : MonoBehaviour
{

    public ParticleSystem particles;
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
        StartCoroutine(ghostMode());
    }

    IEnumerator ghostMode()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().frozen = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().nav.Stop();
        VolumeController.main.ghostmode(true);
        yield return new WaitForSeconds(0.05f);
        VolumeController.main.ghostmode(false);
        particles.Play();
        yield return new WaitForSeconds(0.05f);
        VolumeController.main.ghostmode(true);
        yield return new WaitForSeconds(0.05f);
        VolumeController.main.ghostmode(false);
        yield return new WaitForSeconds(0.05f);
        VolumeController.main.ghostmode(true);

    }
}
