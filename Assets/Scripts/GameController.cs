using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    public int Score = 0;
    public int Lives = 3;
    public int HighScore = 0;
    public int ScoreStep = 10;

    public int CurrentLevel = 2;

    public Text ScoreText;
    public Text LivesText;
    public Text FinishScoreText;
    public Text HighScoreText;

    public GameObject GameOverPanel;
    public GameObject PausePanel;

    public GameObject ParentBricks;
    public GameObject BrickPrefub;
    public Ball ball;

    public bool isGameOver = false;

    private int bricksCount = 0;

    void Start()
    {
        
        GameOverPanel.SetActive(false);
        LoadLevel(CurrentLevel);
    }
    
    void Update()
    {
        ScoreText.text = Score.ToString();
        LivesText.text = Lives.ToString() + " LIVES";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.Pause();
        }
    }

    public void LoadLevel(int levelId)
    {
        ball.Reset();
        DestroyDropDownObjects();

        if (this.ParentBricks)
        {
            foreach(Transform child in this.ParentBricks.transform)
            {
                Destroy(child.gameObject);
            }
        }
        string jsonPath = Application.streamingAssetsPath + "/Levels/level" + levelId.ToString() + ".json";
        string jsonString = File.ReadAllText(jsonPath);
        Level level = JsonUtility.FromJson<Level>(jsonString);

        foreach (Vector3 position in level.Blocks)
        {
            var newBrick = Instantiate(BrickPrefub, position, Quaternion.identity);
            newBrick.transform.parent = ParentBricks.transform;
        }

        this.bricksCount = level.Blocks.Count;        
    }
    public void AppendScore()
    {
        this.Score += ScoreStep;        
        if (this.Score > this.HighScore) this.HighScore = this.Score;

        this.bricksCount--;
        if(bricksCount <= 0)
        {            
            this.CurrentLevel++;
            this.LoadLevel(this.CurrentLevel);
        }

    }

    public void RemoveLive()
    {
        this.Lives -= 1;

        DestroyDropDownObjects();

        if (this.Lives <= 0)
        {
            this.FinishScoreText.text = "Score: " + this.Score.ToString();
            this.HighScoreText.text = "Highscore: " + this.HighScore.ToString();
            GameOverPanel.SetActive(true);
            this.isGameOver = true;
        }
    }

    public void Restart()
    {
        this.isGameOver = false;
        this.CurrentLevel = 1;
        this.Score = 0;
        this.Lives = 3;
        this.LoadLevel(this.CurrentLevel);
        GameOverPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        //this.PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        this.PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void DestroyDropDownObjects()
    {
        foreach(GameObject ddObject in GameObject.FindGameObjectsWithTag("DDObject"))
        {
            Destroy(ddObject);
        }
    }
}
