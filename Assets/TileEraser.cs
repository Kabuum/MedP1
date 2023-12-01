using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class TileEraser : MonoBehaviour
{
    [Tooltip("Tilemap containig only doors must be added from the scene")]
    public Tilemap doorTileMap;

    public void OpenDoor()
    {
        StartCoroutine(OpenDoor(3));
    }
    public IEnumerator OpenDoor(float seconds)
    {
        Vector3Int tilePos = doorTileMap.WorldToCell(transform.position);
        if (doorTileMap.HasTile(tilePos) != false)
        {
            Vector3Int oldTilePos;
            oldTilePos = tilePos;
            TileBase oldTile = doorTileMap.GetTile(tilePos);
            doorTileMap.SetTile(tilePos, null);
            yield return new WaitForSeconds(seconds);
            doorTileMap.SetTile(oldTilePos, oldTile); 
        }
        yield break;
    }

}
