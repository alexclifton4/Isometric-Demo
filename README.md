# Isometric Demo
Simple demonstration of an isometric (or "2.5D") game world, and saving mechanism.

This does not use Unity's isometric grid or file serialisation functions, as this is about implementing those myself, not using Unity.

## Save format
The map file is compressed using the Deflate algorithm, and is saved in the persistentDataPath

When uncompressed, the file format is as follows:  
[00-03] magic number - c0de1dea  
[04] file format version - currently 01  
[05] map width - uint, max value 255  
[06] map height - unit, max value 255  
[07-end] map data, see below - width * height * 2 bytes long  

The map data is a list of tiles, starting at the West  
Each tile is 2 bytes, as follows:  
-First byte for tile material:  
00 - grass  
01 - wall  
-Second byte for building:  
00 - none  
01 - hut  
02 - tower  
