using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Data
public enum Dir { Left, Right, Stop };

public enum CharacterList { PlayerCharacter, StopCharacter, GroundCharacter, AirCharacter, PotopCharacter, RunCharacter, Boss };

public enum DefList { Physics, Energy, };

public enum BossSkil { LeftDirBullet, DownDirBullet, CrossDirBullet };

public struct CharacterData
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


#region Character

public interface Character
{
        
    void CharacterInit(ref CharacterData Data);
}

 class AirCharacter : Character 
{
    int characterCode;
    public AirCharacter(int code)
    {
        characterCode = code;
    }

    public void CharacterInit(ref CharacterData character)
    {
        if(characterCode == 3)
        {
            character.Hp = 450;
            character.Damage = 5;
            character.SkilDamage = 100;
            character.FDefense[0] = 2;
            character.FDefense[1] = 4;
            character.Speed = 13 * 0.5f;
            character.AtkSpeed = 1 / 2f;

        }
        else if(characterCode == 6)
        {
            character.Hp = 550;
            character.Damage = 5.5f;
            character.SkilDamage = 175;
            character.FDefense[0] = 3;
            character.FDefense[1] = 6;
            character.Speed = 14 * 0.5f;
            character.AtkSpeed = 1 / 2f;
        }
        else if(characterCode == 10)
        {
            character.Hp = 700;
            character.Damage = 15;
            character.FDefense[0] = 2;
            character.FDefense[1] = 7;
            character.Speed = 15 * 0.5f;
            character.AtkSpeed = 1 / 2f;
        }
        
    }

    
}

class GroundCharacter : Character
{
    int characterCode;
   public GroundCharacter(int code)
    {
        characterCode = code;
    }
    public void CharacterInit(ref CharacterData character)
    {
        if (characterCode == 1)
        {
            character.Hp = 750;
            character.SkilDamage = 150;
            character.FDefense[0] = 4;
            character.FDefense[1] = 5;
            character.Speed = 7 / 0.5f;
            character.AtkSpeed = 1 / 3f;
        }
        else if (characterCode == 4)
        {
            character.Hp = 950;
            character.SkilDamage = 225;
            character.FDefense[0] = 5;
            character.FDefense[1] = 6;
            character.Speed = 8 * 0.5f;
            character.AtkSpeed = 1 / 3f;
        }
    }
}

class StopCharacter : Character
{
    int characterCode;
    
    public StopCharacter(int code)
    {
        characterCode = code;
    }

    public void CharacterInit(ref CharacterData character)
    {
        if (characterCode == 2)
        {
            character.Hp = 200;
            character.SkilDamage = 350;
            character.FDefense[0] = 2;
            character.FDefense[1] = 0;
        }
        else if (characterCode == 5)
        {
            character.Hp = 250;
            character.SkilDamage = 350;
            character.FDefense[0] = 3;
            character.FDefense[1] = 0;
        }
        else if (characterCode == 9)
        {
            character.Hp = 300;
            character.SkilDamage = 550;
            character.FDefense[0] = 3;
            character.FDefense[1] = 0;
        }
    }
}

class PotopCharacter : Character
{
    public PotopCharacter(ref CharacterData data)
    {
        CharacterInit(ref data);
    }
    public void CharacterInit(ref CharacterData character)
    {
        character.Hp = 1000;
        character.SkilDamage = 135;
        character.AtkSpeed = 0.3f / 3f;
        character.FDefense[0] = 6;
        character.FDefense[1] = 5;
    }
}

class RunCharacter : Character
{
    public RunCharacter(ref CharacterData data)
    {
        CharacterInit(ref data);
    }
    public void CharacterInit(ref CharacterData character)
    {
        character.Hp = 300;
        character.SkilDamage = 300;
        character.FDefense[0] = 10;
        character.FDefense[1] = 10;
        character.Speed = 10 * 0.5f;
    }

}
#endregion


public class Data : MonoBehaviour
{
    private float MaxHp;
    protected CharacterData GameData;
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
                AirCharacter air = new AirCharacter(Code);
                air.CharacterInit(ref GameData);
                break;
            case CharacterList.GroundCharacter:
                GroundCharacter ground = new GroundCharacter(Code);
                ground.CharacterInit(ref GameData);
                break;
            case CharacterList.StopCharacter:
                StopCharacter stop = new StopCharacter(Code);
                stop.CharacterInit(ref GameData);
                break;
            case CharacterList.PotopCharacter:
                PotopCharacter potop = new PotopCharacter(ref GameData); 
                break;
            case CharacterList.RunCharacter:
                RunCharacter run = new RunCharacter(ref GameData);
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
        MaxHp = GameData.Hp;

    }

    public void SetGameData(CharacterData Data)
    {
        GameData = Data;
    }

    public void SetShield(bool Value)
    {
        Shield = Value;
    }

    public CharacterData GetGameData()
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


