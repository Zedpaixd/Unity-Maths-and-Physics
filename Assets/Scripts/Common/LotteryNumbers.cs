using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteryNumbers : MonoBehaviour
{
    public List<int> winningNumbers = new List<int>();
    public List<int> heldNumbers = new List<int>();

    List<int> winners = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < winningNumbers.Count; i++)
        {
            if (heldNumbers.Contains(winningNumbers[i]))
            {
                Debug.Log($"Winner! {winningNumbers[i]}");
                winners.Add(winningNumbers[i]);
            }
        }

        if (winners.Count == 0)
            Debug.Log("Didnt win :(");
    }
}
