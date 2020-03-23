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
    public GameObject ParentBricks;
    public GameObject BrickPrefub;

    public bool isGameOver = false;
    
    void Start()
    {
        
        GameOverPanel.SetActive(false);
        LoadLevel(CurrentLevel);
    }
    
    void Update()
    {
        ScoreText.text = Score.ToString();
        LivesText.text = Lives.ToString() + " LIVES";
    }

    public void LoadLevel(int levelId)
    {
        Debug.Log(levelId);
        string jsonPath = Application.dataPath + "/Levels/level" + levelId.ToString() + ".json";
        string jsonString = File.ReadAllText(jsonPath);
        Level level = JsonUtility.FromJson<Level>(jsonString);

        foreach (Vector3 position in level.Blocks)
        {
            var newBrick = Instantiate(BrickPrefub, position, Quaternion.identity);
            newBrick.transform.parent = ParentBricks.transform;
        }
    }
    public void AppendScore()
    {
        this.Score += ScoreStep;
        if (this.Score > this.HighScore) this.HighScore = this.Score;

    }

    public void RemoveLive()
    {
        this.Lives -= 1;
        
        if(this.Lives <= 0)
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
        this.Score = 0;
        this.Lives = 3;
        GameOverPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
