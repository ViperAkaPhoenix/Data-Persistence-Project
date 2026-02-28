using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField playerNameTextField;
    public TMP_Text bestScoreTextField;

    private void SetPlayerName()
    {
        string textValue = playerNameTextField.text;
        if(string.IsNullOrEmpty(textValue))
        {
            textValue = "Default player";
        }
        GameManager.Instance.playerName = textValue;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GameManager.Instance.LoadPlayerData();
        bestScoreTextField.text = GameManager.Instance.highScorePlayerName + " : " + GameManager.Instance.highScore;

    }

    public void StartNew()
    {
        SetPlayerName();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        GameManager.Instance.SavePlayerData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
