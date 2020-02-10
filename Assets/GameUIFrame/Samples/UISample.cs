

using UnityEngine;

public class UISample : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OpenUIWindow(UIWindows.LoginWindow);
    }
    
}
