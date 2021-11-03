using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class JoistickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image joistickbg;
    [SerializeField]
    private Image joistick;
    private Vector2 inputvector2;
    private void Awake()
    {
        joistickbg = GetComponent<Image>();
        joistick = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputvector2 = Vector2.zero;
        joistick.rectTransform.anchoredPosition = Vector2.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joistickbg.rectTransform,ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joistickbg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joistickbg.rectTransform.sizeDelta.x);

            inputvector2 = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
            inputvector2 = (inputvector2.magnitude > 1.0f) ? inputvector2.normalized : inputvector2;

            joistick.rectTransform.anchoredPosition = new Vector2(inputvector2.x *(joistickbg.rectTransform.sizeDelta.x/2), (inputvector2.y * (joistickbg.rectTransform.sizeDelta.y / 2)));
        }
    }
    public float Horizontal()
    {
        if (inputvector2.x != 0)
        {
            return inputvector2.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public float Vertical()
    {
        if (inputvector2.y != 0)
        {
            return inputvector2.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
