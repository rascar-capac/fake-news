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

    public int TrustLevel { get; set; }
    public int ParanoiaLevel { get; set; }
    public int ContaminationLevel { get; set; }
    public int CasualtiesCount { get; set; }


    private float timer;

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
