using UnityEngine;
using System.Collections;

public class FloatingTextController : MonoBehaviour {

    private static FloatingTextBehavior popupText;
    private static GameObject canvas;
    private static bool initialized = false;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupText)
            popupText = Resources.Load<FloatingTextBehavior>("Prefab/PopupTextParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {   
        if (!initialized)
            Initialize();   
        FloatingTextBehavior instance = Instantiate(popupText);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPos;
        instance.SetText(text);
    }
}
