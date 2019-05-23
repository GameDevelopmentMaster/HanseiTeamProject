using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemList { TreepleDir, HomingMissile, Nulclear, }

public class Item : MonoBehaviour
{
    
    
   public static ItemList list;
    // Start is called before the first frame update
    void Start()
    {
        list = ItemList.TreepleDir;
       // int a = Random.Range(1, 3);
        //switch (a)
        //{
        //    case 1:
        //        list = ItemList.TreepleDir;
        //        break;
        //    case 2:
        //        list = ItemList.HomingMissile;
        //        break;
        //    case 3:
        //        list = ItemList.Nulclear;
        //        break;

        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
