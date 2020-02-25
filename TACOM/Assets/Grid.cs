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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            yLayer += size;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            yLayer -= size;
        }
        if (yLayer % size != 0)
        {
            yLayer = size * Mathf.RoundToInt(yLayer / size);
        }
    }



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
        for (int y = 0; y < numGizmos; y++)
        {
            if (y * size == yLayer)
            {
                Gizmos.color = Color.red;
            }
            for (int z = 0; z < numGizmos; z++)
            {
                for (int x = 0; x < numGizmos; x++)
                {
                    Vector3 point = new Vector3(x * size, y * size, z * size);
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
            if (y * size == yLayer)
            {
                Gizmos.color = Color.yellow;
            }
        }
    }
}
