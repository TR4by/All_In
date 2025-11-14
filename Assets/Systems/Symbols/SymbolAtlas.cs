using UnityEngine;
using UnityEngine.U2D;

public class SymbolAtlas : MonoBehaviour
{
    public static SymbolAtlas Instance;
    public Sprite[] symbols;

    public Sprite GetSymbol(SymbolType symbolType) => symbols[(int)symbolType];

    private void Awake()
    {
        Instance = this;
    }
}
