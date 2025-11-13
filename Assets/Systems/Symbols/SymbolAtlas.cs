using UnityEngine;
using UnityEngine.U2D;

public class SymbolAtlas : MonoBehaviour
{
    public static SymbolAtlas Instance;
    public Sprite[] symbols;

    private void Awake()
    {
        Instance = this;
    }
}
