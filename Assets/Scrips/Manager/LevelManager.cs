using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject deathScreen;

    [SerializeField] private SaveData data;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private List<GameObject> canvasUI;
    [SerializeField] private Transform SettingPanel;

    public bool IsPause = false;

    

    public int Score
    {
        get {  return score; }
        set{ score = value; }
    }
    protected void Awake()
    {
        if (Instance == null) Instance = this;
        HighScore.Initialize();

        data = new SaveData();
        deathScreen.SetActive(false);
    }

    public void GameOver()
    {
        deathScreen.SetActive(true);
        foreach (var item in canvasUI)
        {
            item.SetActive(false);
        }
        //Inventory.Instance.gameObject.SetActive(false);
        scoreText.text = "Score: " + Score.ToString();

        string loadedData = HighScore.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<SaveData>(loadedData);
        }
        else
        {
            data = new SaveData();
        }

        if (data.HighScores == null)
            data.HighScores = new List<int>();

        bool inserted = false;
        for (int i = 0; i < data.HighScores.Count; i++)
        {
            if (score > data.HighScores[i])
            {
                data.HighScores.Insert(i, score);
                inserted = true;
                break;
            }
        }
        if (!inserted && data.HighScores.Count < 5)
        {
            data.HighScores.Add(score);
        }

        if (data.HighScores.Count > 5)
        {
            data.HighScores.RemoveRange(5, data.HighScores.Count - 5);
        }

        if (data.HighScores.Count > 0)
            highScoreText.text = "Highest score: " + data.HighScores[0].ToString();
        else
            highScoreText.text = "Highest score: " + score.ToString();

        string saveData = JsonUtility.ToJson(data);
        HighScore.Save("save", saveData);
    }

    public void ReplayGame()
    {
        Inventory.Instance.gameObject.SetActive(true);
        // Load lại scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Pause()
    {
        canvasUI[0].SetActive(true);
        Time.timeScale = 0;
        IsPause = true;
    }

    public void Resume()
    {
        canvasUI[0].SetActive(false);
        Time.timeScale = 1;
        IsPause = false;
    }

    public void OpenSettingPanal()
    {
        canvasUI[0].SetActive(false);
        SettingPanel.gameObject.SetActive(true);
    }

    public void BackToMainMenu(string name) {
        Inventory.Instance.gameObject.SetActive(false);
        SceneManager.LoadScene(name); }

    public void updateScore(int mount)
    {
        score += mount;
    }
}


[System.Serializable]
public class SaveData
{
    [SerializeField] private List<int> highScores;

    public List<int> HighScores
    {
        get { return highScores; }
        set { highScores = value; }
    }

    public SaveData()
    {
        highScores = new List<int>();
    }

    public SaveData(List<int> scores)
    {
        highScores = new List<int>(scores);
    }
}