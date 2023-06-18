using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    [Header("Question UI")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Image questionImage;
    [SerializeField] private TextMeshProUGUI answer1;
    [SerializeField] private TextMeshProUGUI answer2;
    [SerializeField] private TextMeshProUGUI answer3;

    [Header("Explanation UI")]
    [SerializeField] private TextMeshProUGUI answerExplanation;
    
    // private variables
    private QuestionGenerator _questionGeneratorRef;

    //
    // UI Functions

    public void DisplayQuestion()
    {
        if (_questionGeneratorRef == null)
            _questionGeneratorRef = FindObjectOfType<QuestionGenerator>();
        
        // display question
        questionText.text = _questionGeneratorRef.chosenQuestion.question;
        questionImage.sprite = _questionGeneratorRef.chosenQuestion.questionImage;
        answer1.text = "A) " + _questionGeneratorRef.chosenQuestion.answerA;
        answer2.text = "B) " + _questionGeneratorRef.chosenQuestion.answerB;
        answer3.text = "C) " + _questionGeneratorRef.chosenQuestion.answerC;
        
        // display explanation
        answerExplanation.text = _questionGeneratorRef.chosenQuestion.answerExplanation;
    }
}
