using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Ethonk/QuestionTemplate", order = 1)]
public class Question : ScriptableObject
{
    [Header("AI")]
    [Range(0, 30)] public float questionLevel;
    
    [Header("Questions")]
    public string question;
    public Sprite questionImage;
    
    [Header("Answers - Potential")]
    public string answerA;
    public string answerB;
    public string answerC;
    
    [Header("Answers - Correct")]
    public string answerExplanation;
    [Range(1, 3)] public int answer = 1;

    [Header("Stats (Difficulty Augmenting)")]
    public float expectedTimeToAnswer;
}
