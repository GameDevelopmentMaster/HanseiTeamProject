using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
   protected Canvas UI;
    string Name;
    [SerializeField]
   protected Camera MainCamera;


    // Start is called before the first frame update

    private void Awake()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        SceneLoadData();
    }

    

    public void SceneLoadData()
    {
        UI = GameObject.Find("Canvas").GetComponent<Canvas>();
        if(MainCamera != null)
        {
            UI.worldCamera = MainCamera;
        }
    }

    //public void SetStage(string StageValue)
    //{
    //    if(SceneName() != "BossScene")
    //    {
    //        UI.transform.GetChild(4).GetComponent<Text>().text = "Stage : " + StageValue;
    //    }

    //}

    public void SetEnemyCount(string EnemyCountValue)
    {
        UI.transform.GetChild(3).GetComponent<Text>().text = "EnemyCount : " + EnemyCountValue;
    }

   

    public void StageEnd()
    {
        UI.transform.GetChild(5).gameObject.SetActive(true);
    }

    public void GameOver()
    {
        UI.transform.GetChild(6).gameObject.SetActive(true);
    }

    public void SetStatsName(string NameValue)
    {
        Name = NameValue ;
        
    }
    public void SetStageValue(int Value)
    {
        CharacterData SetData = GameObject.Find("Player").GetComponent<CharacterParent>().GetGameData(); ;
        switch (Name)
        {
            case "PhysicsDef":
                SetData.FDefense[0] += Value;
                GameObject.Find("Player").GetComponent<CharacterParent>().SetGameData(SetData);
                break;
            case "EnergyDef":
                SetData.FDefense[1] += Value;
                GameObject.Find("Player").GetComponent<CharacterParent>().SetGameData(SetData);
                break;
            case "AtkSpeed":
                SetData.AtkSpeed += Value;
                GameObject.Find("Player").GetComponent<CharacterParent>().SetGameData(SetData);
                break;
            case "MoveSpeed":
                SetData.Speed += Value;
                GameObject.Find("Player").GetComponent<CharacterParent>().SetGameData(SetData);
                break;
            case "120mmDemage":
                break;
            case "BoostAttackSpeed":
                GameObject.Find("Player").GetComponentInChildren<PlayerBust>().SetAtkUpgrade();
                break;
            case "HominMissileEnemy":
                for (int i = 1; i < 4; i++)
                {
                    GameObject.Find("PlayerImage").transform.GetChild(i).GetComponent<HomingMissile>().upgrade = true;
                }
                break;
            case "BoostCoolTime":
                GameObject.Find("Player").GetComponentInChildren<PlayerBust>().SetCoolUpgrade();
                break;
            case "UtiSkllCount":
                GameObject.Find("Player").GetComponent<PlayerSkil>().CountUp();
                break;
            case "Hp":
                SetData.Hp += 200;
                GameObject.Find("Player").GetComponent<CharacterParent>().SetGameData(SetData);
                    break;

        }
    }

    #region Camera Fade

    protected IEnumerator CameraFadeOut()
    {
        MainCamera.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        for (int i = 0; i < 10; i++)
        {
            MainCamera.transform.GetChild(0).GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.1f);
            yield return null;
        }
    }

    protected IEnumerator CameraFadeIn()
    {
        for (int i = 0; i < 10; i++)
        {
            MainCamera.transform.GetChild(0).GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f);
            yield return null;
        }
        MainCamera.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        SceneLoadData();
    }
    #endregion
}
