using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(TileData))]
public class CustomTileData : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
        
        SerializedProperty x = property.FindPropertyRelative("x");
        SerializedProperty y = property.FindPropertyRelative("y");
        SerializedProperty INDEX = property.FindPropertyRelative("INDEX");

        Rect newPosition = position;
        newPosition.y += y.arraySize * 25;

        for (int Y = 0; Y < y.arraySize; Y++)
        {
            newPosition.x = 30;
            newPosition.height = 20;
            newPosition.width = 20;

            for (int X = 0; X < x.arraySize; X++)
            {
                
                
                EditorGUI.PropertyField(newPosition, INDEX.GetArrayElementAtIndex(Y*x.arraySize + X), GUIContent.none);
                newPosition.x += newPosition.width + 5;
            }
            newPosition.x = position.x;
            newPosition.y -= 25;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty y = property.FindPropertyRelative("y");
        return 25 * y.arraySize + 20;
    }
}
[CustomPropertyDrawer(typeof(TileData))]
public class CustomTileDataInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

       
        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}

[System.Serializable]
public class TileData 
{

    //public TileData(int w, int h) { rows = new rowData[w * h]; }

    public TileData(int[,] grid)
    {
        x =  new int[grid.GetLength(0)];
        y = new int[grid.GetLength(1)];

        INDEX = new int[x.Length * y.Length];
    }

    public int[] x;
    public int[] y;
    public int[] INDEX;

}