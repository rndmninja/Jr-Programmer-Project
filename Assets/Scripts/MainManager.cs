using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // DATA PERSISTENCE
    public static MainManager Instance;
    public Color TeamColor;
    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }

    private void Awake()
    {
        // DATA PERSISTENCE
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // DATA PERSISTENCE
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }
}
