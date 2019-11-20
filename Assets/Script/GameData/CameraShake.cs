using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originePos;
    public bool ShakeStart;
    float timer;
     float amount = 0.3f;
     float Endtime = 0.3f;
    float startShake = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ShakeStart)
        {
            originePos = transform.position;
            startShake += Time.deltaTime;
            if(startShake > 0.5f)
            {
                StartCoroutine(Shake(amount, Endtime));
            }
           
        }
    }
    public void SetAmount(float Value)
    {
        amount = Value;
    }
    IEnumerator Shake(float _Amount, float _EndTime)
    {
        while(timer <= _EndTime)
        {
            transform.position = (Vector3)Random.insideUnitCircle * _Amount + originePos;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = originePos;
        timer = 0;
        ShakeStart = false;
        startShake = 0;
    }
}
