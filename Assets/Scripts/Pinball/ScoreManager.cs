using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class ScoreManager : MonoBehaviour
{
    public List<float> topTenScores;
    public TextMeshProUGUI scoresText;
    public TextMeshProUGUI currentScoreText;

    private float currentScore;

    void Start()
    {
        if (!scoresText)
            scoresText = GetComponent<TextMeshProUGUI>();

        UpdateTopTenScoresText();
    }

    public void AddScore(float score)
    {
        currentScore += score;
        UpdateCurrentScoreText();
    }

    public void EndRound()
    {
        topTenScores.Add(currentScore);

        //order by descending first, then remove duplicates with distinct, then take the first 10 elements
        topTenScores = topTenScores.OrderByDescending(x => x).Distinct().Take(10).ToList();

        UpdateTopTenScoresText();
    }


    void UpdateCurrentScoreText() 
    {
        currentScoreText.text = string.Format("Current score:\n{0}", currentScore.ToString("f0"));
    }

    void UpdateTopTenScoresText()
    {
        if (topTenScores.Count == 0)
            return;


        currentScore = 0;
        UpdateCurrentScoreText();

        string text = "Top ten scores:\n";
        for (int i = 0; i < topTenScores.Count; i++)
        {
            text += string.Format("{0}: {1}\n", i+1, topTenScores[i].ToString("f0"));
        }

        scoresText.text = text;
    }
}
