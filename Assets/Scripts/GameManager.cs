using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;
    public string highScorePlayerName;
    public int highScore;

    private void Awake()
    {
        // start of a new code. Destroy if the code wants to create a new instance if one already exists

        if (Instance != null)
        {

            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadPlayerData();
    }

    [System.Serializable]

    class SaveData
    {
        public string currentPlayerName;
        public string highScorePlayerName;
        public int highScore;
    }

    public void SavePlayerData()
    {
        SaveData data = new SaveData();
        data.currentPlayerName = playerName;
        data.highScorePlayerName = highScorePlayerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.currentPlayerName;
            highScorePlayerName = data.highScorePlayerName;
            highScore = data.highScore; 

        }
    }

}
