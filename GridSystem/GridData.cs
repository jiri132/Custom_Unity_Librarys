[System.Serializable]
public class GridData
{
	public int X { get; }
	public int Y { get; }
	public float cellSize { get; }
	public float[] originPosition { get; }
	public int[,] GridMap { get; }
	//public TextMesh[,] DebugTexts { get; }
	//public GameObject[,] Objects { get; set; }

	public GridData(Grid grid)
	{
		//singel values
		X = grid.X;
		Y = grid.Y;
		cellSize = grid.cellSize;

		//vector 3 data
		originPosition = new float[3];
		originPosition[0] = grid.originPosition.x;
		originPosition[1] = grid.originPosition.y;
		originPosition[2] = grid.originPosition.z;

		//2d arrays
		GridMap = grid.GridMap;
		//DebugTexts = grid.DebugTexts;
		//Objects = grid.Objects;
	}
}
