using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "LocalizedAsset - CHOOSE.TABLE.ENTRY.asset", menuName = "Localization/Localized Asset")]
public class LocalizedAsset : ScriptableObject
{
    [Space]
    public LocalizedString stringReference = new LocalizedString();
}
