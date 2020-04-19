using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationHandler : MonoBehaviour
{
    [SerializeField] [Range(0, int.MaxValue)] private int startingTrustLevel = 0;
    [SerializeField] [Range(0, int.MaxValue)] private int startingParanoiaLevel = 0;
    [SerializeField] [Range(0, int.MaxValue)] private int startingContaminationLevel = 0;
    [SerializeField] [Range(0, int.MaxValue)] private int startingCasualtiesCount = 0;

    public int TrustLevel { get; set; }
    public int ParanoiaLevel { get; set; }
    public int ContaminationLevel { get; set; }
    public int CasualtiesCount { get; set; }

    private void Start()
    {
        TrustLevel = startingTrustLevel;
        ParanoiaLevel = startingParanoiaLevel;
        ContaminationLevel = startingContaminationLevel;
        CasualtiesCount = startingCasualtiesCount;
    }
}
