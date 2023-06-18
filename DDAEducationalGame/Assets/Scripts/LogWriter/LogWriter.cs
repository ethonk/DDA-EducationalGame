using System;
using System.IO;
using UnityEngine;

public class LogWriter : MonoBehaviour
{
    [Header("File Settings")]
    [SerializeField] private string fileName = "PlayerLog.txt";
    [SerializeField] private string filePath;

    private void Awake()
    {
        //
        // Get file path

        filePath = GetFilePath();
    }

    private string GetFilePath()
    {
        string dataPath = Application.dataPath;
        string rootFolderPath = dataPath.Substring(0, dataPath.LastIndexOf("/", StringComparison.Ordinal));

        return Path.Combine(rootFolderPath, fileName);
    }

    public void AppendToFile(string text)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(text);
        }
    }

    public void ClearFile()
    {
        File.WriteAllText(filePath, string.Empty);
        
        // write over a new log
        AppendToFile("==========> NEW LOG: " + DateTime.Now);
        AppendToFile("");
    }
}
