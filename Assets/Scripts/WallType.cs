using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallType : MonoBehaviour
{
    public WallSide side;
}

public enum WallSide {Top, Left, Right, Bottom }
