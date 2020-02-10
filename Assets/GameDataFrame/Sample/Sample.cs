using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDataFrame;

public class Sample : MonoBehaviour {

    // Use this for initialization
    void Start () {

        GameDataManager.Instance.Init();

        CityDataSet cityDataSet = new CityDataSet();
        cityDataSet = cityDataSet.Load();
        Debug.Log("CityDataSet" + cityDataSet.GetCityDataByID(1).name);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
