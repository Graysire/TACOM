using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstruction : MonoBehaviour
{
    public int cover;
    public PathGrid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid.WorldToNode(transform.position).coverBonus = cover;
        if (cover > 5)
        {
            grid.WorldToNode(transform.position).isSightObstructed = true;
        }
        if (cover > 2)
        {
            grid.WorldToNode(transform.position).isMoveObstructed = true;
        }
    }
}
