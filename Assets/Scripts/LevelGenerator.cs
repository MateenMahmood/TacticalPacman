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

    [SerializeField] private GameObject parentWallObject = null;
    [SerializeField] private GameObject stitchedWall = null;
    [SerializeField] private GameObject innerWall = null;
    [SerializeField] private GameObject innerWallCorner = null;
    [SerializeField] private GameObject outerWall = null;
    [SerializeField] private GameObject outerWallCorner = null;
    [SerializeField] private GameObject tJWall = null;
    [SerializeField] private GameObject pellet = null;
    [SerializeField] private GameObject powerPellet = null;

    // Start is called before the first frame update
    void Start() {
        objectSize = outerWall.GetComponent<Renderer>().bounds.size;
        GenOneQuad();
        Vector3 quadLoc = new Vector3(2f * 14 * objectSize.x, 0f, 0f);
        Instantiate(parentWallObject, quadLoc, Quaternion.Euler(new Vector3(0, 180, 0)));
        quadLoc = new Vector3(0, (-2f * 14f * objectSize.y) - objectSize.y, 0);
        Instantiate(parentWallObject, quadLoc, Quaternion.Euler(new Vector3(180, 0, 0)));
        quadLoc.x += 2f * 14 * objectSize.x;
        Instantiate(parentWallObject, quadLoc, Quaternion.Euler(180, 180, 0));

        startPos = new Vector3(0, 0, 0);

        GenLastLine();
        quadLoc = new Vector3(2f * 14 * objectSize.x, 0, 0);
        Instantiate(stitchedWall, quadLoc, Quaternion.Euler(new Vector3(0, 180, 0)));
    }

    void GenOneQuad() {
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

                        // Corner Edge Cases
                        // == Top ==
                        if (i == 0) {
                            // == Left ==
                            if (levelMap[i,j+1] == 2) {
                                GenTopLeft(outerWallCorner);
                                break;
                            }

                            // == Right ==
                            if (levelMap[i,j-1] == 2) {
                                GenTopRight(outerWallCorner);
                                break;
                            }
                        }

                        // == Bottom ==
                        if (i == 14) {
                            // == Left ==
                            if (levelMap[i,j+1] == 2) {
                                GenBotLeft(outerWallCorner);
                                break;
                            }

                            // == Right ==
                            if (levelMap[i,j-1] == 2) {
                                GenBotRight(outerWallCorner);
                                break;
                            }
                        }

                        // Regular Cases
                        // == Top ==
                        if (levelMap[i-1,j] != 2) {
                            // == Left ==
                            if (levelMap[i,j+1] == 2) {
                                GenTopLeft(outerWallCorner);
                                break;
                            }

                            // == Right ==
                            if (levelMap[i,j-1] == 2) {
                                GenTopRight(outerWallCorner);
                                break;
                            }
                        }

                        // == Bottom ==
                        if (levelMap[i+1,j] != 2) {
                            // == Left ==
                            if (levelMap[i,j+1] == 2) {
                                GenBotLeft(outerWallCorner);
                                break;
                            }

                            // == Right ==
                            if (levelMap[i,j-1] == 2) {
                                GenBotRight(outerWallCorner);
                                break;
                            }
                        }
                        startPos.x += objectSize.x;
                        break;
                    
                    case 2:
                        // Outer Wall
                        if (i != 0) {
                            if (levelMap[i-1,j] == 1 || levelMap[i-1,j] == 2) {

                                GenBotLeft(outerWall);
                                break;
                            }
                        }

                        GenTopLeft(outerWall);
                        break;
                    
                    case 3:
                        // Inner Wall Corner

                        if (i != 0 && j != 13) {
                            
                            // == Top ==
                            // Check if there is no wall above
                            if (levelMap[i-1, j] != 3 && levelMap[i-1, j] != 4) {
                                // == Left ==
                                if (levelMap[i,j-1] != 3 && levelMap[i,j-1] != 4) {
                                    GenTopLeft(innerWallCorner);
                                    break;
                                }

                                // == Right ==
                                if (levelMap[i,j+1] != 3 && levelMap[i,j+1] != 4) {
                                    GenTopRight(innerWallCorner);
                                    break;
                                }
                            }

                            // == Bottom ==
                            // Check if there is no wall below
                            if (levelMap[i+1, j] != 3 && levelMap[i+1, j] != 4) {

                                // == Left ==
                                if (levelMap[i,j-1] != 3 && levelMap[i,j-1] != 4) {
                                    GenBotLeft(innerWallCorner);
                                    break;
                                }

                                // == Right ==
                                if (levelMap[i,j+1] != 3 && levelMap[i,j+1] != 4) {
                                    GenBotRight(innerWallCorner);
                                    break;
                                }
                            }

                            // If surrounded by all sides by walls
                            if ((levelMap[i-1,j] == 3 || levelMap[i-1,j] == 4) &&
                            (levelMap[i+1,j] == 3 || levelMap[i+1,j] == 4) &&
                            (levelMap[i,j-1] == 3 || levelMap[i,j-1] == 4) &&
                            (levelMap[i,j+1] == 3 || levelMap[i,j+1] == 4)) {
                                //Check if there are two straight walls in succession

                                // == Top ==
                                if (levelMap[i+1,j] == 4 && levelMap[i+2,j] == 4) {
                                    // == Left ==
                                    if (levelMap[i,j+1] == 4 && levelMap[i,j+2] == 4) {
                                        GenTopLeft(innerWallCorner);
                                        break;
                                    }

                                    // == Right ==
                                    if (levelMap[i,j-1] == 4 && levelMap[i,j-2] == 4) {
                                        GenTopRight(innerWallCorner);
                                        break;
                                    }
                                }

                                // == Bottom ==
                                if (levelMap[i-1,j] == 4 && levelMap[i-2,j] == 4) {
                                    // == Left ==
                                    if (levelMap[i,j+1] == 4 && levelMap[i,j+2] == 4) {
                                        GenBotLeft(innerWallCorner);
                                        break;
                                    }
                                    // == Right ==
                                    if (levelMap[i,j-1] == 4 && levelMap[i,j-2] == 4) {
                                        GenBotRight(innerWallCorner);
                                        break;
                                    }
                                }
                            }
                        }

                        // At the very edge of the maze
                        if (j == 13) {
                            // Check above twice for GenBot
                            if ((levelMap[i-1,j] == 3 || levelMap[i-1,j] == 4) &&
                            (levelMap[i-2,j] == 3 || levelMap[i-2,j] == 4)) {
                                // Check left for GenRight
                                if (levelMap[i,j-1] == 3 || levelMap[i,j-1] == 4) {
                                    GenBotRight(innerWallCorner);
                                } else {
                                    GenBotLeft(innerWallCorner);
                                }
                            }
                            // Check below twice for GenTop
                            if ((levelMap[i+1,j] == 3 || levelMap[i+1,j] == 4) &&
                            (levelMap[i+2,j] == 3 || levelMap[i+2,j] == 4)) {
                                // Check left for GenRight
                                if (levelMap[i,j-1] == 3 || levelMap[i,j-1] == 4) {
                                    GenTopRight(innerWallCorner);
                                } else {
                                    GenTopLeft(innerWallCorner);
                                }
                            }
                        }

                        startPos.x += objectSize.x;
                        break;

                        

                    case 4:
                        // Inner Wall
                        if (i != 14) {

                            if (j == 13 || i == 14) {
                                // Check if there is anything to the left
                                if (levelMap[i,j-1] == 3 || levelMap[i,j-1] == 4) {
                                    GenTopLeft(innerWall);
                                    
                                    break;
                                } else {
                                    GenBotLeft(innerWall);
                                    break;
                                }
                            }

                            if (j != 0 && j != 13) {
                                if ((levelMap[i,j-1] == 3 || levelMap[i,j-1] == 4) &&
                                    (levelMap[i,j+1] == 3 || levelMap[i,j+1] == 4)) {
                                    GenTopLeft(innerWall);
                                    break;
                                } 
                                
                                if ((levelMap[i-1,j] == 3 || levelMap[i-1,j] == 4) &&
                                (levelMap[i+1,j] == 3 || levelMap[i+1,j] == 4)) {
                                    GenBotLeft(innerWall);
                                    break;
                                }

                            }
                            GenTopLeft(innerWall);
                            break;

                        } else {
                            startPos.x += objectSize.x;
                            break;
                        }
                    
                    case 5:
                        // Pellet
                        if (i != 14) {
                            Instantiate(pellet, startPos, Quaternion.identity, parentWallObject.transform);
                        }
                        startPos.x += objectSize.x;
                        break;

                    case 6:
                        // Power Pellet
                        Instantiate(powerPellet, startPos, Quaternion.identity, parentWallObject.transform);
                        startPos.x += objectSize.x;
                        break;

                    case 7:
                        // T Junction
                        Instantiate(tJWall, startPos, Quaternion.identity, parentWallObject.transform);
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

    void GenLastLine() {
        for(int i = 0; i < levelMap.GetLength(0); i++) {
            for (int j = 0; j < levelMap.GetLength(1); j++) {
                int caseSwitch = levelMap[i,j];
                
                if (caseSwitch == 4 && i == 14) {
                    startPos.y -= objectSize.y;
                    Instantiate(innerWall, startPos, Quaternion.Euler(new Vector3(0, 0, 90)), stitchedWall.transform);
                    startPos.x += objectSize.x;
                    startPos.y += objectSize.y;
                } else if (caseSwitch == 5 && i == 14) {
                    Instantiate(pellet, startPos, Quaternion.identity, parentWallObject.transform);
                    startPos.x += objectSize.x;
                } else {
                    startPos.x += objectSize.x;
                }
            }
            // Once the Line is done
            startPos.y -= objectSize.y;
            startPos.x = 0;
        }
    }

    GameObject GenTopRight(GameObject cornerWall) {
        startPos.x += objectSize.x;
        return(Instantiate(cornerWall, startPos, Quaternion.Euler(new Vector3(0, 0, 270)), parentWallObject.transform));
    }

    GameObject GenTopLeft(GameObject cornerWall) {
        GameObject temp = Instantiate(cornerWall, startPos, Quaternion.identity, parentWallObject.transform);
        startPos.x += objectSize.x;
        return(temp);
    }

    GameObject GenBotRight(GameObject cornerWall) {
        startPos.x += objectSize.x;
        startPos.y -= objectSize.y;
        GameObject temp = Instantiate(cornerWall, startPos, Quaternion.Euler(new Vector3(0, 0, 180)), parentWallObject.transform);
        startPos.y += objectSize.y;
        return(temp);
    }

    GameObject GenBotLeft(GameObject cornerWall) {
        startPos.y -= objectSize.y;
        GameObject temp = Instantiate(cornerWall, startPos, Quaternion.Euler(new Vector3(0, 0, 90)), parentWallObject.transform);
        startPos.x += objectSize.x;
        startPos.y += objectSize.y;
        return(temp);
    }
}
