using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 游戏中的漂浮动态提示
/// </summary>
public class Tips : UIWidget
{
    // public Image image;
    public Text tipsText;

    public void Init(string message)
    {
        tipsText.text = message;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.DOLocalMove(new Vector3(0, 100, 0), 1.8f).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        Destroy(gameObject);
    }
}