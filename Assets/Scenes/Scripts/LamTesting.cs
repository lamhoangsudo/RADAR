using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using UnityEngine.UI;
using TMPro;

public class LamTesting : MonoBehaviour
{
    [SerializeField] private LamUI_StarRadarChart chart;
    [SerializeField] private Transform ControlPanel;

    private Button atkInc;
    private Button atkDec;
    private Button defInc;
    private Button defDec;
    private Button spdInc;
    private Button spdDec;
    private Button manaInc;
    private Button manaDec;
    private Button healthInc;
    private Button healthDec;
    private Button random;

    private LamStats stats;
    private bool isRamdom;
    private float timeCount = 0;
    [SerializeField] private float timeMax;
    private void Awake()
    {
        atkInc = ControlPanel.Find("ATK++").GetComponent<Button>();
        atkDec = ControlPanel.Find("ATK--").GetComponent<Button>();
        defInc = ControlPanel.Find("DEF++").GetComponent<Button>();
        defDec = ControlPanel.Find("DEF--").GetComponent<Button>();
        spdInc = ControlPanel.Find("SPD++").GetComponent<Button>();
        spdDec = ControlPanel.Find("SPD--").GetComponent<Button>();
        manaInc = ControlPanel.Find("MANA++").GetComponent<Button>();
        manaDec = ControlPanel.Find("MANA--").GetComponent<Button>();
        healthInc = ControlPanel.Find("HEALTH++").GetComponent<Button>();
        healthDec = ControlPanel.Find("HEALTH--").GetComponent<Button>();
        random = ControlPanel.Find("Random").GetComponent<Button>();
        isRamdom = false;
    }
    private void Start()
    {
        stats = new(10, 10, 10, 10, 10);
        chart.SetStar(stats);
        //lambda action
        atkInc.onClick.AddListener(() => { stats.IncreaseStatAmount(LamStats.StatType.Attack); });
        atkDec.onClick.AddListener(() => { stats.DecreaseStatAmount(LamStats.StatType.Attack); });
        defInc.onClick.AddListener(() => { stats.IncreaseStatAmount(LamStats.StatType.Defence); });
        defDec.onClick.AddListener(() => { stats.DecreaseStatAmount(LamStats.StatType.Defence); });
        spdInc.onClick.AddListener(() => { stats.IncreaseStatAmount(LamStats.StatType.Speed); });
        spdDec.onClick.AddListener(() => { stats.DecreaseStatAmount(LamStats.StatType.Speed); });
        manaInc.onClick.AddListener(() => { stats.IncreaseStatAmount(LamStats.StatType.Mana); });
        manaDec.onClick.AddListener(() => { stats.DecreaseStatAmount(LamStats.StatType.Mana); });
        healthInc.onClick.AddListener(() => { stats.IncreaseStatAmount(LamStats.StatType.Health); });
        healthDec.onClick.AddListener(() => { stats.DecreaseStatAmount(LamStats.StatType.Health); });
        random.onClick.AddListener(() =>
        {
            if (isRamdom == false)
            {
                isRamdom = true;
            }
            else
            {
                isRamdom = false;
            }
        });
    }
    private void Update()
    {
        if (isRamdom)
        {
            timeCount = Time.deltaTime + timeCount;
            if (timeCount >= timeMax)
            {
                timeCount = 0;
                stats.RandomStatValue(LamStats.StatType.Attack);
                stats.RandomStatValue(LamStats.StatType.Defence);
                stats.RandomStatValue(LamStats.StatType.Speed);
                stats.RandomStatValue(LamStats.StatType.Mana);
                stats.RandomStatValue(LamStats.StatType.Health);
            }
        }

    }
}
