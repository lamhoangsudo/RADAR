using System;
using UnityEngine;

public class LamStats
{
    public event EventHandler OnStatsChange;
    public static float MIN_STAT_VALUE = 0;
    public static float MAX_STAT_VALUE = 20;
    public enum StatType
    {
        Attack,
        Defence,
        Speed,
        Mana,
        Health
    }
    private SingleStat attackStat;
    private SingleStat defenceStat;
    private SingleStat speedStat;
    private SingleStat manaStat;
    private SingleStat healthStat;

    public LamStats(float attackValue, float defenceValue, float speedValue, float manaVaule, float healthVaule)
    {
        attackStat = new SingleStat(attackValue);
        defenceStat = new SingleStat(defenceValue);
        speedStat = new SingleStat(speedValue);
        manaStat = new SingleStat(manaVaule);
        healthStat = new SingleStat(healthVaule);
    }
    private SingleStat GetSingleStatType(StatType type)
    {
        return type switch
        {
            StatType.Attack => attackStat,
            StatType.Defence => defenceStat,
            StatType.Speed => speedStat,
            StatType.Mana => manaStat,
            StatType.Health => healthStat,
            _ => null,
        };
    }
    public void SetStat(float stat, StatType type)
    {
        GetSingleStatType(type).SetStat(stat);
        OnStatsChange?.Invoke(this, EventArgs.Empty);
    }
    public void IncreaseStatAmount(StatType type)
    {
        SetStat(GetSingleStatType(type).GetStat() + 1, type);
    }
    public void DecreaseStatAmount(StatType type)
    {
        SetStat(GetSingleStatType(type).GetStat() - 1, type);
    }
    public void RandomStatValue(StatType type)
    {
        SetStat(GetSingleStatType(type).RandomStats(), type);
    }
    public float GetStat(StatType type)
    {
        return GetSingleStatType(type).GetStat();
    }
    public float GetStatNormalized(StatType type)
    {
        return GetSingleStatType(type).GetStatNormalized();
    }
    private class SingleStat
    {
        private float stat;

        public SingleStat(float stat)
        {
            this.stat = stat;
        }

        public void SetStat(float stat)
        {
            //input value [min, max]
            // = if(min < value < max) {}
            this.stat = Mathf.Clamp(stat, MIN_STAT_VALUE, MAX_STAT_VALUE);
        }
        public float GetStat()
        {
            return stat;
        }
        public float GetStatNormalized()
        {
            return (float)stat / MAX_STAT_VALUE;
        }
        public float RandomStats()
        {
            return UnityEngine.Random.Range(MIN_STAT_VALUE, MAX_STAT_VALUE);
        }
    }
}
