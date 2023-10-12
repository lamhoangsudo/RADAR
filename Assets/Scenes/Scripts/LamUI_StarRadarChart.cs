using TMPro;
using UnityEngine;

public class LamUI_StarRadarChart : MonoBehaviour
{
    private LamStats stats;
    [SerializeField] private Transform radarMesh;
    [SerializeField] private Material material;
    private readonly float attackSize = 242.5f;
    private CanvasRenderer canvasRenderer;
    [SerializeField] private GameObject atk;
    [SerializeField] private GameObject def;
    [SerializeField] private GameObject spd;
    [SerializeField] private GameObject mana;
    [SerializeField] private GameObject health;
    private void Awake()
    {
        canvasRenderer = radarMesh.GetComponent<CanvasRenderer>();
    }
    private void Start()
    {
        UpdateUI();
    }
    public void SetStar(LamStats stats)
    {
        this.stats = stats;
        stats.OnStatsChange += Stats_OnStatsChange;
    }

    private void Stats_OnStatsChange(object sender, System.EventArgs e)
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        Mesh mesh = new Mesh();
        int centerVertice = 0;
        int attackVertice = 1;
        int defendVertice = 2;
        int speedVertice = 3;
        int manaVertice = 4;
        int healthVertice = 5;



        Vector3[] vertices = new Vector3[6];
        Vector2[] uv = new Vector2[6];
        int[] triangles = new int[3 * 5];

        Vector3 attackVector = CalculateVectorAngle(attackVertice, LamStats.StatType.Attack);
        Vector3 defendVector = CalculateVectorAngle(defendVertice, LamStats.StatType.Defence);
        Vector3 speedVector = CalculateVectorAngle(speedVertice, LamStats.StatType.Speed);
        Vector3 manaVector = CalculateVectorAngle(manaVertice, LamStats.StatType.Mana);
        Vector3 healthVector = CalculateVectorAngle(healthVertice, LamStats.StatType.Health);
        vertices[centerVertice] = Vector3.zero;
        vertices[attackVertice] = attackVector;
        vertices[defendVertice] = defendVector;
        vertices[speedVertice] = speedVector;
        vertices[manaVertice] = manaVector;
        vertices[healthVertice] = healthVector;

        triangles[0] = centerVertice;
        triangles[1] = attackVertice;
        triangles[2] = defendVertice;

        triangles[3] = centerVertice;
        triangles[4] = defendVertice;
        triangles[5] = speedVertice;

        triangles[6] = centerVertice;
        triangles[7] = speedVertice;
        triangles[8] = manaVertice;

        triangles[9] = centerVertice;
        triangles[10] = manaVertice;
        triangles[11] = healthVertice;

        triangles[12] = centerVertice;
        triangles[13] = healthVertice;
        triangles[14] = attackVertice;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        canvasRenderer.SetMesh(mesh);
        canvasRenderer.SetMaterial(material, null);

        atk.GetComponent<TextMeshProUGUI>().SetText(stats.GetStat(LamStats.StatType.Attack).ToString());
        def.GetComponent<TextMeshProUGUI>().SetText(stats.GetStat(LamStats.StatType.Defence).ToString());
        spd.GetComponent<TextMeshProUGUI>().SetText(stats.GetStat(LamStats.StatType.Speed).ToString());
        mana.GetComponent<TextMeshProUGUI>().SetText(stats.GetStat(LamStats.StatType.Mana).ToString());
        health.GetComponent<TextMeshProUGUI>().SetText(stats.GetStat(LamStats.StatType.Health).ToString());
    }
    public Vector3 CalculateVectorAngle(int type, LamStats.StatType statType)
    {
        float angleIncrement = 360f / 5;
        return Quaternion.Euler(0, 0, -angleIncrement * (type - 1)) * Vector3.up * attackSize * stats.GetStatNormalized(statType);
    }
}
