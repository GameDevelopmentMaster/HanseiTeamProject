using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHP : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Image>().fillAmount = transform.GetComponentInParent<CharacterParent>().GetGameData().Hp/ transform.GetComponentInParent<CharacterParent>().GetMaxHp();
    }
}
