using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsPanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI questionsAnsweredText;
    [SerializeField] private TextMeshProUGUI answerAccuracyText;
    [SerializeField] private TextMeshProUGUI determinedLevelText;
    [SerializeField] private TextMeshProUGUI timeOnQuestionText;

    [Header("References")]
    [SerializeField] private PlayerModel playerModelReference;
    
    //
    //  = CORE =

    private void Start()
    {
        //  - get player model ref
        playerModelReference = GameObject.FindObjectOfType<PlayerModel>();
    }

    private void Update()
    {
        // 
        // First, check if player model reference exists.

        if (!playerModelReference)
        {
            print("ERROR: Player Model doesn't exist!");
            return;
        }
        
        //
        // Update UI
        
        //  - update questions answered
        UpdateQuestionsAnswered(playerModelReference.answersTotal);
        //  - update answer accuracy
        UpdateAnswerAccuracy(RoundTo2DP(playerModelReference.answerAccuracy));
        //  - update determined level
        UpdateDeterminedLevel(RoundTo2DP(playerModelReference.aiDeterminedLevel));
        //  - update time on question
        UpdateTimeOnQuestion(playerModelReference.timeOnQuestion);
    }

    private float RoundTo2DP(float val)
    {
        return Mathf.Round(val * 100.0f) / 100.0f;
    }
    
    //
    //  = UI UPDATE FUNCTIONS =
    
    //  - update questions answered
    private void UpdateQuestionsAnswered(int answered)
    {
        questionsAnsweredText.text = answered.ToString();
    }
    
    //  - update answer accuracy
    private void UpdateAnswerAccuracy(float accuracy)
    {
        answerAccuracyText.text = (100.0f * accuracy) + "%";
    }
    
    //  - update determined level
    private void UpdateDeterminedLevel(float level)
    {
        determinedLevelText.text = level.ToString();
    }
    
    //  - update time on question
    private void UpdateTimeOnQuestion(float time)
    {
        timeOnQuestionText.text = (int)time + "s";
    }
}
