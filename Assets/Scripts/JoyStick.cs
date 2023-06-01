using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image jsContainer;
    private Image joystick;

    public Vector3 InputDirection;

    void Start()
    {

        jsContainer = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>(); 
        InputDirection = Vector3.zero;
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 position = Vector2.zero;

       
        RectTransformUtility.ScreenPointToLocalPointInRectangle
                (jsContainer.rectTransform,
                ped.position,
                ped.pressEventCamera,
                out position);

        position.x = (position.x / jsContainer.rectTransform.sizeDelta.x);
        position.y = (position.y / jsContainer.rectTransform.sizeDelta.y);

        float x = (jsContainer.rectTransform.pivot.x == 1f) ? position.x * 2 + 1 : position.x * 2 - 1;
        float y = (jsContainer.rectTransform.pivot.y == 1f) ? position.y * 2 + 1 : position.y * 2 - 1;

        InputDirection = new Vector3(position.x * 2 + 0, position.y * 2);
        InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

        
        joystick.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (jsContainer.rectTransform.sizeDelta.x / 3) , InputDirection.y * (jsContainer.rectTransform.sizeDelta.y) / 3);

    }

    public void OnPointerDown(PointerEventData ped)
    {

        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {

        InputDirection = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
    }
}