using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
	public int Width;
	public int Height;
	public float Size;
	private Vector3 OriginPosition;
	public Transform Parent;

	Grid GridMap;

    // Start is called before the first frame update
    void Start()
    {
		OriginPosition = new Vector3(-Width / 2 * Size, -Height / 2 * Size);
		//GridMap = new Grid(Width, Height, Size, OriginPosition,Parent);
    }
}
