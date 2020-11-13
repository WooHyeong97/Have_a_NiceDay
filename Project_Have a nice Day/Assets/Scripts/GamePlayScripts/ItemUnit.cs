using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnit : MonoBehaviour
{
    int map = 4;
    int index = 1;

    public void ButtonClick()
    {
        PlayManager.Instant.startGameScript.StartPlayLevel(map, index);
        PlayManager.Instant.gamePlaying.Reset();
        PlayManager.Instant.lineControl.Reset();
    }
}
