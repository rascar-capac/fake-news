using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class AInitializer<T> : MonoBehaviour where T : AData
{
    [SerializeField] private List<T> datas = null;
    [SerializeField] private TextMeshProUGUI text = null;

    public T Data { get; set; }

    protected virtual void Init()
    {
        Data = datas[Random.Range(0, datas.Count)];
        text.SetText(Data.text);
    }
}
