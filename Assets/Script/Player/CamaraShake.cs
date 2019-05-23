using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraShake : MonoBehaviour
{
    Vector3 originePos;
    public bool ShakeStart;
    float timer;
    float amount = 0.5f;
    float Endtime = 0.3f;
    float startShake = 0;
    // Start is called before the first frame update
    void Start()
    {
        originePos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShakeStart)
        {
            startShake += Time.deltaTime;
            if(startShake > 0.5f)
            {
                StartCoroutine(Shake(amount, Endtime));
            }
           
        }
    }

    IEnumerator Shake(float _Amount, float _EndTime)
    {
        while(timer <= _EndTime)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _Amount + originePos;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = originePos;
        timer = 0;
        ShakeStart = false;
        startShake = 0;
    }
}
