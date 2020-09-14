using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0}, 
    };

    Vector3 startPos = new Vector3 (0, 0, 0);

    Vector3 objectSize;

    [SerializeField] private GameObject innerWall = null;
    [SerializeField] private GameObject innerWallCorner = null;
    [SerializeField] private GameObject outerWall = null;
    [SerializeField] private GameObject outerWallCorner = null;
    [SerializeField] private GameObject tJWall = null;

    // Start is called before the first frame update
    void Start() {
        objectSize = outerWall.GetComponent<Renderer>().bounds.size;

        // Loop over every element in the array        
        for(int i = 0; i < levelMap.GetLength(0); i++) {
            for (int j = 0; j < levelMap.GetLength(1); j++) {
                int caseSwitch = levelMap[i,j];
                switch (caseSwitch) {
                    case 0:
                        // Nothing
                        startPos.x += objectSize.x;
                        break;

                    case 1:
                        // Outer Wall Corner
                        Instantiate(outerWallCorner, startPos, Quaternion.identity);
                        startPos.x += objectSize.x;
                        break;
                    
                    case 2:
                        // Outer Wall
                        Instantiate(outerWall, startPos, Quaternion.identity);
                        startPos.x += objectSize.x;
                        break;
                    
                    case 3:
                        // Inner Wall Corner
                        Instantiate(innerWallCorner, startPos, Quaternion.identity);
                        startPos.x += objectSize.x;
                        break;

                    case 4:
                        // Inner Wall
                        Instantiate(innerWall, startPos, Quaternion.identity);
                        startPos.x += objectSize.x;
                        break;
                    
                    case 5:
                        // Pellet
                        startPos.x += objectSize.x;
                        break;

                    case 6:
                        // Power Pellet
                        startPos.x += objectSize.x;
                        break;

                    case 7:
                        // T Junction
                        Instantiate(tJWall, startPos, Quaternion.identity);
                        startPos.x += objectSize.x;
                        break;


                    default:
                    Debug.Log("Error at level building. This should never be run.");
                        break;
                }
            }
            // Once the Line is done
            startPos.y -= objectSize.y;
            startPos.x = 0;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
