using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AnswersUI : MonoBehaviour
{
    [Header("Question Panel References")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Image questionImage;
    
    [Header("Answer Panel References")]
    [SerializeField] private TextMeshProUGUI answerAText;
    [SerializeField] private TextMeshProUGUI answerBText;
    [SerializeField] private TextMeshProUGUI answerCText;

    [Header("Answer Result Panel References")]
    [SerializeField] private TextMeshProUGUI resultHeadText;
    [SerializeField] private TextMeshProUGUI resultBodyText;

    [Header("AI")]
    [SerializeField] private PlayerModel playerModelReference;
    
    //
    //  = CORE =

    private void Start()
    {
        playerModelReference = FindObjectOfType<PlayerModel>();
    }
}
