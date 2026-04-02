using UnityEngine;

[CreateAssetMenu(fileName = "MangleData", menuName = "MangleFiles/MangleData")]
public class MangleData : ScriptableObject
{
    // Novas mudanças dentro do script, dia 1/23/2026
    public ASFNAF.Mangle.SettingsDataStruct settings;

    public ASFNAF.Mangle.MangleDataStruct mangle;
}
