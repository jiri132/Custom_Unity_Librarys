using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[System.Serializable]
public class Grid
{
	//all variables for the grid
	public int X { get; private set; }
	public int Y { get; private set; }
	public float cellSize { get; private set; }
	public Vector3 originPosition { get; private set; }
	public int[,] GridMap { get; private set; }
	public TextMesh[,] DebugTexts { get; private set; }
	public GameObject[,] Objects { get; set; }
	public Transform parent { get; }
	private bool SpawnIndex;

	//the grid his 
	public Grid(int X, int Y, float cellSize, Vector3 originPosition, Transform parent, bool SpawnIndex)
	{
		this.X = X;
		this.Y = Y;
		this.cellSize = cellSize;
		this.originPosition = originPosition;
		this.parent = parent;
		this.SpawnIndex = SpawnIndex;

		GridMap = new int[this.X, this.Y];
		DebugTexts = new TextMesh[this.X, this.Y];
		Objects = new GameObject[this.X, this.Y];

		if (SpawnIndex)
		{
			for (int y = 0; y < GridMap.GetLength(1); y++)
			{
				for (int x = 0; x < GridMap.GetLength(0); x++)
				{
					DebugTexts[x, y] = GridDebugTextRendering.CreateWorldText($"[ {x} , {y} ] Object Data", GridMap[x, y].ToString(), parent, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, cellSize / 2, 20, Color.white, TextAnchor.MiddleCenter);
					//Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
					//Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
					//Debug.Log($"GridPos [{x} , {y}]");
					//Thread.Sleep(1);
				}
			}
		}
		//Debug.DrawLine(GetWorldPosition(0, Y), GetWorldPosition(X, Y), Color.white, 100f);
		//Debug.DrawLine(GetWorldPosition(X, 0), GetWorldPosition(X,Y), Color.white, 100f);
	}


	#region Save System Grid
	public static void _SaveGrid(Grid grid, string worldName)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + $"/{worldName}-GridData.jai";
		FileStream stream = new FileStream(path, FileMode.Create);

		GridData data = new GridData(grid);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static GridData _LoadGrid(string worldName)
	{
		string path = Application.persistentDataPath + $"/{worldName}-GridData.jai";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			GridData data = formatter.Deserialize(stream) as GridData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}
	#endregion

	#region SAVE / LOAD / RELOAD
	public void SaveGrid(string worldName)
	{
		_SaveGrid(this, worldName);
	}
	public void LoadGrid(string worldName)
	{
		GridData data = _LoadGrid(worldName);


		//singel values
		X = data.X;
		Y = data.Y;
		cellSize = data.cellSize;

		//vector 3 data
		Vector3 originPosition;
		originPosition.x = data.originPosition[0];
		originPosition.y = data.originPosition[1];
		originPosition.y = data.originPosition[2];
		originPosition = this.originPosition;

		//2d arrays
		GridMap = data.GridMap;
		//Debug.Log(GridMap.GetLength(0));
		//DebugTexts = data.DebugTexts;
		//Objects = data.Objects;

		ReloadMap(SpawnIndex);
	}
	private void ReloadMap(bool spawnWithIndexes)
	{
		for (int y = 0; y < GridMap.GetLength(1); y++)
		{
			for (int x = 0; x < GridMap.GetLength(0); x++)
			{
				//DebugTexts[x, y] = GridDebugTextRendering.CreateWorldText($"[ {x} , {y} ] Object Data", GridMap[x, y].ToString(), parent, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, cellSize / 2, 20, Color.white, TextAnchor.MiddleCenter);
				if (spawnWithIndexes)
					DebugTexts[x, y].text = GridMap[x, y].ToString();

				if (GridMap[x, y] != 0)
				{
					Objects[x, y] = GameObject.Instantiate(GameManager.Instance.ItemIndexes[GridMap[x, y]], GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, Quaternion.identity, parent);
				}
			}
		}
	}
	#endregion

	#region REUSABLE FUNCTIONS FOR LOGIC
	public Vector3 GetWorldPosition(int x, int y)
	{
		return new Vector3(x, y) * cellSize + originPosition;
	}

	public void GetXY(Vector3 WorldPosition,out int x, out int y)
	{
		x = Mathf.FloorToInt((WorldPosition - originPosition).x / cellSize);
		y = Mathf.FloorToInt((WorldPosition - originPosition).y / cellSize);
	}
	
	public void SetValue(int x, int y, int value, bool SpawnIndex)
	{
		if (x >= 0 && y >= 0 && x < X && y < Y)
		{
			GridMap[x, y] = value;
			if (SpawnIndex)
				DebugTexts[x, y].text = GridMap[x, y].ToString();
		}
	}

	public void SetValue(Vector3 worldPosition, int value, bool SpawnIndex)
	{
		int x, y;
		GetXY(worldPosition, out x, out y);
		SetValue(x,y, value, SpawnIndex);
	}

	public int GetValue(int x,int y)
	{
		if (x >= 0 && y >= 0 && x < X && y < Y)
		{
			return GridMap[x, y];
		}else
		{
			return 0;
		}
	}
	public int GetValue(Vector3 worldPosition)
	{
		int x, y;
		GetXY(worldPosition, out x, out y);
		return GetValue(x, y);
	}
	#endregion

}
