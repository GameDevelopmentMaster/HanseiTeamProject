using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Data
public class Data : MonoBehaviour
{
    private float MaxHp;
    protected ChacterData GameData;
    bool Shield;
    bool Check = false;
    float ShieldTime;
    public float HP;

    public void HpAttack(float Damage, DefList def)
    {
        if (Shield)
        {
            if (MaxHp == 18500)
            {
                GameObject.Find("Player").GetComponentInParent<CharacterParent>().Damage(Damage * 0.7f, def);
            }
        }
        else
        {
            GameData.Hp -= Damage * (1 - GameData.FDefense[(int)def] / 100);
        }
    }


    virtual public void Update()
    {
        HP = GameData.Hp;
        if (Shield)
        {
            switch (MaxHp)
            {
                case 1500:
                    if (GameObject.Find("Player").GetComponent<PlayerSkil>().GetSkil())
                    {
                        ShieldTime += Time.deltaTime;

                    }
                    else if (ShieldTime > 1f && GameObject.Find("Player").GetComponent<PlayerSkil>().GetSkil() == false)
                    {
                        ShieldTime = 0;
                        Shield = false;
                    }
                    break;
                case 18500:
                    ShieldTime += Time.deltaTime;
                    if(ShieldTime>2.5f)
                    {
                        ShieldTime = 0;
                        Shield = false;
                       StartCoroutine(GameObject.Find("Boss").GetComponent<BossSKill>().BossShieldEnd());
                    }
                    break;
            }

        }
        
    }

    protected void CharacterInit(CharacterList character, int Code)
    {
        GameData.FDefense = new float[2];
        switch (character)
        {
            case CharacterList.AirCharacter:
                if (Code == 3)
                {
                    MaxHp = GameData.Hp = 450;
                    GameData.Damage = 5;
                    GameData.SkilDamage = 100;
                    GameData.FDefense[0] = 2;
                    GameData.FDefense[1] = 4;
                    GameData.Speed = 13 * 0.5f;
                    GameData.AtkSpeed = 1 / 2f;
                }
                else if (Code == 6)
                {
                    MaxHp = GameData.Hp = 550;
                    GameData.Damage = 5.5f;
                    GameData.SkilDamage = 175;
                    GameData.FDefense[0] = 3;
                    GameData.FDefense[1] = 6;
                    GameData.Speed = 14 * 0.5f;
                    GameData.AtkSpeed = 1 / 2f;
                }
                else if (Code == 10)
                {
                    MaxHp = GameData.Hp = 700;
                    GameData.Damage = 15;
                    GameData.FDefense[0] = 2;
                    GameData.FDefense[1] = 7;
                    GameData.Speed = 15 * 0.5f;
                    GameData.AtkSpeed = 1 /2f;
                }
                break;
            case CharacterList.GroundCharacter:
                if (Code == 1)
                {
                    MaxHp = GameData.Hp = 750;
                    GameData.SkilDamage = 150;
                    GameData.FDefense[0] = 4;
                    GameData.FDefense[1] = 5;
                    GameData.Speed = 7 / 0.5f;
                    GameData.AtkSpeed = 1 / 3f;
                }
                else if (Code == 4)
                {
                    MaxHp = GameData.Hp = 950;
                    GameData.SkilDamage = 225;
                    GameData.FDefense[0] = 5;
                    GameData.FDefense[1] = 6;
                    GameData.Speed = 8 * 0.5f;
                    GameData.AtkSpeed = 1 / 3f;
                }
                break;
            case CharacterList.StopCharacter:
                if (Code == 2)
                {
                    MaxHp = GameData.Hp = 200;
                    GameData.SkilDamage = 350;
                    GameData.FDefense[0] = 2;
                    GameData.FDefense[1] = 0;
                }
                else if (Code == 5)
                {
                    MaxHp = GameData.Hp = 250;
                    GameData.SkilDamage = 350;
                    GameData.FDefense[0] = 3;
                    GameData.FDefense[1] = 0;
                }
                else if (Code == 9)
                {
                    MaxHp = GameData.Hp = 300;
                    GameData.SkilDamage = 550;
                    GameData.FDefense[0] = 3;
                    GameData.FDefense[1] = 0;
                }
                break;
            case CharacterList.PotopCharacter:
                MaxHp = GameData.Hp = 1000;
                GameData.SkilDamage = 135;
                GameData.AtkSpeed = 0.3f / 3f;
                GameData.FDefense[0] = 6;
                GameData.FDefense[1] = 5;
                break;
            case CharacterList.RunCharacter:
                MaxHp = GameData.Hp = 700;
                GameData.SkilDamage = 300;
                GameData.FDefense[0] = 10;
                GameData.FDefense[1] = 10;
                GameData.Speed = 10 * 0.5f;
                break;
            case CharacterList.PlayerCharacter:
                if(Check)
                {
                    GameData.Hp = MaxHp;
                }
                else
                {
                    GameData.Hp = MaxHp = 1500;
                    GameData.Speed = 10 * 0.5f;
                    GameData.Damage = 18.5f;
                    GameData.AtkSpeed = 8 / 3f;
                }
                Check = true;
                break;
            case CharacterList.Boss:
                GameData.Hp = MaxHp = 18500;
                GameData.FDefense[0] = 15;
                GameData.FDefense[1] = 15;
                break;
        }

    }

    public void SetGameData(ChacterData Data)
    {
        GameData = Data;
    }

    public void SetShield(bool Value)
    {
        Shield = Value;
    }

    public ChacterData GetGameData()
    {
        return GameData;
    }
    

    public float GetMaxHp()
    {
        return MaxHp;
    }

    public CharacterList GetCharacter(GameObject gameObject)
    {
        switch (gameObject.name)
        {
            case "AirEnemy":
                return CharacterList.AirCharacter;
            case "GroundEnemy":
                return CharacterList.GroundCharacter;
            case "MineEnemy":
                return CharacterList.StopCharacter;
            case "PotopEnemy":
                return CharacterList.PotopCharacter;
            case "RunEnemy":
                return CharacterList.RunCharacter;
            case "Player":
                return CharacterList.PlayerCharacter;
            case "Boss":
                return CharacterList.Boss;
            default:
                return (CharacterList)10;
        }
    }
}
public enum Dir { Left, Right, Stop };

public enum CharacterList { PlayerCharacter, StopCharacter, GroundCharacter, AirCharacter, PotopCharacter, RunCharacter, Boss };

public enum DefList { Physics, Energy, };

public enum BossSkil {LeftDirBullet,DownDirBullet,  CrossDirBullet };

public struct ChacterData
{
    public float[] FDefense;     // 0: 물리 1 : 에너지
    public float Hp;
    public float Speed;
    public float Damage;
    public float AtkSpeed;
    public float SkilSpeed;
    public float SkilDamage;

};
#endregion