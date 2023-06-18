using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AnswerQuestion : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent onAnswerCorrect;
    [SerializeField] private UnityEvent onAnswerIncorrect;

    [Header("Result Outcome UI")]
    [SerializeField] private TextMeshProUGUI resultOutcomeUI;
    [SerializeField] private Color resultOutcomeColorCorrect;
    [SerializeField] private Color resultOutcomeColorIncorrect;
    
    // private variables
    private QuestionGenerator _questionGeneratorRef;

    public void AnswerWith(int answer)
    {
        // 
        // Attempt to find question generator
        
        if (_questionGeneratorRef == null) 
            _questionGeneratorRef = FindObjectOfType<QuestionGenerator>();
        
        //
        // Compare with right answer

        if (answer == _questionGeneratorRef.chosenQuestion.answer)
        {
            onAnswerCorrect.Invoke();
            SetOutcomeHead(true);
        }
        else
        {
            onAnswerIncorrect.Invoke();
            SetOutcomeHead(false);
        }
    }

    private void SetOutcomeHead(bool correct)
    {
        if (correct)
        {
            resultOutcomeUI.text = "Correct!";
            resultOutcomeUI.color = resultOutcomeColorCorrect;
        }
        else
        {
            resultOutcomeUI.text = "Incorrect!";
            resultOutcomeUI.color = resultOutcomeColorIncorrect;
        }
    }
}
