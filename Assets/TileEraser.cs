using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class TileEraser : MonoBehaviour
{
    public Tilemap door;
    public TileBase tile;

    Vector3Int oldTilePos;
    public Vector3Int offset;

    public void OpenDoor()
    {
        StartCoroutine(OpenDoor(3));
    }
    public IEnumerator OpenDoor(float seconds)
    {
        var tilePos = door.WorldToCell(transform.position);
        oldTilePos = tilePos;
        door.SetTile(tilePos, null);
        yield return new WaitForSeconds(seconds);
        door.SetTile(oldTilePos, tile);
    }

}
