using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestionGenerator : MonoBehaviour
{
    [Header("Questions")] 
    public Question chosenQuestion;
    [SerializeField] private List<Question> loadedQuestions;

    [Header("Questions Path")]
    [SerializeField] private string questionsPath = "Questions";

    [Header("Events")]
    [SerializeField] private UnityEvent onQuestionGenerated;

    // - last question
    private Question _lastQuestion;
    
    //
    // ** CORE * /

    //
    // ** QUESTION CHOOSING * /
    
    //  - choose a question closest difficulty
    public void ChooseClosestQuestion(float targetDifficulty)
    {
        //  - current closest difference and questions
        var closestDifference = float.MaxValue;
        var closestQuestion = loadedQuestions[0];
        
        //  - loop through each question
        foreach (var question in loadedQuestions)
        {
            //  - compare difference
            var difference = Mathf.Abs(question.questionLevel - targetDifficulty);
            
            //  - if the difference is less, set the new closest question
            if (!(difference < closestDifference)) continue;
            if (question == _lastQuestion) continue;
            
            //  - set the new closest question
            closestDifference = difference;
            closestQuestion = question;
        }
        
        //  - set the closest
        chosenQuestion = closestQuestion;
        //  - set last question
        _lastQuestion = chosenQuestion;
        
        //
        // Invoke event
        
        onQuestionGenerated.Invoke();
    }

    //
    // ** QUESTION LOADING * /
    
    //  - loads all questions in a path
    public void LoadAllQuestions()
    {
        //  - load all questions in a given folder
        var questionsArray = Resources.LoadAll<Question>(questionsPath);
        
        //  - add all questions to list
        loadedQuestions.AddRange(questionsArray);
    }
}
