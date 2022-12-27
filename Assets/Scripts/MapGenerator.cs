using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap, DrawMesh};
    public DrawMode drawMode;

    public int MapWidth;
    public int MapHeight;
    public float NoiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] Regions;
    public void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, seed, NoiseScale,octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[MapWidth * MapHeight];
        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < Regions.Length; i++)
                {
                    if(currentHeight <= Regions[i].height)
                    {
                        colourMap[y * MapWidth + x] = Regions[i].colour;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTextureeMap(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if(drawMode == DrawMode.ColourMap)
        {
            display.DrawTextureeMap(TextureGenerator.TextureFromColourMap(colourMap, MapWidth, MapHeight));
        }else if(drawMode == DrawMode.DrawMesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier,meshHeightCurve), TextureGenerator.TextureFromColourMap(colourMap, MapWidth, MapHeight));
        }
      
    }
    private void OnValidate()
    {
        if (MapWidth < 1)
        {   MapWidth = 1;
        } 
        if (MapHeight < 1)
        {   MapHeight = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if( octaves < 0)
        {
            octaves = 0;
        }
    }
    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color colour;        
    }
    
}
