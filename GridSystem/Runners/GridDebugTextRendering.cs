using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridDebugTextRendering : Editor
{
	//create text in world
	public static TextMesh CreateWorldText(string name,string text, Transform parent = null, Vector3 localPosition = default(Vector3),float characterSize = .1f ,int fontsize = 40, Color color = new Color(), TextAnchor anchor = TextAnchor.UpperLeft,TextAlignment alignment = TextAlignment.Left, int sortingOrder = 0)
	{
		if (color == null) color = Color.white;

		return CreateWorldText(name,parent,text,localPosition,characterSize,fontsize,color,anchor,alignment,sortingOrder);
	}

	//create text in world
	public static TextMesh CreateWorldText(string name,Transform parent, string text, Vector3 localPosition,float characterSize ,int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder )
	{
		GameObject gameObject = new GameObject(name, typeof(TextMesh));
		Transform transform = gameObject.transform;
		transform.SetParent(parent, false);
		transform.localPosition = localPosition;
		TextMesh textMesh = gameObject.GetComponent<TextMesh>();
		textMesh.anchor = textAnchor;
		textMesh.alignment = textAlignment;
		textMesh.text = text;
		textMesh.characterSize = characterSize;
		textMesh.fontSize = fontSize;
		textMesh.color = color;
		textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
		return textMesh;
	}
}
