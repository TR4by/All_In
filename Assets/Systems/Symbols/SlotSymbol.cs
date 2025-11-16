using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "SlotSymbol", menuName = "SlotSymbol", order = 0)]
public class SlotSymbol : ScriptableObject
{
    public SymbolType symbolType;
    public Image image;
    public int atlasID;
}
