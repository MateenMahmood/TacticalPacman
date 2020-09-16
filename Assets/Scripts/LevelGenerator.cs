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

                                startPos.y -= objectSize.y;
                                Instantiate(outerWall, startPos, Quaternion.Euler(new Vector3(0, 0, 90)));
                                startPos.x += objectSize.x;
                                startPos.y += objectSize.y;
                                break;
                            }
                        }

                        Instantiate(outerWall, startPos, Quaternion.identity);
                        startPos.x += objectSize.x;
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
                                Debug.Log("I should be shown twice");
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
                                    Debug.Log("This is running");
                                    Debug.Log(levelMap[i,j+1] == 4);
                                    Debug.Log(levelMap[i,j+2] == 4);
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
                        startPos.x += objectSize.x;
                        break;

                        

                    case 4:
                        // Inner Wall

                        if (j != 0 && j != 13) {
                            if ((levelMap[i,j-1] == 3 || levelMap[i,j-1] == 4) &&
                                (levelMap[i,j+1] == 3 || levelMap[i,j+1] == 4)) {
                                Instantiate(innerWall, startPos, Quaternion.identity);
                                startPos.x += objectSize.x;
                                break;
                            }
                        }
                        

                        startPos.y -= objectSize.y;
                        Instantiate(innerWall, startPos, Quaternion.Euler(new Vector3(0, 0, 90)));
                        startPos.x += objectSize.x;
                        startPos.y += objectSize.y;
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

    void GenTopRight(GameObject cornerWall) {
        startPos.x += objectSize.x;
        Instantiate(cornerWall, startPos, Quaternion.Euler(new Vector3(0, 0, 270)));
    }

    void GenTopLeft(GameObject cornerWall) {
        Instantiate(cornerWall, startPos, Quaternion.identity);
        startPos.x += objectSize.x;
    }

    void GenBotRight(GameObject cornerWall) {
        startPos.x += objectSize.x;
        startPos.y -= objectSize.y;
        Instantiate(cornerWall, startPos, Quaternion.Euler(new Vector3(0, 0, 180)));
        startPos.y += objectSize.y;
    }

    void GenBotLeft(GameObject cornerWall) {
        startPos.y -= objectSize.y;
        Instantiate(cornerWall, startPos, Quaternion.Euler(new Vector3(0, 0, 90)));
        startPos.x += objectSize.x;
        startPos.y += objectSize.y;
    }
}
