/// <summary>
/// Interface to be implemented by all pinball obstacles.
/// </summary>
public interface IPinballObstacle
{
    public abstract float score { get; set; }
    public abstract ScoreManager manager { get; set; }
    public abstract void AddScore();
    public abstract bool isArenaEdge { get; set; }
    public virtual void Initialize(ScoreManager scoreManager, float score) 
    {
        this.score = score;
        this.manager = scoreManager;
    }

}
