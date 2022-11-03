using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballCapsuleObstacle : MonoBehaviour, IPinballObstacle
{
    public float score { get; set; }
    public ScoreManager manager { get; set; }
    public bool isArenaEdge { get; set; }

    public GameObject scoreDisplayObject;

    public void Initialize(ScoreManager mgr, float score)
    {
        this.score = score * 2 * (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
        manager = mgr;
    }

    public void AddScore()
    {       
        manager.AddScore(score);

        GameObject scoreOb = Instantiate(scoreDisplayObject, transform.position + Vector3.up * (transform.lossyScale.y + 1) * 1.5f, Quaternion.identity);
        scoreOb.GetComponent<ScoreDisplay>().Initialize(score);
    }

    //public void Initialize(ScoreManager manager, float score)
    //{
    //    this.manager = manager;
    //    this.score = score;
    //}
}
