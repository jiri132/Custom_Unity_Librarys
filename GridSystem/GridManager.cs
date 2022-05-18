using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GRID_TYPE { CHOOSE_A_TYPE , EDITOR , TILEDATA };

public class GridManager : MonoBehaviour
{
	//Edit this if you want a bigger grid while USING @TILEDATA
	//[ X , Y ] cords
	static int[,] TileDataSize = new int[18, 10]; 


    #region TOUCH AT OWN RISK
	//type of grid
    public GRID_TYPE GRID_TYPE = GRID_TYPE.CHOOSE_A_TYPE;



	//basics for gridmap
	[Header("Grid Propertys")]
	public float Block_Size = 1f;
	public bool Centered = true;
	public Transform Parent;

	//debug informations
	[Header("Debug Options")]
	public bool debug_text = false;
	public bool debug_logs = false;

	//the position of the grid
	private Vector3 OriginPosition;

	//the grid size for building your own map inside of game
	[Header("Grid Size")]
	public int Width=0;
	public int Height=0;
	//a standard grid map always the same on enter
	public TileData TileData = new TileData(TileDataSize);

	Grid GridMap;

	// Start is called before the first frame update
	void Start()
	{
		if (GRID_TYPE == GRID_TYPE.TILEDATA)
			Width = TileData.x.Length;
			Height = TileData.y.Length;
		if (Centered) { OriginPosition = new Vector3(-Width / 2 * Block_Size, -Height / 2 * Block_Size); }


		if (Parent == null) { Parent = this.gameObject.transform; }

        switch (GRID_TYPE)
        {
            case GRID_TYPE.EDITOR:
				GridMap = new Grid(Width, Height, Block_Size, OriginPosition, Parent, debug_text);
				break;
            case GRID_TYPE.TILEDATA:
				GridMap = new Grid(TileData, Block_Size, OriginPosition, Parent, debug_text);
				break;
            default:
                break;
        }

		
		if (debug_logs)
			Debug.Log($"TileData 1D array length = {TileData.INDEX.Length}");
			Debug.Log($"total variables needed in 1D array = {Width*Height}");
	}
    #endregion
}
