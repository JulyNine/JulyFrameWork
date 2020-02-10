using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public GameObject uiRoot;
    public Camera uiCamera;
    public UIButton loginButton;
    Object obj2;
    // Use this for initialization
    void Start () {


        loginButton.Init();

        loginButton.onClick.AddListener(delegate ()
        {
            this.LoginButtonClick(this.gameObject);
        });

        string windowName = "loginwindowtest";
        string resourcePath = GlobalSystemData.UI_WINDOW_DIR;
        string resourceName = resourcePath + windowName;


        obj2 = Resources.Load(resourceName);
        GameObject obj = Instantiate(obj2) as GameObject; 

     //   GameObject obj = (GameObject.Instantiate(Resources.Load(resourceName, typeof(GameObject))) as GameObject);

        obj.transform.SetParent(uiRoot.transform);
        //设置camera
        Canvas[] canvases = obj.transform.GetComponentsInChildren<Canvas>();
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].worldCamera = uiCamera;
        }
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.localPosition = Vector3.zero;
        rect.localScale = Vector3.one;
        rect.sizeDelta = Vector2.zero;
        obj2 = null;
        //obj2 = null;
        //   obj = null;
        //    canvases = null;
        //     rect = null;

    }
    public void LoginButtonClick(GameObject go)
    {
        LogManager.Log("LoginButtonClick");
        Resources.UnloadUnusedAssets();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
