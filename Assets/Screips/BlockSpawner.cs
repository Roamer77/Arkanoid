using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;


public class BlockSpawner : MonoBehaviour
{
    public static Action AllBlocksDestroyed;
    public static Action<int> BlockDestroyed;

    [SerializeField] private Block _block;

    private int _destroidBlocks;
    [SerializeField] private BlockWithHealth _blockWithHealth;

    private Renderer _blockRenderer;

    private Camera _camera;
    private float _cameraWidth;
    private float _cameraHeight;

    private float _blockWidth;
    private float _blockHeight;

    private float _startBlockRenderPositionX;
    private float _startBlockRenderPositionY;

    private List<Block> _spawnedBlocks = new List<Block>();

    private void Awake()
    {
        _blockRenderer = _block.GetComponent<Renderer>();
        ScaleBlockSize(3);
        var blockSize = _blockRenderer.bounds.size;
        _blockWidth = blockSize.x;
        _blockHeight = blockSize.y;

        _camera = Camera.main;
        _cameraHeight = 2f * _camera.orthographicSize;
        _cameraWidth = _cameraHeight * _camera.aspect;
        
        var halfBlockWidth = _blockWidth / 2;
        var halfBlockHeight = _blockHeight / 2;

        _startBlockRenderPositionX = (_cameraWidth / 2) - halfBlockWidth;
        _startBlockRenderPositionY = (_cameraHeight / 2) - halfBlockHeight;

        GameManager.SpawnBlocks += SpawnLinesOfBlocks;
        GameManager.DestroyAllBlocksInLevel += DestroyAllBlocks;
    }

    void OnDisable() {
        GameManager.SpawnBlocks -= SpawnLinesOfBlocks;
        GameManager.DestroyAllBlocksInLevel -= DestroyAllBlocks;
    }

    public void SpawnLinesOfBlocks(int amountOfRows, List<BlockInfo> block)
    {
        for (var i = 0; i < amountOfRows; i++)
        {
            var tmp = _blockHeight;
            tmp *= i;
            CreatListOfBlocks(_startBlockRenderPositionX, _startBlockRenderPositionY - tmp, block);     
        }
    }

    private void  CreatListOfBlocks(float xStartPosition, float yStartPosition, List<BlockInfo> block)
    {
        var totalWeight = 0f;

        foreach (var item in block)
        {
            totalWeight += item.Chanse;
        }

        for (var i = -xStartPosition; i <= xStartPosition + _blockWidth; i += _blockWidth)
        {
            var prefab = PickOne(block, totalWeight);
            var tmpBlock = GameObject.Instantiate(prefab, new Vector3(i, yStartPosition), Quaternion.identity);
            tmpBlock.Destroyed += OnBlockDestroyed;
            _spawnedBlocks.Add(tmpBlock);
        }
    }

    public Block PickOne (List<BlockInfo> prob, float totalWeight)
    {
        var random = UnityEngine.Random.Range(0,totalWeight);
        for (var i = 0; i < prob.Count; i++)
        {
            if(random <= prob[i].Chanse)
            {
                return prob[i].Prefab;
            }
            else
            {
                random -= prob[i].Chanse;
            }
        }
        return null;
    }   
 
    private void OnBlockDestroyed(Block block)
    {          
        _spawnedBlocks.Remove(block);
        _destroidBlocks++;
        BlockDestroyed?.Invoke(_destroidBlocks);
        if (_spawnedBlocks.Count < 1)
        {
            AllBlocksDestroyed?.Invoke();
        }
    }

    private void DestroyAllBlocks()
    {
        foreach (var block in _spawnedBlocks)
        {
            if(block != null)
            {
                block.Destroyed -= OnBlockDestroyed;
                Destroy(block.gameObject);
            }
        }
        _spawnedBlocks.Clear();
        _destroidBlocks = 0;
    }

    private void ScaleBlockSize(int size)
    {
        _block.transform.localScale = new Vector3(size, size, 0);
        _blockWithHealth.transform.localScale = new Vector3(size, size, 0);
    }
}