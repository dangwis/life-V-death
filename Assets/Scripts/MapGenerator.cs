using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class MapGenerator : MonoBehaviour {
    public GameObject TileWall;
    public GameObject TileFloor;
    public GameObject TileTorch;
    public GameObject TileDeadFountain;
    public GameObject TileLivingFountain;
    public GameObject TileFallingBlock;
    public GameObject TilePregame;
    public GameObject TileCrate;
    public GameObject TileVase;
    public TextAsset textFile;
    public static int fountLoc;

    List<Vector3> fountainPositions = new List<Vector3>();

	// Use this for initialization
	void Start () {
        string line;
        Vector3 tilePos = Vector3.zero;

        string[] lines = textFile.text.Split("\n"[0]);

        for (int j = 0; j < lines.Length; j++) {
            line = lines[j];
            if (line != null) {
                for (int i = 0; i < line.Length; ++i) {
                    tilePos.x = i;
                    switch (line[i]) {
                        case ('G'): // Ground
                            Instantiate(TileFloor, tilePos, Quaternion.identity);
                            break;
                        case ('W'): // Wall
                            Instantiate(TileWall, tilePos, Quaternion.identity);
                            break;
                        case ('U'): // Torch facing up
                            Instantiate(TileFloor, tilePos, Quaternion.identity);
                            Instantiate(TileTorch, tilePos, Quaternion.Euler(0, 0, 0));
                            break;
                        case ('D'): // Torch facing down
                            Instantiate(TileFloor, tilePos, Quaternion.identity);
                            Instantiate(TileTorch, tilePos, Quaternion.Euler(0, 180, 0));
                            break;
                        case ('L'): // Torch facing left
                            Instantiate(TileFloor, tilePos, Quaternion.identity);
                            Instantiate(TileTorch, tilePos, Quaternion.Euler(0, 270, 0));
                            break;
                        case ('R'): // Torch facing right
                            Instantiate(TileFloor, tilePos, Quaternion.identity);
                            Instantiate(TileTorch, tilePos, Quaternion.Euler(0, 90, 0));
                            break;
                        case ('F'): // Fountain location
                            fountainPositions.Add(tilePos);
                            break;
                        case ('N'): // Nothing
                            break;
                        case ('B'): // Falling block
                            Instantiate(TileFloor, tilePos, Quaternion.identity);
                            Instantiate(TileFallingBlock, tilePos, Quaternion.identity);
                            break;
                        case ('P'): // Pregame Wall
                            Instantiate(TileFloor, tilePos, Quaternion.identity);
                            Instantiate(TilePregame, tilePos, Quaternion.identity);
                            break;
                        case ('C'): // Crate Tile
                            Instantiate(TileCrate, tilePos, Quaternion.identity);
                            break;
                        case ('V'): // Vase Tile
                            Instantiate(TileVase, tilePos, Quaternion.identity);
                            break;
                    }
                }
            }
            tilePos.z--;
        }

        // Spawn 1 living fountain and the rest dead
        int livingFountain = Random.Range(0, fountainPositions.Count);
        fountLoc = livingFountain;

        for (int i = 0; i < fountainPositions.Count; i++) {
            if (i == livingFountain) {
                Instantiate(TileLivingFountain, fountainPositions[i], Quaternion.identity);
            } else {
                Instantiate(TileDeadFountain, fountainPositions[i], Quaternion.identity);
            }
        }
        
    }
}
