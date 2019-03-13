using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour
{
    public MapManager mapManager;
    
    // Start is called before the first frame update
    void Start()
    {
        //init a 2d array for map values, then create the map
        TileDetails[,] map = new TileDetails[15, 15];
        mapManager.CreateMap(map);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Saves the current map to file
    public void Save()
    {
        SaveGame.Save(mapManager.map, Path.Combine(Application.persistentDataPath, "save.map"));
        print("Game saved");
    }

    //Loads a map from a file
    public void Load()
    {
        TileDetails[,] map = SaveGame.Load(Path.Combine(Application.persistentDataPath, "save.map"));
        mapManager.CreateMap(map);
        print("Game loaded");
    }
}
