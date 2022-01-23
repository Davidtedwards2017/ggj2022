using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTypeFilteredRandom : FilteredRandom<BrickType>
{
    public BrickTypeFilteredRandom(IEnumerable<BrickType> collection, int historyLength) 
        : base (collection, historyLength)
    {
        this.PreventPattern = false;
    }
}
