using UnityEngine;

public class LineClearController : MonoBehaviour
{
    public GlobalPropertiesSO GlobalProperties;

    public void ClearLine(BrickMatchArgs args)
    {
        var lineClearSequence = Instantiate(GlobalProperties.prefabs.LineClearSequence, transform);
        lineClearSequence.Init(args.Bricks);        
    }
}
