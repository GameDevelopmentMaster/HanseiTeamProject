using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public Animator BoomAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(transform.GetChild(0).name != "EnemyImage")
        {
            BoomAnimation.gameObject.SetActive(true);   
            BoomAnimation.Play(1);
            for (int i=0; i<transform.childCount; i++)
            {
                if(transform.GetChild(i).gameObject.activeSelf == true && BoomAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime <1.0f)
                {
                    return;
                }
                if (i == transform.childCount-1)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            BoomAnimation.gameObject.transform.position = transform.GetChild(0).position;
        }
    }
}
