using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public MapManager mapManager;
    public Vector2 worldPos;

    private bool mouseOver = false;
    private SpriteRenderer tileRenderer;
    private SpriteRenderer buildingRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        tileRenderer = GetComponent<SpriteRenderer>();
        buildingRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
    }

    // Update is called once per frame
    private void Update()
    {
        if (mouseOver && Input.GetMouseButton(0))
        {
            //Do something depending on mode
            if (mapManager.buildingSelected)
            {
                //Change sprite and update map
                buildingRenderer.sprite = mapManager.currentMaterial;
                mapManager.map[(int)worldPos.x, (int)worldPos.y].building = mapManager.currentMaterialId;
            }
            else
            {
                //Change sprite and update map
                tileRenderer.sprite = mapManager.currentMaterial;
                mapManager.map[(int)worldPos.x, (int)worldPos.y].tile = mapManager.currentMaterialId;
            }
        }
    }

    //Set the mouseOver boolean
    private void OnMouseEnter()
    {
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        mouseOver = false;
    }
}

//holds details about a tile
[System.Serializable]
public struct TileDetails
{
    public int tile, building;

    public TileDetails(int t, int b)
    {
        tile = t;
        building = b;
    }
}