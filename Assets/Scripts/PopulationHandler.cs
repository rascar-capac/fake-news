using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopulationHandler : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private int startingTrustLevel = 0;
    // [SerializeField] [Range(0, 100)] private int startingParanoiaLevel = 0;
    // [SerializeField] [Range(0, 100)] private int startingContaminationLevel = 0;
    // [SerializeField] [Range(0, 100)] private int startingCasualtiesCount = 0;
    // [SerializeField] [Range(0, 1f)] private float randomContaminationVariationProbability = 0.1f;
    private int trustLevel;
    // private int paranoiaLevel;
    // private int contaminationLevel;
    // private int casualtiesCount;
    private TimeHandler timeHandler;
    private UnityEvent onTrustLevelChanged;
    // private UnityEvent onParanoiaLevelChanged;
    // private UnityEvent onContaminationLevelChanged;
    // private UnityEvent onCasualtiesCountChanged;
    // private UnityEvent onFullContamination;
    private UnityEvent onTrustNull;

    public int TrustLevel
    {
        get => trustLevel;
        set
        {
            if(value != trustLevel)
            {
                trustLevel = Mathf.Clamp(value, 0, 100);
                if(trustLevel == 0)
                {
                    onTrustNull.Invoke();
                }
                onTrustLevelChanged.Invoke();
            }
        }
    }
    // public int ParanoiaLevel
    // {
    //     get => paranoiaLevel;
    //     set
    //     {
    //         if(value != paranoiaLevel)
    //         {
    //             paranoiaLevel = Mathf.Clamp(value, 0, 100);
    //             onParanoiaLevelChanged.Invoke();
    //         }
    //     }
    // }
    // public int ContaminationLevel
    // {
    //     get => contaminationLevel;
    //     set
    //     {
    //         if(value != contaminationLevel)
    //         {
    //             contaminationLevel = Mathf.Clamp(value, 0, 100);
    //             if(contaminationLevel == 100)
    //             {
    //                 onFullContamination.Invoke();
    //             }
    //             onContaminationLevelChanged.Invoke();
    //         }
    //     }
    // }
    // public int CasualtiesCount
    // {
    //     get => casualtiesCount;
    //     set
    //     {
    //         if(value != casualtiesCount)
    //         {
    //             casualtiesCount = Mathf.Clamp(value, 0, 100);
    //             onCasualtiesCountChanged.Invoke();
    //         }
    //     }
    // }
    public UnityEvent OnTrustLevelChanged => onTrustLevelChanged;
    // public UnityEvent OnParanoiaLevelChanged => onParanoiaLevelChanged;
    // public UnityEvent OnContaminationLevelChanged => onContaminationLevelChanged;
    // public UnityEvent OnCasualtiesCountChanged => onCasualtiesCountChanged;
    // public UnityEvent OnFullContamination => onFullContamination;
    public UnityEvent OnTrustNull => onTrustNull;

    private void Awake()
    {
        trustLevel = startingTrustLevel;
        // paranoiaLevel = startingParanoiaLevel;
        // contaminationLevel = startingContaminationLevel;
        // casualtiesCount = startingCasualtiesCount;
        timeHandler = GetComponent<TimeHandler>();
        onTrustLevelChanged = new UnityEvent();
        // onParanoiaLevelChanged = new UnityEvent();
        // onContaminationLevelChanged = new UnityEvent();
        // onCasualtiesCountChanged = new UnityEvent();
        // onFullContamination = new UnityEvent();
        onTrustNull = new UnityEvent();
    }

    private void Start()
    {
        // timeHandler.OnTimeChanged.AddListener(ApplyRandomContaminationVariation);
    }

    // private void ApplyRandomContaminationVariation()
    // {
    //     if(Random.value <= randomContaminationVariationProbability)
    //     {
    //         ContaminationLevel += Random.Range(1, 10);
    //     }
    // }
}
