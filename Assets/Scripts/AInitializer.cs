using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class AInitializer<T> : MonoBehaviour where T : AData
{
    [SerializeField] private TextMeshProUGUI text = null;

    public T Data { get; set; }

    public virtual void Init()
    {
        text.SetText(Data.text);
    }
}
