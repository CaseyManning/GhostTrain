using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DartScoreManager : MonoBehaviour
{
    public TMP_Text textScore;
    public float score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0f;
        textScore.text = score.ToString() + "Points";
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = score.ToString() + "Points";
    }
}
