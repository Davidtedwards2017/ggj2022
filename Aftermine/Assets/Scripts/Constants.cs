using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{

}

public enum Side
{
    Upper,
    Lower
}


public static class SideExtensions
{
    public static Side Flip(this Side side)
    {
        switch (side) 
        {
            case Side.Lower: 
                return Side.Upper;
            case Side.Upper: 
                return Side.Lower;
        }

        return default;

    }
}