using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager manager;

    [SerializeField] private GameObject deathScreen;

    [SerializeField] private SaveData data;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    public int Score
    {
        get {  return score; }
        set{ score = value; }
    }
    private void Awake()
    {
        manager = this; //đảo bảo chỉ có một Level manager tồn tại (singleton)
        HighScore.Initialize();

        data = new SaveData(0);
    }

    public void GameOver()
    {
        deathScreen.SetActive(true);
        scoreText.text = "Score: " + Score.ToString();

        string loadedData = HighScore.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<SaveData>(loadedData);
        }

        highScoreText.text = "High score: " + data.HighScore.ToString();

        if (data.HighScore < score)
        {
            data.HighScore = score;
        }

        string saveData = JsonUtility.ToJson(data);
        HighScore.Save("save", saveData);
    }

    public void ReplayGame()
    {
        Debug.Log("I'm got calling");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void updateScore(int mount)
    {
        score += mount;
    }
}
[System.Serializable]
public class SaveData
{
    [SerializeField]  private int highScore;

    public int HighScore { get { return highScore; } set { highScore = value; } }
    SaveData() { highScore = 0; }
    public SaveData(int _hs)
    {
        highScore = _hs;
    }
}
