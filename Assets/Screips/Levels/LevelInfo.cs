using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Level/New Level")]
public class LevelInfo : ScriptableObject
{
    [SerializeField]
    private string _levelName;

    [SerializeField]
    private int _amountOfRows;

    [SerializeField]
    private float _chanseOfBlockWithHeathInRow;

    [SerializeField]
    private float _chanseOfSimpleBlockInRow;

    [SerializeField]
    private float _chanseOfBonusWidthBlockInRow;

    [SerializeField]
    private float _chanseOfSlowDownBlockInRow;

    [SerializeField]
    private float _chanseOfSplitBlockInRow;

    [SerializeField] 
    private BlockWithHealth _blockWithHealth;

    [SerializeField] 
    private Block _block;

    [SerializeField]
    private IncreasePlatformWidth _bonusWidthBlock;

    [SerializeField]
    private SlowDownBallBlock _slowDownBallBlock;

    [SerializeField]
    private SplitBallBlock _splitBallBlock;

    [SerializeField]
    private float _buffSpeedBallPefLevel;

    [SerializeField]
    private int _timeRewindAttapts;

    public string LevelName => _levelName;
    
    public int AmointOfRows => _amountOfRows;
    
    public float ChanseOfBlockWithHeathInRow => _chanseOfBlockWithHeathInRow;
    
    public float ChanseOfSimpleBlockInRow => _chanseOfSimpleBlockInRow;

    public float ChanseOfBonusWidthBlockInRow => _chanseOfBonusWidthBlockInRow;

    public float ChanseOfSlowDownBlockInRow => _chanseOfSlowDownBlockInRow;

    public float ChanseOfSplitBlockInRow => _chanseOfSplitBlockInRow;

    public BlockWithHealth BlockWithHealth => _blockWithHealth;

    public Block Block => _block;

    public IncreasePlatformWidth BonusWidthBlock => _bonusWidthBlock;

    public SlowDownBallBlock SlowDownBallBlock => _slowDownBallBlock;

    public SplitBallBlock SplitBallBlock => _splitBallBlock;

    public float BuffSpeedBallPefLevel => _buffSpeedBallPefLevel;

    public int TimeRewindAttapts => _timeRewindAttapts;

    public List<BlockInfo> Blocks()
    { var blocks = new List<BlockInfo>()
      {
        new BlockInfo(Block, ChanseOfSimpleBlockInRow),
        new BlockInfo(BlockWithHealth, ChanseOfBlockWithHeathInRow),
        new BlockInfo(BonusWidthBlock, ChanseOfBonusWidthBlockInRow),
        new BlockInfo(SlowDownBallBlock, ChanseOfSlowDownBlockInRow),
      };
      return blocks;
    } 
}
