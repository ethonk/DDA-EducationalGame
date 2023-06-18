using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnswerManager : MonoBehaviour
{
    [SerializeField] private Transform answerA;
    [SerializeField] private Transform answerB;
    [SerializeField] private Transform answerC;
    
    private Dictionary<Transform, bool> _spawnSpotsDictionary;
    [SerializeField] private Transform spawnSpotsParent;

    public bool initialized = false;

    private void Awake()
    {
        //  - init
        _spawnSpotsDictionary = new Dictionary<Transform, bool>();
        
        //  - iterate spawns
        for (var i = 0; i < spawnSpotsParent.childCount; i++)
        {
            var childTransform = spawnSpotsParent.GetChild(i);
            _spawnSpotsDictionary.Add(childTransform, false);
        }

        initialized = true;
    }

    public void SetPositionsRandom()
    {
        if (!initialized) return;

        answerA.position = ChooseRandomSpawnSpot().position;
        answerB.position = ChooseRandomSpawnSpot().position;
        answerC.position = ChooseRandomSpawnSpot().position;
    }
    
    public Transform ChooseRandomSpawnSpot()
    {
        List<Transform> keysWithFalseValue = new List<Transform>();

        foreach (KeyValuePair<Transform, bool> kvp in _spawnSpotsDictionary)
        {
            if (!kvp.Value)
            {
                keysWithFalseValue.Add(kvp.Key);
            }
        }
        
        int randomIndex = Random.Range(0, keysWithFalseValue.Count);
        _spawnSpotsDictionary[keysWithFalseValue[randomIndex]] = true;
        return keysWithFalseValue[randomIndex];
    }

    public void SetAllSpawnSpotsFalse()
    {
        _spawnSpotsDictionary.Clear();
        //  - iterate spawns
        for (var i = 0; i < spawnSpotsParent.childCount; i++)
        {
            var childTransform = spawnSpotsParent.GetChild(i);
            _spawnSpotsDictionary.Add(childTransform, false);
        }
    }
}
