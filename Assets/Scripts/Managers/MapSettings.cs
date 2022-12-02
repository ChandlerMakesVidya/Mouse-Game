using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSettings : MonoBehaviour
{
    public string mapName;
    public Transform[] cheesePositions;

    private void Start()
    {
        List<Transform> _cheesePos = new List<Transform>();
        int num;
        for (int i = 0; i < GameManager.GM.cheeseCountByDifficulty[GameManager.GM.difficulty]; i++)
        {
            do
            {
                num = Random.Range(0, cheesePositions.Length);
            } while (_cheesePos.Contains(cheesePositions[num]));
            _cheesePos.Add(cheesePositions[num]);
        }

        //instantiate cheese at all _cheesePos
    }
}
