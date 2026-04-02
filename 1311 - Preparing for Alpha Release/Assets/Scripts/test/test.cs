using UnityEngine;
using Discord.Sdk;

using ASFNAF.Discord;

public class DiscordTest : MonoBehaviour
{
    // Funções
    private void Start()
    {
        RichPresence.SetupClient();
        RichPresence.SetupActivity();

        RichPresence.SetDetails("Área de Desenvolvimento");
    }
}