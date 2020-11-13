using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeCreate : MonoBehaviour
{
    int numberRow;
    int numberCol;

    float startX, startZ;

    int stt = 0;

    public TableUnit[,] listScript;

    public GameObject unit;

    int[] gameMap = new int[16] { 0, 1, 4, 0, 0, 0, -1, 0, 0, 2, 0, 0, 0, 0, 3, -1 };
    int[] newMap;

    public void CreateTableUnit(int newRow, int newCol)
    {
        numberRow = newRow;
        numberCol = newCol;

        listScript = new TableUnit[numberRow, numberCol];

        startX = -numberRow / 2.0f + 0.5f;
        startZ = -numberCol / 2.0f + 0.5f;

        for (int i = 0; i < numberRow; i++)
        {
            for (int j = 0; j < numberCol; j++)
            {
                Vector3 pos = new Vector3(startX, startZ, -0.1f);
                startZ += 2;

                InstantNewUnit(pos, i, j);

            }
            startZ = -numberCol / 2.0f + 0.5f;
            startX += 2;
        }

        PlayManager.Instant.gamePlaying.listScript = listScript;
        ApplyData(numberRow);
    }

    public void ApplyData(int map)
    {
        newMap = gameMap;
        int count = 0;
        int brick = 0;
        for (int i = 0; i < numberRow; i++)
        {
            for (int j = 0; j < numberCol; j++)
            {
                int index = newMap[count];
                switch (index)
                {
                    case 0:
                        listScript[i, j].SetNumver(0);
                        listScript[i, j].ChangeNormalBox();
                        break;
                    case -1:
                        listScript[i, j].SetNumver(0);
                        listScript[i, j].ChangeBlockBox();
                        brick++;
                        break;
                    default:
                        listScript[i, j].SetNumver(newMap[count]);
                        listScript[i, j].ChangeNormalBox();
                        break;
                }
                count++;
            }
        }
        int totalBox = numberRow * numberCol - brick;
        PlayManager.Instant.gamePlaying.SetValue(totalBox, map);
    }

    GameObject GetBox(Vector3 pos)
    {
        GameObject newObj;
        newObj = Instantiate(unit, pos, Quaternion.identity) as GameObject;

        return newObj;
    }

    public void InstantNewUnit(Vector3 pos, int row, int col)
    {
        GameObject obj = GetBox(pos);

        listScript[row, col] = obj.GetComponent<TableUnit>();
        listScript[row, col].index = stt;
        stt++;
        listScript[row, col].SetValue(row, col);
    }
}
