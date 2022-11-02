using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformManager : MonoBehaviour
{
    private Tilemap _tm;
    private Grid _gr;
    // Start is called before the first frame update
    void Start()
    {
        _tm = GetComponent<Tilemap>();
        _gr = GetComponentInParent<Grid>();
    }
    // Update is called once per frame
    void Update()
    {
        /*var v = transform.position;
        var pos = _gr.WorldToCell(Vector3Int.RoundToInt(v));
        var vec = new Vector3Int(0, 0, 0);
        pos = pos - vec;*/
        //Debug.Log(pos);
        //Debug.Log(_tm.GetTile(pos));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
