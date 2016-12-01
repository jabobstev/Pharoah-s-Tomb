using UnityEngine;
using System.Collections;

public class FloatingTextController : MonoBehaviour {

    public GameObject popupText;
    public GameObject canvas;

    public void CreateFloatingText(string text, Transform location, Color? textColor = null, Color? outlineColor = null)
    {   
        GameObject instance = Instantiate(popupText);
        FloatingTextBehavior ft = instance.GetComponent<FloatingTextBehavior>();
        Vector2 screenPos = Camera.main.WorldToScreenPoint(location.position);
        ft.transform.SetParent(canvas.transform, false);
        ft.transform.position = screenPos;
        ft.SetText(text);
        ft.SetColor(textColor ?? new Color32(255, 185, 0, 255), outlineColor ?? new Color32(255, 124, 0, 255));
    }
}
