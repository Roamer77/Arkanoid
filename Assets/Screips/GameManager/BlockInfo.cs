
public class BlockInfo 
{
  public Block Prefab {get; set;}
  public float Chanse {get; set;}    
  public BlockInfo(Block prefab, float chanse)
  {
    Prefab = prefab;
    Chanse = chanse;
  }     
}
