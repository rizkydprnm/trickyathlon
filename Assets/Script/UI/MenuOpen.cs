using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class MenuOpen : MonoBehaviour
{
    [SerializeField] private GameObject nextMenu;

    private GameObject thisMenu;

    public void Execute()
    {
        thisMenu = gameObject.transform.parent.gameObject;
        Sequence.Create()
            .ChainCallback(()=>GetComponent<Button>().interactable = false)
            .Chain(Tween.UIAnchoredPositionX(
                thisMenu.GetComponent<RectTransform>(), startValue: 0, endValue: 200, duration: 0.125f, Ease.OutCirc)
            )
            .ChainCallback(() =>
            {
                GetComponent<Button>().interactable = true;
                thisMenu.SetActive(false);
                nextMenu.SetActive(true);
            })
            .Chain(Tween.UIAnchoredPositionX(nextMenu.GetComponent<RectTransform>(), startValue: 200, endValue: 0, duration: 0.125f, Ease.InCirc));
    }
}
