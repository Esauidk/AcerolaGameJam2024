using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public delegate void FinishedPuzzle();

    public FinishedPuzzle finished;
}
