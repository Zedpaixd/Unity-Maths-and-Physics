using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballCubeObstacle : MonoBehaviour, IPinballObstacle
{
    public float score { get; set; }
    public ScoreManager manager { get; set; }

    public bool isArenaEdge { get; set; }

    public GameObject scoreDisplayObject;
    public void Initialize(ScoreManager mgr, float score)
    {
        this.score = score / ((transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3);
        manager = mgr;
    }

    public void AddScore()
    {
        manager.AddScore(score);

        GameObject scoreOb = Instantiate(scoreDisplayObject, transform.position + Vector3.up * (transform.localScale.y + 1) * 4, Quaternion.identity);
        scoreOb.GetComponent<ScoreDisplay>().Initialize(score);
    }

    //public void Initialize(ScoreManager manager, float score)
    //{
    //    this.manager = manager;
    //    this.score = score;
    //}
}
