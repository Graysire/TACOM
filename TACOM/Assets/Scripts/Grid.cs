using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f; //size of each square on the grid
    [SerializeField]
    private float yLayer = 0f; //height of the current portion of the grid
    [SerializeField]
    private int numGizmos = 4; //number of gizmos to draw in each direction

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xLocation = Mathf.RoundToInt(position.x / size);
        int zLocation = Mathf.RoundToInt(position.z / size);

        return new Vector3((float)xLocation * size, yLayer, (float)zLocation * size) + transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int x = 0; x < numGizmos; x++)
        {
            for (int z = 0; z < numGizmos; z++)
            {
                for (int y = 0; y < numGizmos; y++)
                {
                    Vector3 point = GetNearestPointOnGrid(new Vector3(x * size, 0f, z * size)) + new Vector3(0f,y * size, 0f);
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
    }
}
