using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour, IPinballObstacle
{
    public float score { get; set; }
    public ScoreManager manager { get; set; }
    public bool isArenaEdge { get; set; }

    public bool IsArenaEdge;

    private void Start()
    {
        isArenaEdge = IsArenaEdge;
    }
    public void AddScore()
    {
        manager.AddScore(score);
    }
}
