using UnityEngine;

using System.IO;

using ASFNAF;

public static class MangleFiles
{
    public static bool GetFileState()
    {
        if (File.Exists(AppGlobals.MangleFileDirectory))
            return true;

        return false;
    }

    public static void FlushData()
    {
        if (!Directory.Exists(AppGlobals.MangleMainDirectory))
            return;

        File.Delete(AppGlobals.MangleMainDirectory);

        Application.Quit();
    }

    public static void SaveMangle(MangleData JSONData)
    {
        if (!Directory.Exists(AppGlobals.MangleMainDirectory))
            Directory.CreateDirectory(AppGlobals.MangleMainDirectory);

        string json = JsonUtility.ToJson(JSONData, true);
        File.WriteAllText(AppGlobals.MangleFileDirectory, json);
    }

    public static void LoadMangle(MangleData JSONData)
    {
        if (!File.Exists(AppGlobals.MangleFileDirectory) || !Directory.Exists(AppGlobals.MangleMainDirectory))
            return;

        string json = File.ReadAllText(AppGlobals.MangleFileDirectory);
        JsonUtility.FromJsonOverwrite(json, JSONData);
    }

    public static void ShowError(int ErrorNumber)
    {
        string foundcontent = "Another Seven's FNaF Fangame has encountered a critical exception and must terminate.";
        string errorText;

        switch (ErrorNumber)
        {
            case 2:
                errorText = "Error 2, Codename \"Empty Man\": ASFNAF did not accept an empty string in player Language.";

                break;
            case 3:
                errorText = "Error 3, Codename \"Empty Man\": ASFNAF did not accept spaces inside the string in player Language";

                break;
            case 4:
                errorText = "Error 4, Codename \"Empty Man\": ASFNAF did not accept resolutions lower than 640x480.";

                break;
            case 40:
                errorText = "Error 40, Codename \"Dumb Rose\": cannot assign night values to a playable scene.";

                break;
            case 41:
                errorText = "Error 41, Codename \"Dumb Rose\": cannot read player Language.";

                break;
            case 42:
                errorText = "Error 42, Codename \"Dumb Rose\": cannot set FPS with the TargetFPS below -1.";

                break;

            default:
                errorText = "Unknown Error. Call the developer for more informations.";

                break;
        }

        // nao se esquecer antes de coloca os mod: asfnaf dmt debug é *Another Seven's FNaF Fangame: Developer Manager Tools Debug*

        if (Application.platform == RuntimePlatform.LinuxPlayer)
        {
            
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            System.Diagnostics.Process.Start("CMD.exe", @$"/C @echo off && title ASFNAF DMT Debug v1&echo ~ {foundcontent}&echo ~ {errorText}&echo ~ && pause");
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            
        }

        Application.Quit();
    }
}