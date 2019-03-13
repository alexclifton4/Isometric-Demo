using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameController gameController;
    public MapManager mapManager;
    public Text materialText;
    public InputField widthInput;
    public InputField heightInput;

    public void GrassClick()
    {
        mapManager.currentMaterial = mapManager.materials[0];
        mapManager.currentMaterialId = 0;
        mapManager.buildingSelected = false;
        materialText.text = "Material: Grass";
    }

    public void WallClick()
    {
        mapManager.currentMaterial = mapManager.materials[1];
        mapManager.currentMaterialId = 1;
        mapManager.buildingSelected = false;
        materialText.text = "Material: Wall";
    }

    public void HutClick()
    {
        mapManager.currentMaterial = mapManager.buildings[1];
        mapManager.currentMaterialId = 1;
        mapManager.buildingSelected = true;
        materialText.text = "Building: Hut";
    }

    public void TowerClick()
    {
        mapManager.currentMaterial = mapManager.buildings[2];
        mapManager.currentMaterialId = 2;
        mapManager.buildingSelected = true;
        materialText.text = "Building: Tower";
    }

    public void RemoveClick()
    {
        mapManager.currentMaterial = null;
        mapManager.currentMaterialId = 0;
        mapManager.buildingSelected = true;
        materialText.text = "Remove Buildings";
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Save()
    {
        gameController.Save();
    }

    public void Load()
    {
        gameController.Load();
    }

    public void New()
    {
        //get width and height
        int width = int.Parse(widthInput.text);
        int height = int.Parse(heightInput.text);

        //construct map and load
        TileDetails[,] map = new TileDetails[width, height];
        mapManager.CreateMap(map);
    }
}
