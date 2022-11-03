using UnityEngine;
using TMPro;

public class ScoreDisplay : ManagedUpdateBehaviour
{
    public float lifeTime = 3f;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private MeshRenderer bg;
    private Material material;
    private Transform mainCam;
    private void Start()
    {
        UpdateManager.Instance.AddToList(this);
        mainCam = Camera.main.transform;

        material = bg.material;
    }

    public void Initialize(float score)
    {
        text.text = score.ToString("f0");
    }

    public override void OnUpdate()
    {
        transform.LookAt(mainCam);
        AdjustTransparency();

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            UpdateManager.Instance.RemoveFromList(this);
            Destroy(gameObject);
        }
    }

    private void AdjustTransparency()
    {
        float clamp01 = Mathf.Clamp(lifeTime, 0, 1);
        text.alpha = clamp01;

        Color bgColor = material.color;
        bgColor.a = clamp01;
        material.color = bgColor;
    }

    //destroy material and meshes to release memory
    private void OnDestroy()
    {
        //Destroy(material);

        //MeshFilter[] meshes = GetComponentsInChildren<MeshFilter>();
        //for (int i = 0; i < meshes.Length; i++)
        //{
        //    Mesh m = meshes[i].mesh;
        //    meshes[i] = null;
        //    Destroy(m);
        //}
        Resources.UnloadUnusedAssets();
    }
}
