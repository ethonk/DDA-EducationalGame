using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Evaluation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private PlayerModel _playerModel;
    private LogWriter _logWriter;

    private void Awake()
    {
        _playerModel = FindObjectOfType<PlayerModel>();
        _logWriter = FindObjectOfType<LogWriter>();

        
        scoreText.text = string.Format("AI LEVEL RANK: {0}", _playerModel.aiDeterminedLevel.ToString("F2"));
    }

    public void AnswerDifficulty(int diff)
    {
        switch (diff)
        {
            case 0:
                string logText1 = string.Format(
                    "Player rated difficulty: {0}", "Too Easy");
                _logWriter.AppendToFile(logText1);
                break;
            
            case 1:
                string logText2 = string.Format(
                    "Player rated difficulty: {0}", "Just Right");
                _logWriter.AppendToFile(logText2);
                break;
            
            case 2:
                string logText3 = string.Format(
                    "Player rated difficulty: {0}", "Too Hard");
                _logWriter.AppendToFile(logText3);
                break;
        }

        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIndex);
    }
}
