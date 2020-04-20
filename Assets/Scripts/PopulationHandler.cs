using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationHandler : MonoBehaviour
{
    [SerializeField] [Range(0, int.MaxValue)] private int startingTrustLevel = 0;
    [SerializeField] [Range(0, int.MaxValue)] private int startingParanoiaLevel = 0;
    [SerializeField] [Range(0, int.MaxValue)] private int startingContaminationLevel = 0;
    [SerializeField] [Range(0, int.MaxValue)] private int startingCasualtiesCount = 0;
    [SerializeField] [Range(0, 1f)] private float randomContaminationVariationProbability = 0.1f;
    [SerializeField] [Range(0, 10f)] private float tickPeriod = 0.5f;
    private int _trustLevel;
    private int _paranoiaLevel;
    private int _contaminationLevel;
    private int _casualtiesCount;
    private float timer;

    public int TrustLevel
    {
        get => _trustLevel;
        set => _trustLevel = Mathf.Clamp(value, 0, 100);
    }
    public int ParanoiaLevel
    {
        get => _paranoiaLevel;
        set => _paranoiaLevel = Mathf.Clamp(value, 0, 100);
    }

    public int ContaminationLevel
    {
        get => _contaminationLevel;
        set => _contaminationLevel = Mathf.Clamp(value, 0, 100);
    }

    public int CasualtiesCount
    {
        get => _casualtiesCount;
        set => _casualtiesCount = Mathf.Clamp(value, 0, 100);
    }

    private void Start()
    {
        TrustLevel = startingTrustLevel;
        ParanoiaLevel = startingParanoiaLevel;
        ContaminationLevel = startingContaminationLevel;
        CasualtiesCount = startingCasualtiesCount;

        timer = tickPeriod;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if(Random.value <= randomContaminationVariationProbability)
            {
                ApplyRandomContaminationVariation();
            }
            timer = tickPeriod;
        }
    }

    private void ApplyRandomContaminationVariation()
    {
        ContaminationLevel += Random.Range(1, 10);
    }
}
