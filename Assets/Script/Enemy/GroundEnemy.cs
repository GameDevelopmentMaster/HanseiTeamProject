using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    float SkilCoolTime;
    float SkilTime;
    // Start is called before the first frame update
    void Start()
    {
        switch (transform.GetComponentInParent<CharacterParent>().Code)
        {
            case 1:
                SkilCoolTime = 2;
                break;
            case 4:
                SkilCoolTime = 1.5f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SkilTime > SkilCoolTime)
        {
            transform.parent.GetChild(1).transform.position = transform.position;
            transform.parent.GetChild(1).gameObject.SetActive(true);
            SkilTime = 0;
        }
        SkilTime += Time.deltaTime;
    }
}
