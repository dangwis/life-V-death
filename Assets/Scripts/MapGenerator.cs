using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class MapGenerator : MonoBehaviour {
    public GameObject TileWall;
    public GameObject TileFloor;
    public GameObject TileTorch;

	// Use this for initialization
	void Awake () {
        string line;
        StreamReader lineReader = new StreamReader("Assets/Materials/MapDef.txt", Encoding.Default);
        Vector3 tilePos = Vector3.zero;

        using (lineReader) {
            do {
                line = lineReader.ReadLine();
                Debug.Log(line);
                if (line != null) {
                    for (int i = 0; i < line.Length; ++i) {
                        tilePos.x = i;
                        switch (line[i]) {
                            case ('G'):
                                Instantiate(TileFloor, tilePos, Quaternion.identity);
                                break;
                            case ('W'):
                                Instantiate(TileWall, tilePos, Quaternion.identity);
                                break;
                            case ('U'):
                                Instantiate(TileFloor, tilePos, Quaternion.identity);
                                Instantiate(TileTorch, tilePos, Quaternion.Euler(0, 0, 0));
                                break;
                            case ('D'):
                                Instantiate(TileFloor, tilePos, Quaternion.identity);
                                Instantiate(TileTorch, tilePos, Quaternion.Euler(0, 180, 0));
                                break;
                            case ('L'):
                                Instantiate(TileFloor, tilePos, Quaternion.identity);
                                Instantiate(TileTorch, tilePos, Quaternion.Euler(0, 270, 0));
                                break;
                            case ('R'):
                                Instantiate(TileFloor, tilePos, Quaternion.identity);
                                Instantiate(TileTorch, tilePos, Quaternion.Euler(0, 90, 0));
                                break;
                        }
                    }
                }
                tilePos.z--;
            }
            while (line != null);

            lineReader.Close();
        }
    }
}
