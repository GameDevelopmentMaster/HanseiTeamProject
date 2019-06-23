using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParent : Data
{
    public Animator BoomAnimation;
    public int Code;
    public bool Check;
    // Start is called before the first frame update
    void Awake()
    {
        CharacterInit(GetCharacter(transform.gameObject),Code);
    }   

    // Update is called once per frame
   override public void Update()
    {
        base.Update();
        if (GameData.Hp < 0 && transform.GetChild(0).gameObject.activeSelf == true)
        {
            BoomAnimation.gameObject.transform.position = transform.GetChild(0).position;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        else if (transform.GetChild(0).gameObject.activeSelf  == false && GameData.Hp < 0)
        {
            BoomAnimation.gameObject.SetActive(true);   
            BoomAnimation.Play(1);
            for (int i=0; i<transform.childCount-1; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf == true || BoomAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime <1.0f)
                {
                    return;
                }
                if (i == transform.childCount-2)
                {
                    Destroy(this.gameObject);
                }
            }
            
        }
        
    }

    public void Damage(float Damage,DefList def)
    {
        HpAttack(Damage, def);
    }

    public void Move(float PosX)
    {
        StartCoroutine(Knock_BackMove(PosX));
    }

    IEnumerator Knock_BackMove(float PosX)
    {
        float Posx = PosX / 10f;
        Vector3 Pos = new Vector3(Posx, 0);
        for (int i = 0; i < 10; i++)
        {
            transform.GetChild(0).position += Pos;
            yield return null;
        }
    }
}
