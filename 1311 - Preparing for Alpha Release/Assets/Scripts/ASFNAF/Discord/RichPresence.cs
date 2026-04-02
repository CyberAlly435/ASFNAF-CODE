using UnityEngine;
using Discord.Sdk;

namespace ASFNAF.Discord;

public static class RichPresence
{
    // Variáveis
    public static Client _client;
    private static Activity _activity;

    [Header("Rich Presence")]
    public static bool richPresenceAvaliable;

    // Funções
    public static void SetupClient()
    {
        _client ??= new();
        _activity ??= new();

        _client.SetApplicationId(AppGlobals.discordAppID);

        _client.Connect();

        _client.SetStatusChangedCallback((status, error, stopcode) =>
        {
            
        });
    }

    public static void SetupActivity()
    {
        // configurar tudo;
        var _timestamp = new ActivityTimestamps();
        var _assets = new ActivityAssets();

        _activity.SetType(ActivityTypes.Playing);
        _activity.SetDetails("On Void");
        _activity.SetState($"ASFNAF BUILD: {AppGlobals.MangleBuild}, VERSION: {AppGlobals.MangleVersion}");

        _timestamp.SetStart((ulong)System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        _assets.SetLargeImage(AppGlobals.MangleBuild.ToLower());

        _activity.SetTimestamps(_timestamp);
        _activity.SetAssets(_assets);

        // atualizar o Rich Presence;
        _client.SetStatusChangedCallback((status, error, stopcode) =>
            _client.UpdateRichPresence(_activity, OnRPUpdate));
            
        _client.UpdateRichPresence(_activity, OnRPUpdate);
        
        _timestamp = null;
        _assets = null;

        richPresenceAvaliable = true;
    }

    public static Client.Status? GetStatus()
    {
        var getStatus = _client?.GetStatus();
        return getStatus;
    }

    public static bool GetAvaliable()
    {
        var getBool = richPresenceAvaliable;
        return getBool;
    }

    public static void DestroyPresence()
    {
        richPresenceAvaliable = false;

        _client?.Dispose();

        _client = null;
        _activity = null;
    }

    public static void ForceKill() => 
        DestroyPresence();

    // funçõesque vão modificar os valores do rich presence

    public static void SetType(ActivityTypes types) =>
        _activity?.SetType(types);

    public static void SetDetails(string details) =>
        _activity?.SetDetails(details);

    public static void SetState(string state) =>
        _activity?.SetState(state);

    private static void OnRPUpdate(ClientResult result)
    {
        if (result.Successful())
            Debug.Log("good");
        else
            Debug.Log("failed");
    }
}