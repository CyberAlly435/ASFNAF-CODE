using System.Collections.Generic;

namespace ASFNAF.Mangle;

public static class MangleLanguage
{
    private static readonly string AngeloSocialMedia = "https://www.instagram.com/s3venn8/";
    private static readonly string AngeloWebsite = "https://asfnaf.angelo-page.online/";

    public class LanguagePack
    {
        public struct DiscordStatus
        {
            public string Warning;
            public string Title;
            public string Extras;
            public string Credits;
            public string Journals;
            public string Waiting;
            public string Playing;
        }
        public DiscordStatus discord;

        #region Warning: ->
        public string WarningText;
        #endregion

        #region Title: ->
        /// <summary>
        /// Aqui abaixo estarão os que pertencem a tela do menu inicial
        /// </summary>
        public string Newgame;
        public string Continue;
        public string SixNight;
        public string CustomNight;

        public string Extras;
        public string Credits;

        public string DeleteContent;
        public string ExitText;

        /// <summary>
        /// A partir da linha abaixo, estarão os pertencentes respectivamente aos créditos e ao menu de extras.
        /// </summary>
        public string AboutCreators;
        public string AboutLeave;
        #endregion

        #region Extras: ->

        public struct Selector
        {
            public string Animatronics;
            public string Interviews;
            public string Minigames;
            public string Extras;
            public string Cheats;
            public string Settings;
            public string Exit;
        }
        public Selector selector;

        public struct Settings
        {
            public struct VideoSettings
            {
                // Opções 1
                public string ResolutionTitle;

                public string WindowModeTitle;
                public string[] WindowModeOptions;

                public string ConstantSizeTitle;

                // Opções 2
                public string FPSTitle;
                public string FPSUnlimitedOption;
                public string VsyncTitle;

                // Opções 3
                public string PostProcessingTitle;
            }
            public VideoSettings videoSettings;

            public struct AudioSettings
            {
                public string mainTitle;
                public string musicTitle;
                public string ambeintTitle;
                public string voiceTitle;

                public string muteTitle;
            }
            public AudioSettings audioSettings;

            public struct LanguageSettings
            {
                // Dropdowns
                public string LanguageTitle;
                public string VoicerTitle;

                // Toggles
                public string SubtitlesTitle;
                public string SystemSyncTitle;
            }
            public LanguageSettings languageSettings;

            public struct ControlsSettings
            {
                public string ControlsText;
            }
            public ControlsSettings controlsSettings;

            public struct FeaturesSettings
            {
                // Discord
                public string DiscordTitle;
                public string[] DiscordStatus;

                // Outros
                public string TimeActivity;
                public string TimeText;
            }
            public FeaturesSettings featuresSettings;

            public string videoButton;
            public string audioButton;
            public string languageButton;
            public string controlsButton;
            public string featuresButton;

            public string Title;

            public string DiscordDetails;
        }
        public Settings settings;

        #endregion

        #region PreparingToNight: ->
        public string WhatNight;
        #endregion

        #region Journals: ->
        public string SkipText;
        #endregion

        #region Main: ->
        public Dictionary<int, string> CameraNames;

        public string NightText;

        public string PowerText;
        public string UsageText;
        public string WindUp;
        public string ChangeSong;
        public string PressCTRL;
        #endregion

        public struct Minigames
        {
            public string[] heCried;
        }; public Minigames minigames;
    }

