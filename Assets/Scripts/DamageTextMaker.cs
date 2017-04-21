using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextMaker : MonoBehaviour {
    [SerializeField]
    Object DamageText;
    GameObject canvas;

    Text _text;
	void Start ()
    {
        canvas = GameObject.Find("Canvas");
	}
	
	public void TextEmit (string text,Vector3 pos)
    {
        GameObject tex = (GameObject)Instantiate(DamageText);
        _text = tex.GetComponent<Text>();

        Vector2 ScreenPos = Camera.main.WorldToScreenPoint(pos);

        tex.transform.SetParent(canvas.transform);
        tex.transform.position = ScreenPos;
        _text.text = text;
	}
}
