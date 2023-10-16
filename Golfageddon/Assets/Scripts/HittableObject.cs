using UnityEngine;

public class HittableObject : MonoBehaviour
{
    [SerializeField]
    private int pointValue;
    public int PointValue
    {
        get { return pointValue; }
    }
}
