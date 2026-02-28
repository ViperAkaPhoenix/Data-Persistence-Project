using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    //public static MainManager Instance;
    
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text BestScoreText;
    public Text ScoreText;
    public Text CurrentPlayer;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;


    /*
    private void Awake()
    {
        // start of a new code. Destroy if the code wants to create a new instance if one already exists
        
        if (Instance != null)
        {

            Destroy(gameObject);
            return;
        }
        
        //Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        CurrentPlayer.text = GameManager.Instance.playerName;
        BestScoreText.text = "Best Score : " + GameManager.Instance.highScorePlayerName + " : " + GameManager.Instance.highScore;
    }

    private void Update()
    {
        
        // return to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }

        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);


        if(m_Points > GameManager.Instance.highScore)
        {
            Debug.Log("New high score! " + m_Points + " is more than " + GameManager.Instance.highScore);
            GameManager.Instance.highScore = m_Points;
            GameManager.Instance.highScorePlayerName = CurrentPlayer.text;
            BestScoreText.text = "Best Score : " + GameManager.Instance.highScorePlayerName + " : " + GameManager.Instance.highScore;
        }

    }


    
}
