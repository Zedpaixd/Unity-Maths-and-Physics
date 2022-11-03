using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI remainingHitsText;
    public TextMeshProUGUI remainingHealthText;
    public GameObject victoryPanel;
    [SerializeField] private int remainingHits;
    [SerializeField] private int remainingHealth;
    private float totalScore = 0f;

    private void Start()
    {
        if (scoreText == null)
            scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

        remainingHealthText.SetText(remainingHealth.ToString());
        remainingHitsText.SetText(remainingHits.ToString());
        scoreText.SetText("0");
    }
    public void AddScore(float score)
    {
        totalScore += score;
        scoreText.SetText(totalScore.ToString("f2"));
        //subtract one hit
        remainingHits--;
        remainingHitsText.SetText(remainingHits.ToString());

        if (remainingHits <= 0)
        {
            FindObjectOfType<Aim>().enabled = false;
            //change victory panel text
            victoryPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"You win!\n Final Score {totalScore}.";
            //show victory panel
            victoryPanel.SetActive(true);
        }

        if (remainingHealth < 0)
        {
            FindObjectOfType<Aim>().enabled = false;
            victoryPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You lost.";
            victoryPanel.SetActive(true);
        }
    }

    public void SubtractScore(float score)
    {
        totalScore -= score;
        remainingHealth -= 1;
        remainingHealthText.SetText(remainingHealth.ToString());
        scoreText.SetText(totalScore.ToString("f2"));
        if (remainingHealth <= 0)
        {
            FindObjectOfType<Aim>().enabled = false;
            victoryPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You lost.";
            victoryPanel.SetActive(true);
        }
    }
}
