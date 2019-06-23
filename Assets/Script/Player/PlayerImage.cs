using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerImage : MonoBehaviour
{

    public Image Hp;

    [SerializeField]
    Image[] SKils;

    Text BulletCountText;
    bool CoroutineCheck = false;
    bool CoroutineEndCheck = false;
    float MaxHp;
    // Start is called before the first frame update
    void Start()
    {
        BulletCountText = GameObject.Find("BulletCount").GetComponent<Text>();
        SKils = new Image[GameObject.Find("Skils").transform.childCount];
        for(int i=0; i<GameObject.Find("Skils").transform.childCount; i++)
        {
            SKils[i] = GameObject.Find("Skils").transform.GetChild(i).GetComponent<Image>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("PlayerImage") != null)
        {
            MaxHp = GameObject.Find("Player").GetComponent<CharacterParent>().GetMaxHp();
            Hp.fillAmount = GameObject.Find("Player").GetComponent<CharacterParent>().GetGameData().Hp / MaxHp;
        }
      for(int i=0; i<SKils.Length; i++)
        {
            
            switch (i)
            {
                case 0:
                    SKils[i].fillAmount += 0.5f * Time.deltaTime;
                    break;
                case 1:
                    SKils[i].fillAmount += 0.2f * Time.deltaTime;
                    break;
                case 3:
                    if(GameObject.Find("Player").transform.GetChild(0).GetComponent<PlayerBust>().GetCoolupgrade() == false)
                    {
                        SKils[i].fillAmount += 0.125f * Time.deltaTime;
                    }
                    else
                    {
                        SKils[i].fillAmount += 0.2f * Time.deltaTime;
                    }
                    break;
                case 4:
                    SKils[i].fillAmount += 0.14f * Time.deltaTime;
                    break;
            }
        }  
    }

    public void BulletRed(ref int BulletCount)
    {
        StartCoroutine(IBulletRed());
        if (CoroutineEndCheck)
        {
            BulletCount = 50;
            CoroutineEndCheck = false;
            CoroutineCheck = false;
        }
    }

    IEnumerator IBulletRed()
    {
        if (CoroutineCheck)
        {
            yield break;
        }
        CoroutineCheck = true;
        Color Red = new Color(255,0,0)/1000;
        for(int i=0; i<10; i++)
        {
            BulletCountText.color += Red;
            yield return new WaitForSeconds(0.1f);
        }
        for(int i=0; i<10; i++)
        {
            BulletCountText.color -= Red;
            yield return new WaitForSeconds(0.1f);
        }
        CoroutineEndCheck = true;
    }
    
    public void BulletText(string String)
    {
        BulletCountText.text = " x "  +String;
    }

    public Image ReturnSkil(int Index)
    {
        return SKils[Index];
    }

    public void SetMaxHp(float Value)
    {
        MaxHp = Value;
    }
}
