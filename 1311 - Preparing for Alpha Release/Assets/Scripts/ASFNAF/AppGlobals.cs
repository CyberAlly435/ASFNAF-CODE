using System;
using System.IO;

namespace ASFNAF;

public static class AppGlobals
{
    public static string MangleBuild = "Discord Update";
    public static string MangleState = "Alpha";
    public static string MangleVersion = "2.7.1";

    /*
    LINUX: /home/%USER%/.config/Angelo/MangleFiles
    WINDOWS: C:/Users/%USER%/.appdata/Roaming/Angelo/MangleFiles
    MACOS:
    */

    public static string ManglePersistentPath = $"{Environment.GetEnvironmentVariable("HOME")}/.config/Angelo";
    public static string MangleMainDirectory => Path.Combine(ManglePersistentPath, "MangleFiles");
    public static string MangleFileDirectory => Path.Combine(MangleMainDirectory, "save.mangle_data.json");

    public static ulong discordAppID = 1390028867183448144;

    // variáveis do dia e mês do natal:
    public static int GetXmasDay()
    {
        return 25;
    }

    public static int GetXmasMonth()
    {
        return 12;
    }
}