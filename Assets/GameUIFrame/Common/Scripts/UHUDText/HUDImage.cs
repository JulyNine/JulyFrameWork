using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDImage : MonoBehaviour
{
    public GridLayoutGroup grid;
    public List<Image> numImageList;

    public void Show(string _value, HUDType hudType)
    {
        int value = int.Parse(_value);
        List<int> numberList = new List<int>();
        if (value > 0)
        {
            while(value/10 > 0)
            {
                numberList.Add(value % 10);
                value = value / 10;
            }
            numberList.Add(value);
        }
        else
        {
            value = -value;
            while (value / 10 > 0)
            {
                numberList.Add(value % 10);
                value = value / 10;
            }
            numberList.Add(value);
        }

        switch (hudType)
        {
            case HUDType.HPReduce:
                numImageList[0].gameObject.SetActive(true);
                AtlasManager.Instance.SetSprite(numImageList[0],"atlas_number","Damage_jian");
                for (int i = 1; i < numImageList.Count; i++)
                {
                    if (i < numberList.Count + 1)
                    {
                        numImageList[i].gameObject.SetActive(true);
                        string name = "Damage_" + numberList[numberList.Count - i];
                        AtlasManager.Instance.SetSprite(numImageList[i], "atlas_number", name);
                    }
                    else
                        numImageList[i].gameObject.SetActive(false);
                }
                break;
            case HUDType.CritHPReduce:
                numImageList[0].gameObject.SetActive(true);
                AtlasManager.Instance.SetSprite(numImageList[0], "atlas_number","CritDamage_jian");
                numImageList[0].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
              //  numImageList[0].color = Color.red;
                for (int i = 1; i < numImageList.Count; i++)
                {
                    if (i < numberList.Count + 1)
                    {
                        numImageList[i].gameObject.SetActive(true);
                        string name = "CritDamage_" + numberList[numberList.Count - i];
                        AtlasManager.Instance.SetSprite(numImageList[i],"atlas_number", name);
                        numImageList[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        //      numImageList[i].color = Color.red;
                    }
                    else
                        numImageList[i].gameObject.SetActive(false);
                }
                break;
            case HUDType.HPRecover:
                numImageList[0].gameObject.SetActive(true);
               AtlasManager.Instance.SetSprite(numImageList[0], "atlas_number","RecoverHP_jia");
              //  numImageList[0].color = Color.green;
                for (int i = 1; i < numImageList.Count; i++)
                {
                    if (i < numberList.Count + 1)
                    {
                        numImageList[i].gameObject.SetActive(true);
                        string name = "RecoverHP_" + numberList[numberList.Count - i];
                        AtlasManager.Instance.SetSprite(numImageList[i], "atlas_number",name);
                        //       numImageList[i].color = Color.green;
                    }
                    else
                        numImageList[i].gameObject.SetActive(false);
                }
                break;
            case HUDType.MPRecover:
                numImageList[0].gameObject.SetActive(true);
                AtlasManager.Instance.SetSprite(numImageList[0], "atlas_number","RecoverMP_jia");
               // numImageList[0].color = Color.blue;
                for (int i = 1; i < numImageList.Count; i++)
                {
                    if (i < numberList.Count + 1)
                    {
                        numImageList[i].gameObject.SetActive(true);
                        string name = "RecoverMP_" + numberList[numberList.Count - i];
                        AtlasManager.Instance.SetSprite(numImageList[i], "atlas_number", name);
                        //       numImageList[i].color = Color.blue;
                    }
                    else
                        numImageList[i].gameObject.SetActive(false);
                }
                break;
        }



        
    }


	// Use this for initialization
	void Start ()
    {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
