using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCheck : MonoBehaviour
{
    public StageManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetStatsName(string NameValue)
    {
        manager.SetStatsName(NameValue);

    }
    public void SetStageValue(int Value)
    {
        manager.SetStageValue(Value);
    }

    public void SceneEnd()
    {
        manager.SceneEnd();
    }
}
