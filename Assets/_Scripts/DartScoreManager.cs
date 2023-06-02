using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DartScoreManager : MonoBehaviour
{
    public TMP_Text textScore;
    public TMP_Text remainingDarts;
    public TMP_Text priorDartText;
    public float score;
    public float priorDartScore;
    private DartController dartController;
    
    private int dartMax;
    private int dartUsed;
    // Start is called before the first frame update
    void Start()
    {
        score = 0f;

        dartController = GameObject.FindObjectOfType<DartController>();
        dartMax = dartController.dartCount;

        textScore.text = score.ToString() + "/" + dartController.scoreToBeat  + "PTS earned";
        remainingDarts.text = dartMax.ToString() + "/" + dartMax.ToString() + " Darts Left";
    }

    // Update is called once per frame
    void Update()
    {
        score = dartController.totalScore;
        priorDartScore = dartController.priorDartScore;
        dartUsed = dartController.currentDart;

        textScore.text = score.ToString() + "/" + dartController.scoreToBeat + "PTS earned.";
        remainingDarts.text = (dartMax - dartUsed).ToString() + "/" + dartMax.ToString() + " Darts Left";
        priorDartText.text = "Prior Dart: " + priorDartScore.ToString() + "PTS";
    }
}
