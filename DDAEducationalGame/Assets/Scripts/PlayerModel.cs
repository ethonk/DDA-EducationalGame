using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : MonoBehaviour
{
    [Header("Model Data")]
    //  - outer data
    public float aiDeterminedLevel = 0.0f;
    public float answerAccuracy = 1.0f;
    public float timeOnQuestion = 0.0f;
    public bool timeRunning = true;
    
    //  - inner data
    public int answersCorrect = 0;
    public int answersTotal = 0;
    
    //  - game data
    public int maxQuestions = 30;

    [Header("Model Settings")] 
    [SerializeField] private int numberOfStoredResults = 10;    // store this many in list

    [Header("Model Stats")] 
    [SerializeField] private List<float> previousResults;

    [Header("Events")]
    [SerializeField] private UnityEvent onGameFinished;
    
    // Private variables
    private QuestionGenerator _questionGeneratorRef;
    private LogWriter _logWriter;
    
    //  
    //  = CORE =

    private void Awake()
    {
        //
        // Get variables
        
        _questionGeneratorRef = gameObject.GetComponent<QuestionGenerator>();
        _logWriter = GameObject.FindObjectOfType<LogWriter>();
        
        //
        // Pre-Calculate variables
        
        //  - pre-determine level
        CalculateLevel();
        
        //  - pre-determine answer accuracy
        CalculateAnswerAccuracy();
        
        //  - reset time on question
        timeOnQuestion = 0.0f;
        
        //  - load all questions
        _questionGeneratorRef.LoadAllQuestions();
        //  - pre-determine question
        if (_questionGeneratorRef) _questionGeneratorRef.ChooseClosestQuestion(aiDeterminedLevel);
    }

    private void Update()
    {
        //  - update time on question
        if (timeRunning) timeOnQuestion += Time.deltaTime;
    }

    //
    //  = MODEL FUNCTIONS =
    
    // Calculate level
    private void CalculateLevel()
    {
        //
        // Level Calculation
        
        //  - declare level
        var determinedLevel = 0.0f;
        
        //  - loop previous results, set the determined level
        foreach (var result in previousResults)
        {
            determinedLevel += result;
        }
        
        //  - get the average and set ai level
        if (previousResults.Count > 0)
        {
            determinedLevel /= previousResults.Count;
            aiDeterminedLevel = determinedLevel;
        }
    }
    
    // Add to list of results
    private void AddResult(float previousResult)
    {
        //  - add result to front
        previousResults.Add(previousResult);
        
        //  - if we exceed the max list size, remove the front
        if (previousResults.Count > numberOfStoredResults)
        {
            previousResults.RemoveAt(0);    // removes front element from array
        }
        
        //  - update AI determined level
        CalculateLevel();
    }

    public void AddAnsweredQuestion(bool correct)
    {
        //
        // Increment answer total and accuracy
        
        //  - add one to total
        answersTotal += 1;
        
        //  - if correct, add to correct answers
        if (correct) answersCorrect += 1;
        
        //  - re-calculate answer accuracy
        CalculateAnswerAccuracy();
        
        // 
        // Increment / Decrement level

        if (correct) 
        {
            //  - cap time to answer if exceeding
            var timeToAnswer = timeOnQuestion;
            if (timeOnQuestion > _questionGeneratorRef.chosenQuestion.expectedTimeToAnswer)
                timeOnQuestion = _questionGeneratorRef.chosenQuestion.expectedTimeToAnswer;
            
            //  - calculate level scaling (increase on high accuracy, and on low time scaling)
            var timeScaling = timeOnQuestion / _questionGeneratorRef.chosenQuestion.expectedTimeToAnswer;
            var levelScaling = answerAccuracy - (0.25f * timeScaling);    // time scaling halved

            //  - change difficulty
            aiDeterminedLevel += levelScaling;
            
            //  - log
            string logText1 = string.Format(
                "Answered question {0}/{1} CORRECTLY. Took {2} seconds, ({3}%) of max allocated time. Answer accuracy: ({4}%).",
                answersTotal, maxQuestions, timeOnQuestion, timeScaling * 100.0f, answerAccuracy * 100.0f);
            string logText2 = string.Format(
                "Level INCREASED by: +{0}. Now: {1}", levelScaling, aiDeterminedLevel);
            
            _logWriter.AppendToFile(logText1);
            _logWriter.AppendToFile(logText2);
            _logWriter.AppendToFile("");
        }
        else
        {
            //  - cap time to answer if exceeding
            var timeToAnswer = timeOnQuestion;
            if (timeOnQuestion > _questionGeneratorRef.chosenQuestion.expectedTimeToAnswer)
                timeOnQuestion = _questionGeneratorRef.chosenQuestion.expectedTimeToAnswer;
            
            //  - calculate level scaling (increase on high accuracy, and on low time scaling)
            var timeScaling = timeOnQuestion / _questionGeneratorRef.chosenQuestion.expectedTimeToAnswer;
            var levelScaling = answerAccuracy + (0.25f * timeScaling);

            //  - change difficulty
            aiDeterminedLevel -= 1 - levelScaling;

            //  - cap if goes below 0
            if (aiDeterminedLevel < 0) aiDeterminedLevel = 0;
            
            //  - log
            string logText1 = string.Format(
                "Answered question {0}/{1} INCORRECTLY. Took {2} seconds, ({3}%) of max allocated time. Answer accuracy: ({4}%).",
                answersTotal, maxQuestions, timeOnQuestion, timeScaling * 100.0f, answerAccuracy * 100.0f);
            string logText2 = string.Format(
                "Level DECREASED by: -{0}. Now: {1}", levelScaling, aiDeterminedLevel);
            
            
            _logWriter.AppendToFile(logText1);
            _logWriter.AppendToFile(logText2);
            _logWriter.AppendToFile("");
        }
        
        //
        // Stop and reset question timer
        
        timeRunning = false;
    }

    public void NextQuestion()
    {
        //
        // Check for game finished
        if (answersTotal >= maxQuestions)
        {
            _logWriter.AppendToFile("==========> GAME FINISHED!");
            _logWriter.AppendToFile("");
            
            string logText1 = string.Format("Player final level: {0}", aiDeterminedLevel);
            _logWriter.AppendToFile(logText1);

            onGameFinished.Invoke();
            
            return;
        }

        //
        // Get next question
        
        _questionGeneratorRef.ChooseClosestQuestion(aiDeterminedLevel);
        
        //
        // Start timer again

        timeOnQuestion = 0.0f;
        timeRunning = true;
    }
    
    // Get answer accuracy
    private void CalculateAnswerAccuracy()
    {
        //  - if no answers, automatically set to 100%
        if (answersTotal == 0)
        {
            answerAccuracy = 1.0f;
            return;
        }
        
        //  - otherwise set answer accuracy
        answerAccuracy = (float)answersCorrect / (float)answersTotal;
    }
}
