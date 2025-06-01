using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private List<TextMeshProUGUI> _listHighScoreTxt;
    private string _loadedData;

    private void OnEnable()
    {
        ShowHighScores();
    }

    public void ShowHighScores()
    {
        string loadedData = HighScore.Load("save");
        SaveData data;

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

        int count = data.HighScores.Count;

        if (count == 0)
        {
            for(int i = 0; i < 5; i++)
            {
                _listHighScoreTxt[i].text = "High score " + i + " : 0";
            }
        }
        else
        {

            for (int i = 0; i < 5; i++)
            {
                if (i < count)
                {
                    _listHighScoreTxt[i].text = "High score " + i + " :" + data.HighScores[i].ToString();
                }
                else
                {
                    _listHighScoreTxt[i].text = "High score " + i +" : 0";
                }
            }
        }
    }
}
