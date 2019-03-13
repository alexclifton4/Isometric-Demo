using System;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour 
{
    //Saves a map to file, and returns True if successful
    public static void Save(TileDetails[,] map, string file)
    {
        List<byte> bytes = new List<byte>();

        //add magic number and file version
        bytes.AddRange(Uint2Byte(0xc0de1dea));
        bytes.Add(0x01);

        //add size of grid
        bytes.Add((byte) map.GetLength(0));
        bytes.Add((byte) map.GetLength(1));

        //Add each tile
        foreach (TileDetails tile in map)
        {
            //Add tile value followed by building value
            bytes.Add((byte)tile.tile);
            bytes.Add((byte)tile.building);
        }

        //Compress the stream
        byte[] byteArray = Compress(bytes.ToArray());
        //byteArray = bytes.ToArray(); //Uncomment to skip compression

        File.WriteAllBytes(file, byteArray);
    }

    //Loads a map from file, and return it
    public static TileDetails[,] Load(string file)
    {
        //Read from the file and decompress
        byte[] bytes = File.ReadAllBytes(file);
        bytes = Decompress(bytes);

        //TODO: check magic number and file version
        //and file length??

        //Get width and height
        uint width = bytes[5];
        uint height = bytes[6];

        //map data starts at byte 7
        int position = 7;

        //construct the map
        TileDetails[,] map = new TileDetails[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileDetails t = new TileDetails(bytes[position], bytes[position + 1]);
                map[x, y] = t;
                position += 2;
            }
        }

        return map;
    }

    //Simple function to convert uint to byte array and fix endianess
    static byte[] Uint2Byte(uint val)
    {
        byte[] b = BitConverter.GetBytes(val);
        Array.Reverse(b);
        return b;
    }

    //Compresses a byte array using DeflateStream
    //taken from https://stackoverflow.com/a/39191998
    static byte[] Compress(byte[] input)
    {
        MemoryStream output = new MemoryStream();
        using (DeflateStream dstream = new DeflateStream(output, System.IO.Compression.CompressionLevel.Optimal))
        {
            dstream.Write(input, 0, input.Length);
        }
        return output.ToArray();
    }

    //Decompress a byte array using DeflateStream
    //taken from https://stackoverflow.com/a/39191998
    public static byte[] Decompress(byte[] data)
    {
        MemoryStream input = new MemoryStream(data);
        MemoryStream output = new MemoryStream();
        using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
        {
            dstream.CopyTo(output);
        }
        return output.ToArray();
    }
}
