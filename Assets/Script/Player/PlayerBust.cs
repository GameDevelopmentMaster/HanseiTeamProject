using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBust : MonoBehaviour
{
    bool Coolupgrade;
    bool Atkupgrade;
    float CoolTime;
    [SerializeField]
    float PlayerSpeed;
    CharacterData chacterData;
    float CheckData;
    private void OnEnable()
    {
        CoolTime = 0;
        PlayerSpeed = transform.GetComponent<Player>().Speed;
        transform.GetComponent<Player>().SpeedSet(PlayerSpeed * 1.3f);
        chacterData = transform.GetComponentInParent<CharacterParent>().GetGameData();
        CheckData = chacterData.AtkSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        CoolTime += Time.deltaTime;
        if (Atkupgrade)
        {
            chacterData.AtkSpeed = CheckData+1;
            transform.GetComponentInParent<CharacterParent>().SetGameData(chacterData);
        }
        if(CoolTime > 2f)
        {
            if (Atkupgrade)
            {
                chacterData.AtkSpeed = CheckData - 1;
                transform.GetComponentInParent<CharacterParent>().SetGameData(chacterData);
            }
            transform.GetComponent<Player>().SpeedReturn();
            
            this.enabled = false;
        }
    }

    public void SetCoolUpgrade()
    {
        Coolupgrade = true;
    }
    public void SetAtkUpgrade()
    {
        Atkupgrade = true;
    }

    public bool GetCoolupgrade()
    {
        return Coolupgrade;
    }
}