    public static Dictionary<LanguageID, LanguagePack> languages => new Dictionary<LanguageID, LanguagePack>
    {
        {
            LanguageID.Portuguese,
            new LanguagePack
            {
                discord = new()
                {
                    Warning = "Prestando atenção ao aviso",
                    Title = "No título do jogo",
                    Extras = "Em Extras",
                    Credits = "Observando os créditos",
                    Journals = "Lendo os jornais",
                    Waiting = "Esperando pela noite {0}",
                    Playing = "No escritório, Noite {0}"
                },

                #region Warning: ->
                WarningText = "AVISO!\n\nEsta Fangame contém elementos baseados em fatos reais relacionados à comunidade mencionada.\nA sua visão é uma adaptação da história.\n\nSe divirta!",
                #endregion

                #region Title: ->
                Newgame = "Novo Jogo",
                Continue = "Continuar",
                SixNight = "Noite 6",
                CustomNight = "Noite Customizada",

                Extras = "Extras",
                Credits = "Créditos",

                DeleteContent = "Pressione e segure \"Delete\" por 5 segundos ou mais para apagar os dados.",
                ExitText = "Realmente gostaria de sair do jogo?\n\nPressione \"Enter\" para sair ou pressione qualquer botão para continuar.",

                AboutCreators = $"<size=180%>Another Seven's FNaF Fangame: </size>\r\n<color=\"red\">Amino PT-BR</color>\r\n\r\n  Nossos Creditos(PT-BR):\r\n \r\n\t- Angelo: Desenvolvedor, Responsavel pelas renders, historia e efeitos sonoros, interprete para Portugues, Ingles e Japones!\r\n\t\r\n\t- Akame: Portadora da voz feminina do Phone Guy em Portugues.\r\n\t- Clowner: Portador da voz masculina do Phone Guy em Portugues.\r\n\r\n\t- Angelo: Portador da voz masculina do Phone Guy em Portugues.\r\n\t- Angelo: Portador da voz masculina do Phone Guy em Ingles\r\n\t- Angelo: Portador da voz masculina do Phone Guy em Japones\r\n\t\r\n\t- Angelo: Legendas em Portugues, Ingles e Japones.\r\n\r\n<size=180%>Gostaria de doar para o Desenvolvedor? </size>\r\n\t\r\n\tAngelo ainda esta trabalhando em uma forma de receber incentivos\r\npara continuar os seus projetos e por mais que seja uma fangame baseada nos assets originais dos jogos de Scott Cawthon, ha bastante amor e carinho depositado no ASFNAF. Entao, ate o momento, a unica forma de doar para o desenvolvedor e conversando com ele.\r\n\r\n\tDiscord: angelic04\r\n\t\r\n\tClique <link=\"{AngeloSocialMedia}\"><color=#89cff0><u>Aqui</link></color></u> para acessar o meu Instagram!\r\n\tClique <link=\"{AngeloWebsite}\"><color=#89cff0><u>Aqui</link></color></u> para acessar o meu website!",
                AboutLeave = "Voltar ao Menu",
                #endregion

                #region Extras: ->
                    selector = new()
                    {
                        Animatronics = "Animatrônicos",
                        Interviews = "Entrevistas",
                        Minigames = "Minijogos",
                        Extras = "Adicionais",
                        Cheats = "Trapaças",
                        Settings = "Configurações",

                        Exit = "Voltar"
                    },

                    settings = new()
                    {
                        videoSettings = new()
                    {
                        // Janelas
                        ResolutionTitle = "Resolução",
                        WindowModeTitle = "Exibição",
                        WindowModeOptions = new string[]
                        {
                            "Tela Exclusiva",
                            "Tela Cheia",
                            "Janela Sem Bordas",
                            "Janela"
                        },
                        ConstantSizeTitle = "Área Linear",

                        // Frames por segundo
                        FPSTitle = "FPS Alvo",
                        FPSUnlimitedOption = "Ilimitado",
                        VsyncTitle = "Vsync",

                        // Qualidade
                        PostProcessingTitle = "Pós Processamento"
                    },

                        audioSettings = new()
                    {
                        mainTitle = "Principal",
                        musicTitle = "Música",
                        ambeintTitle = "Ambiente",
                        voiceTitle = "Voz do Telefone",

                        muteTitle = "Mute"
                    },

                        languageSettings = new()
                    {
                        // Idioma
                        LanguageTitle = "Idioma",
                        VoicerTitle = "Voz",

                        // Outros
                        SubtitlesTitle = "Legendas",
                        SystemSyncTitle = "SystemSync"
                    },

                        controlsSettings = new()
                    {
                        ControlsText = @"Controles:

Q - Ligar/Desligar Luz Esquerda
D - Ligar/Desligar Luz Direita
A - Fechar Porta Esquerda
S - Usar a Câmera
Espaço - Ligar/Desligar o Ventilador

C - Resolver um problema

Ctrl - Lanterna

Esc - Segurar por 5 segundos retorna ao Menu"
                    },

                        featuresSettings = new()
                    {
                        // Discord
                        DiscordTitle = "Discord Sync",
                        DiscordStatus = new string[]
                        {
                            "Conectado",
                            "Desconectado"
                        },

                        // Outros
                        TimeActivity = "Tempo de Jogo",
                        TimeText = "{0}h {1}m {2}s"
                    },

                        videoButton = "Vídeo",
                        audioButton = "Áudio",
                        languageButton = "Linguagem",
                        controlsButton = "Controles",
                        featuresButton = "Mecânicas",

                        Title = "Configurações",

                        DiscordDetails = "Configurando o jogo"
                    },

                #endregion

                #region PreparingToNight: ->
                WhatNight = "10 PM\n\nNoite {0}",
                #endregion

                #region Journals: ->
                SkipText = "Pressione <u>SpaceBar</u> para pular a cena.",
                #endregion

                #region Main: ->
                CameraNames = new Dictionary<int, string>()
                {
                    { 1, "Área de Jantar" },
                    { 2, "Canto Oeste" },
                    { 3, "Canto Leste" },
                    { 4, "Corredor Oeste" },
                    { 5, "Corredor Leste" },
                    { 6, "Sala de Festas 2" },
                    { 7, "Sala de Festas 1" },
                    { 8, "Ventilação Norte" },
                    { 9, "Ventilação Sul" },
                    { 10, "Corredor Principal" },
                    { 11, "Abrigo da Pacífica" },
                    { 12, "Peças e Outras Tralhas" },
                    { 13, "Estrela Principal" },
                    { 14, "Palco Principal" },
                    { 15, "Palco Infantil" },
                    { 16, "Área das Crianças" },
                    { 17, "Área de Jogos" },
                    { 18, "Canto dos Presentes" },
                    { 19, "Bastidores" },
                    { 20, "Banheiros" },
                    { 21, "Cozinha" }
                },

                NightText = "Noite",

                PowerText = "Energia Restante",
                UsageText = "Uso de Energia",
                PressCTRL = "Pressione Ctrl para usar a lanterna",

                WindUp = "Dar corda\nMusical",
                ChangeSong = "Mudar Canto",
                #endregion
            }
        },
        {
            LanguageID.Japanese,
            new LanguagePack
            {
                discord = new()
                {
                    Warning = "警告に注意する",
                    Title = "ゲームタイトルで",
                    Extras = "エキストラ",
                    Credits = "クレジットを確認する",
                    Journals = "新聞を読む",
                    Waiting = "夜を待つ {0}",
                    Playing = "オフィス、夜{0}"
                },

                #region Warning: ->
                WarningText = "警告！\n\nこのファンゲームには、前述のコミュニティに関連する実際の出来事に基づいた要素が含まれています。\nその解釈は、物語の実際のバージョンです。\n\n楽しむ！",
                #endregion

                #region Title: ->
                Newgame = "新しいゲーム",
                Continue = "続ける",
                SixNight = "第6の夜",
                CustomNight = "カスタムナイト",

                Extras = "エキストラ",
                Credits = "クレジット",

                DeleteContent = "「Delete」を5秒以上押し続けるとデータが削除されます。",
                ExitText = "本当にゲームを終了しますか？\n\n終了するには「ENTER」を押し、続行するには任意のキーを押してください。",

                AboutCreators = $"<size=100%>Another Seven's FNaF Fangame: </size>\r\n<color=\"red\">Amino PT-BR</color>\r\n\r\n 私たちのクレジット(日本語-日本):\r\n \r\n\t- Angelo: 開発者。レンダー、ストーリー、効果音を担当。ポルトガル語・英語・日本語の通訳。\r\n\t\r\n\t- Akame: ポルトガル語版 Phone Guy の女性ボイス担当。\r\n\t- Clowner: ポルトガル語版 Phone Guy の男性ボイス担当。\r\n\r\n\t- Angelo: ポルトガル語版 Phone Guy の男性ボイス担当。\r\n\r\n\t- Angelo: 英語版 Phone Guy の男性ボイス担当。\r\n\r\n\t- Angelo: 日本語版 Phone Guy の男性ボイス担当。\r\n\r\n\t- Angelo: ポルトガル語・英語・日本語のクローズドキャプション担当。\r\n\r\n<size=180%>開発者に支援したいですか？</size>\r\n\t\r\n\tAngelo は現在、プロジェクトを継続するための支援を受け取る方法を準備中です。この作品は Scott Cawthon のオリジナルゲームのアセットを基にしたファンゲームですが、ASFNAF には多くの情熱と愛情が込められています。そのため、現時点で開発者を支援する唯一の方法は、直接連絡を取ることです。\r\n\r\n\tDiscord: angelic04\r\n\t\r\n\tInstagram へアクセスするには <link=\"{AngeloSocialMedia}\"><color=#89cff0><u>こちら</link></color></u> をクリックしてください。\r\n\tWeb サイトへアクセスするには <link=\"{AngeloWebsite}\"><color=#89cff0><u>こちら</link></color></u> をクリックしてください。",
                AboutLeave = "タイトルに戻る",
                #endregion

                #region Extras
                    selector = new()
                    {
                        Animatronics = "アニマトロニクス",
                        Interviews = "インタビュー",
                        Minigames = "ミニゲーム",
                        Extras = "その他",
                        Cheats = "チート",
                        Settings = "設定",

                        Exit = "戻る"
                    },

                    settings = new()
                    {
                        videoSettings = new()
                    {
                        // Janelas
                        ResolutionTitle = "解像度",
                        WindowModeTitle = "表示",
                        WindowModeOptions = new string[]
                        {
                            "専用スクリーン",
                            "フルスクリーン",
                            "ボーダーレス",
                            "ウィンドウ"
                        },
                        ConstantSizeTitle = "固定サイズ",

                        // Frames por segundo
                        FPSTitle = "目標FPS",
                        FPSUnlimitedOption = "無制限",
                        VsyncTitle = "Vsync",

                        // Qualidade
                        PostProcessingTitle = "ポストプロセス"
                    },

                        audioSettings = new()
                    {
                        mainTitle = "メイン",
                        musicTitle = "音楽",
                        ambeintTitle = "環境音",
                        voiceTitle = "電話音声",

                        muteTitle = "ミュート"
                    },

                        languageSettings = new()
                    {
                        // Idioma
                        LanguageTitle = "言語",
                        VoicerTitle = "声優",

                        // Outros
                        SubtitlesTitle = "字幕",
                        SystemSyncTitle = "システム同期"
                    },

                        controlsSettings = new()
                    {
                        ControlsText = @"操作方法:

Q - 左ライトのオン/オフ
D - 右ライトのオン/オフ
A - 左ドアを閉じる
S - カメラを使用
スペースキー - タービンファンをオン/オフ

C - 問題を解決する

Ctrl - 懐中電灯

Esc - 5秒間押し続けるとメニューに戻る"
                    },

                        featuresSettings = new()
                    {
                        // Discord
                        DiscordTitle = "Discord同期",
                        DiscordStatus = new string[]
                        {
                            "繋がっている",
                            "繋がっていない"
                        },

                        // Outros
                        TimeActivity = "アクティビティ時間",
                        TimeText = "{0}時 {1}分 {2}秒"
                    },

                        videoButton = "ビデオ",
                        audioButton = "オーディオ",
                        languageButton = "言語",
                        controlsButton = "コントロール",
                        featuresButton = "機能",
                        
                        Title = "設定",

                        DiscordDetails = "ゲームを設定する"
                    },
                #endregion

                #region PreparingToNight: ->
                WhatNight = "午後10時\n\n夜 {0}日",
                #endregion

                #region Journals: ->
                SkipText = "ジャンプするには <u>SPACEBAR</u> を押します。",
                #endregion

                #region Main: ->
                CameraNames = new Dictionary<int, string>()
                {
                    { 1, "食事スペース" },
                    { 2, "西の角" },
                    { 3, "東の角" },
                    { 4, "西廊下" },
                    { 5, "東廊下" },
                    { 6, "パーティールーム１" },
                    { 7, "パーティールーム２" },
                    { 8, "北通気口" },
                    { 9, "南通気口" },
                    { 10, "メインホール" },
                    { 11, "パシフィカの避難所" },
                    { 12, "部品とサービス" },
                    { 13, "メインのスター" },
                    { 14, "メインステージ" },
                    { 15, "子供のステージ" },
                    { 16, "キッズエリア" },
                    { 17, "ゲームエリア" },
                    { 18, "ギフトコーナー" },
                    { 19, "バックステージ" },
                    { 20, "お手洗い" },
                    { 21, "キッチン" }
                },

                NightText = "夜",

                PowerText = "残りエネルギー",
                UsageText = "エネルギー使用量",
                PressCTRL = "懐中電灯を使用するには、Ctrl キーを押します",

                WindUp = "巻き上げ式\nオルゴール",
                ChangeSong = "曲を変える",
                #endregion
            }
        },
        {
            LanguageID.English,
            new LanguagePack
            {
                discord = new()
                {
                    Warning = "Paying attention to the warning",
                    Title = "In game title",
                    Extras = "In Extras",
                    Credits = "Looking credits",
                    Journals = "Reading the newspapers",
                    Waiting = "Waiting for night {0}",
                    Playing = "Office, night {0}"
                },

                #region Warning: ->
                WarningText = "WARNING!\n\nThis fangame contains elements based on real events related to the mentioned community.\nIts interpretation is an adaptation of the story.\n\nHave Fun!",
                #endregion

                #region Title: ->
                Newgame = "New game",
                Continue = "Continue",
                SixNight = "6th Night",
                CustomNight = "Custom Night",

                Extras = "Extras",
                Credits = "Credits",

                DeleteContent = "Press n hold \"Delete\" for 5 seconds to flush your data.",
                ExitText = "Do you really want to exit the game?\n\nPress \"Enter\" to quit or press any key to continue.",

                AboutCreators = $"<size=100%>Another Seven's FNaF Fangame: </size>\r\n<color=\"red\">Amino PT-BR</color>\r\n\r\n Our Credits(EN-US):\r\n \r\n\t- Angelo: Developer, responsible for renders, story and sound effects; CC in Portuguese, English and Japanese!\r\n\t\r\n\t- Akame: Female voice provider for the Phone Guy in Portuguese.\r\n\t- Clowner: Male voice provider for the Phone Guy in Portuguese.\r\n\r\n\t- Angelo: Male voice provider for the Phone Guy in Portuguese.\r\n\r\n\t- Angelo: Male voice provider for the Phone Guy in English.\r\n\r\n\t- Angelo: Male voice provider for the Phone Guy in Japanese.\r\n\r\n\t- Angelo: CCs in Portuguese, English and Japanese.\r\n\r\n<size=180%>Would you like to support the Developer? </size>\r\n\t\r\n\tAngelo is still working on a way to recieve support to continue his projects, and even though this is a fangame based on the original assets from Scott Cawthon's games, a lot of care and dedication has been invested in ASFNAF. So, for now, the only way to donate to the developer is by contacting him directly.\r\n\r\n\tDiscord: angelic04\r\n\t\r\n\tClick <link=\"{AngeloSocialMedia}\"><color=#89cff0><u>Here</link></color></u> to access my instagram!\r\n\tClick <link=\"{AngeloWebsite}\"><color=#89cff0><u>Here</link></color></u> to access my website!",
                AboutLeave = "Back to Title",
                #endregion

                #region Extras
                    selector = new()
                    {
                        Animatronics = "Animatronics",
                        Interviews = "Interviews",
                        Minigames = "Minigames",
                        Extras = "Miscellaneous",
                        Cheats = "Cheats",
                        Settings = "Settings",

                        Exit = "Back"
                    },

                    settings = new()
                    {
                        videoSettings = new()
                    {
                        // Janelas
                        ResolutionTitle = "Resolution",
                        WindowModeTitle = "Display",
                        WindowModeOptions = new string[]
                        {
                            "Exclusive Screen",
                            "Full Screen",
                            "Borderless",
                            "Window"
                        },
                        ConstantSizeTitle = "Constant Size",

                        // Frames por segundo
                        FPSTitle = "Target FPS",
                        FPSUnlimitedOption = "Unlimited",
                        VsyncTitle = "Vsync",

                        // Qualidade
                        PostProcessingTitle = "Post Processing"
                    },

                        audioSettings = new()
                    {
                        mainTitle = "Main",
                        musicTitle = "Music",
                        ambeintTitle = "Ambient",
                        voiceTitle = "Phone Voice",

                        muteTitle = "Mute"
                    },

                        languageSettings = new()
                    {
                        // Idioma
                        LanguageTitle = "Language",
                        VoicerTitle = "Voice",

                        // Outros
                        SubtitlesTitle = "Subtitles",
                        SystemSyncTitle = "SystemSync"
                    },

                        controlsSettings = new()
                    {
                        ControlsText = @"Controls:

Q - Turn Left Light On/Off
D - Turn Right Light On/Off
A - Close Left Door
S - Use Camera
Spacebar - Tun Fan On/Off

C - Solve a problem

Ctrl - Flashlight

Esc - Hold for 5 seconds to returns to menu"
                    },

                        featuresSettings = new()
                    {
                        // Discord
                        DiscordTitle = "Discord Sync",
                        DiscordStatus = new string[]
                        {
                            "Connected",
                            "Disconnected"
                        },

                        // Outros
                        TimeActivity = "Playing Time",
                        TimeText = "{0}h {1}m {2}s"
                    },

                        videoButton = "Video",
                        audioButton = "Audio",
                        languageButton = "Language",
                        controlsButton = "Controls",
                        featuresButton = "Features",
                    
                        Title = "Settings",

                        DiscordDetails = "Setting up the game"
                    },
                #endregion

                #region PreparingToNight: ->
                WhatNight = "10 PM\n\nNight {0}",
                #endregion

                #region Journals: ->
                SkipText = "Press and hold <u>SpaceBar</u> to skip.",
                #endregion

                #region Main: ->
                CameraNames = new Dictionary<int, string>()
                {
                    { 1, "Dinner Area" },
                    { 2, "West Corner" },
                    { 3, "East Corner" },
                    { 4, "West Hallway" },
                    { 5, "East Hallway" },
                    { 6, "Party Room 2" },
                    { 7, "Party Room 1" },
                    { 8, "North Vent" },
                    { 9, "South Vent" },
                    { 10, "Main Hall" },
                    { 11, "Pacífica's Shelter" },
                    { 12, "Parts & Services" },
                    { 13, "Main Star" },
                    { 14, "Main Stage" },
                    { 15, "Kids Stage" },
                    { 16, "Kids Area" },
                    { 17, "Game Area" },
                    { 18, "Gift Corner" },
                    { 19, "Backstage" },
                    { 20, "Restroom" },
                    { 21, "Kitchen" }
                },

                NightText = "Night",

                PowerText = "Power Left",
                UsageText = "Power Usage",
                PressCTRL = "Press Ctrl to use the flashlight",

                WindUp = "Wind Up\nMusic Box",
                ChangeSong = "Change Song"
                #endregion
            }
        }
    };

    public static LanguagePack Get(LanguageID id) => languages[id];
}