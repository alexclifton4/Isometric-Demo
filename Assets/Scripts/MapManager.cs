using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Vector2 size;
    public GameObject tilePrefab;
    public Sprite[] materials;
    public Sprite[] buildings;

    public Sprite currentMaterial;
    public int currentMaterialId;
    public bool buildingSelected = false;
    public TileDetails[,] map;

    public TileDetails[,] Convert(int[,] map)
    {
        TileDetails[,] output = new TileDetails[15,15];
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                TileDetails t = new TileDetails(map[x, y], 0);
                output[x, y] = t;
            }
        }
        return output;
    }

    //Initialises a map of grass
    public void CreateMap(TileDetails[,] map)
    {
        //Destroy existing tiles if they exist
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        //save the map
        this.map = map;

        //Loop through the level data and create grid tiles
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Vector2 pos = new Vector2
                {
                    x = x * 0.5f,
                    y = (map.GetLength(0) - 1 - y) * 0.5f
                };
                pos = World2Screen(pos);

                //instantiate prefab and setup fields
                GameObject tile = Instantiate(tilePrefab);
                tile.GetComponent<Tile>().worldPos = new Vector2(x, y);
                tile.transform.SetParent(this.GetComponent<Transform>());
                tile.GetComponent<Tile>().mapManager = this;
                tile.transform.position = pos;

                //Move it down to the centre of the screen
                tile.transform.Translate(0, -map.GetLength(0) / 4.2f, 0);

                //Select the right sprite
                tile.GetComponent<SpriteRenderer>().sprite = materials[map[x,y].tile];

                //Select the right building
                if (map[x,y].building != 0)
                {
                    tile.GetComponentsInChildren<SpriteRenderer>()[1].sprite = buildings[map[x, y].building];
                }
            }
        }

        //Set the default material
        currentMaterial = materials[0];
    }

    //Converts world coordinated to screen ones
    static Vector2 World2Screen(Vector2 world)
    {
        return new Vector2
        {
            x = world.x - world.y,
            y = (world.x + world.y) / 2
        };
    }
}