using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingTextBehavior : MonoBehaviour {

    public Animator animator;
    private Text damageText;

	// Use this for initialization
	void Awake () {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Debug.Log(animator.GetCurrentAnimatorClipInfo(0));
        Destroy(gameObject, clipInfo[0].clip.length);
        damageText = animator.GetComponent<Text>();
	}   
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetText(string text)
    {
        damageText.text = text;
    }
}
