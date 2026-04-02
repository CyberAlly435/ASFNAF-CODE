using UnityEngine;

using System.Collections.Generic;
using System.Collections;
using System;

using Random = UnityEngine.Random;

public class AnimatronicsBodies
{
    public enum Animatronics
    {
        // Última alteração em 9 de Janeiro de 2026

        /*
         * <REGISTRO>
         * Animatrônicos Alfa: Hanzo, Mang, Akemi;
         * Animatrônicos Beta: Balthazar, Jinx, Lie, Horacio, Danger FT Saas;
         * Animatrônicos Charlie: Pacífica, Seven, Light;
         * Animatrônicos Delta: Nexus
         * </REGISTRO>
         */

        Hanzo, // withered foxy
        Mang, // toy mangle
        Akemi, // toy chica

        Jinx, // chica
        Marcus, // bonnie
        Anknon, // freddy

        Balthazar, // toy bonnie
        Lie, // ennard
        Horacio, // freddy
        DangerFTSaas, // balloonboy

        Pacifica, // ballora
        Seven, // puppet
        Light, // funtime foxy

        // Secrets:

        Paperman_A,
        Paperman_B,
        Paperman_C
    }

    public struct EndoIA
    {
        public Dictionary<int, int> NightValues;

        public EndoIA(
            // Noite Padrão
            int night1,
            int night2,
            int night3,
            int night4,
            int night5,

            // Noite Extra
            int night6,
            int night7
        )
        {
            NightValues = new Dictionary<int, int>
            {
                { 1, night1 },
                { 2, night2 },
                { 3, night3 },
                { 4, night4 },
                { 5, night5 },
                { 6, night6 },
                { 7, night7 }
            };
        }
    }

    public abstract class EndoBase
    {
        // Atributos
        protected List<int> _cameras;
        protected int? _currentCamera;
        protected int? _spawnCamera;
        protected int? _ai;
        protected bool _onOffice;

        public string Name;

        public int? CurrentCamera
        {
            get
            {
                return _currentCamera;
            }
        }

        public bool OnOffice
        {
            get
            {
                return _onOffice;
            }
        }

        #region Métodos
        // Construtores e Destrutivos

        /// <summary>
        /// Construtor de EndoBase.
        /// </summary>
        /// 
        /// <param name="name">Nome do Animatrônico</param>
        protected EndoBase(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Destruidor de EndoBase.
        /// </summary>
        ~EndoBase()
        {
            this.Name = null;
        }

        // Funções
        public abstract IEnumerator OnAI();
        #endregion
    }

    // Classes Herdadas da Abstração do EndoBase

    public class EndoOne : EndoBase
    {
        // Atributos

        #region Métodos

        // Construtores e Destrutivos aqui em baixo

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="name">Nome do Animatrônico.</param>
        /// <param name="cameras">Todas as câmeras em que o Animatrônico pode percorrer.</param>
        /// <param name="endoIA">Estrutura de Dificuldade do Animatrônico.</param>
        /// <param name="night">Noite base.</param>
        public EndoOne(
            string name, // Propriedades herdadas de EndoBase

            List<int> cameras, EndoIA endoIA, int night // Propriedades específicas do EndoOne
        ) : base(name)
        {
            // aqui eu vou definir primeiro as câmeras e em seguida eu vou definir a câmera em que o Animatronic está e a câmera de Spawn.
            _cameras = cameras;

            _currentCamera = cameras[0];
            _spawnCamera = cameras[0];

            // aqui eu vou me preparar para inicializar os Animatrônicos.
            //this.OnAI();
        }

        ~EndoOne()
        {
            // destruir os valores
            _cameras = null;
            _currentCamera = null;
            _spawnCamera = null;
        }

        // Funções
        public override IEnumerator OnAI()
        {
            yield return null;
        }
        #endregion
    }

    public class EndoTwo : EndoBase
    {
        // Atributos
        private List<int> _newPath;

        #region Métodos
        // Construtores e Destrutivos
        public EndoTwo(
            string name, // Propriedades herdadas de EndoBase

            List<int> cameras, List<int> newPath, EndoIA endoIA, int night // Propriedades específicas de EndoTwo
        ) : base(name)
        {
            // aqui eu vou definir primeiro as câmeras e em seguida eu vou definir a câmera em que o Animatronic está e a câmera de Spawn.
            _cameras = cameras;
            _newPath = newPath;

            _currentCamera = cameras[0];
            _spawnCamera = cameras[0];

            // aqui eu vou me preparar para inicializar os Animatrônicos.
            //this.OnAI();
        }

        ~EndoTwo()
        {
            _cameras = null;
            _newPath = null;

            _currentCamera = null;
            _spawnCamera = null;
        }

        // Funções
        public override IEnumerator OnAI()
        {
            yield return null;
        }
        #endregion
    }

    public class EndoSpecial : EndoBase
    {
        // Atributos
        private int _check;

        public new List<int> _cameras;
        public new int? _currentCamera;
        public new int? _spawnCamera;
        public new int? _ai;
        public int check
        {
            get { return _check; }
            set
            {
                _check = value;

                if (!OnOffice)
                {
                    if (check < _cameras.Count)
                        _currentCamera = _cameras[_check];
                    else
                        _onOffice = true;
                }
            }
        }

        #region Métodos
        // Construtores e Destrutivos
        public EndoSpecial(
            Action<EndoSpecial> specialAction, string name // Funções de EndoSpecial e Propriedades herdadas de EndoBase
        ) : base(name) => specialAction(obj: this);

        ~EndoSpecial()
        {

        }

        // Outros métodos

        public override IEnumerator OnAI()
        {
            yield return null;
        }
        #endregion
    }

    #region Animatronics Próprios
    public class EndoMarionette : EndoBase
    {
        #region  Atributos da classe privados
        private int _musicBox_Counter = 2000;
        private int _marionette_Stage = 0;
        private int _musicBox_AddCounter;
        private int _musicBox_RemoveCounter;

        private bool _musicBox_PlayerRebooting;
        private bool _musicBox_Cooldown;
        private bool _marionette_Cooldown = false;

        private new int _currentCamera;

        private Main mainScript;
        private Dictionary<int, int> addCounters = new()
        {
            // noite padrão
            { 1, 15 },
            { 2, 15 },
            { 3, 13 },
            { 4, 11 },
            { 5, 8 },

            // noite extra
            { 6, 7 },
            { 7, 5 }
        };
        private Dictionary<int, int> removeCounters = new()
        {
            // noite padrão
            { 1, 4 },
            { 2, 4 },
            { 3, 5 },
            { 4, 7 },
            { 5, 8 },

            // noite extra
            { 6, 9 },
            { 7, 11 }
        };
        public int MarionetteStage
        {
            get { return _marionette_Stage; }
        }
        #endregion

        #region Métodos
        // Construtores e Destruidores
        public EndoMarionette(string name, Main main, int night) : base(name)
        {
            this._musicBox_AddCounter = this.addCounters[night];
            this._musicBox_RemoveCounter = this.removeCounters[night];

            this._cameras = new()
            {
                18, // Câmera 12
                1, // Câmera 1
                10, // Câmera 7
                5, // Câmera 2B
                3, // Câmera 1B
                0 // Office
            };
            this._currentCamera = this._cameras[0];

            this.mainScript = main;

            this.Name = name;
        }

        ~EndoMarionette()
        {
            this._cameras = null;

            this.mainScript = null;
        }

        // Métodos herdados
        public override IEnumerator OnAI()
        {
            while (this.mainScript)
            {
                if (this._cameras.Contains(this.mainScript.CameraValues))
                {
                    if (this.mainScript.CameraValues > 0)
                    {

                    }
                    else
                    {

                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        // Métodos próprios de Marionette

        /// <summary>
        /// Inicia o contador da caixa de música
        /// </summary>
        /// <returns></returns>
        public IEnumerator StartCounter()
        {
            // Enquanto a noite não termina ou o contador estiver acima de 0, ele alimenta o loop;
            while (this.mainScript.TimeWatch < 6 || this.mainScript.TimeWatch >= 10 || this._musicBox_Counter > 0)
            {
                // Caso o contador seja maior que 0 e o jogador não esteja rebobinando, ele subtrai
                // do contador o valor correspondente a noite;.
                if (this._musicBox_Counter > 0 && !this._musicBox_PlayerRebooting)
                {
                    // Checa se o cooldown está desativado;
                    if (!this._musicBox_Cooldown)
                        this._musicBox_Counter -= this._musicBox_RemoveCounter;

                    // delay de 1 décimo de segundo;
                    yield return new WaitForSeconds(0.1f);
                }
                // Caso o contador seja maior que 0 e o jogador esteja rebobinando, ele adiciona ao
                // contador o valor correspondente a noite;
                else if (this._musicBox_Counter > 0 && this._musicBox_PlayerRebooting)
                {
                    // Se o som de rebobinar não estiver tocando, ele toca o som;
                    if (!this.mainScript.audioManager.windup.isPlaying)
                        this.mainScript.audioManager.windup.Play();

                    // Adiciona o valor correspondente a noite até que ele alcance o inteiro 2000;
                    this._musicBox_Counter += Math.Clamp(this._musicBox_AddCounter, 0, 2000);

                    // Delay de 1 décimo de segundos para compensar a trabalheira;
                    yield return new WaitForSeconds(0.1f);
                }

                // Checa se o contador é maior que 0 antes de continuar as operações;
                if (this._musicBox_Counter > 0)
                {
                    // Checa se o contador é menor que 400;
                    if (this._musicBox_Counter <= 200)
                    {
                        // Se sim, ele dispara: 

                        // Dá o sprite da figura de aviso;
                        if (this.mainScript.layer9Class.WarningPuppet.sprite != this.mainScript.layer5Class.Warnings[1])
                            this.mainScript.layer9Class.WarningPuppet.sprite = this.mainScript.layer5Class.Warnings[1];

                        if (this.mainScript.layer5Class.WarningPuppet.sprite != this.mainScript.layer5Class.Warnings[1])
                            this.mainScript.layer5Class.WarningPuppet.sprite = this.mainScript.layer5Class.Warnings[1];

                        // Se a figura de aviso tiver a sua visibilidade diferente do valor da câmera maior que 0, ele atualiza 1 vez;
                        if (this.mainScript.layer5Class.WarningPuppet.isActiveAndEnabled != this.mainScript.CameraValues > 0)
                            this.mainScript.layer5Class.WarningPuppet.gameObject.SetActive(this.mainScript.CameraValues > 0);

                        // Pisca a figura do aviso enquanto estiver com a câmera no escritório;
                        // Se não estiver, ele esconde;
                        if (this.mainScript.CameraValues == 0)
                            this.mainScript.layer9Class.WarningPuppet.gameObject.SetActive(!this.mainScript.layer9Class.WarningPuppet.isActiveAndEnabled);
                        else
                            this.mainScript.layer9Class.WarningPuppet.gameObject.SetActive(false);

                        // Delay pra as pisca soarem mais urgentes;
                        yield return new WaitForSeconds(0.08f);
                    }
                    else if (this._musicBox_Counter > 200 && this._musicBox_Counter <= 400)
                    {
                        // Se sim, ele dispara: 

                        // Dá o sprite da figura de aviso;
                        if (this.mainScript.layer9Class.WarningPuppet.sprite != this.mainScript.layer5Class.Warnings[0])
                            this.mainScript.layer9Class.WarningPuppet.sprite = this.mainScript.layer5Class.Warnings[0];

                        if (this.mainScript.layer5Class.WarningPuppet.sprite != this.mainScript.layer5Class.Warnings[0])
                            this.mainScript.layer5Class.WarningPuppet.sprite = this.mainScript.layer5Class.Warnings[0];

                        // Se a figura de aviso tiver a sua visibilidade diferente do valor da câmera maior que 0, ele atualiza 1 vez;
                        if (this.mainScript.layer5Class.WarningPuppet.isActiveAndEnabled != this.mainScript.CameraValues > 0)
                            this.mainScript.layer5Class.WarningPuppet.gameObject.SetActive(this.mainScript.CameraValues > 0);

                        // Pisca a figura do aviso enquanto estiver com a câmera no escritório;
                        // Se não estiver, ele esconde;
                        if (this.mainScript.CameraValues == 0)
                            this.mainScript.layer9Class.WarningPuppet.gameObject.SetActive(!this.mainScript.layer9Class.WarningPuppet.isActiveAndEnabled);
                        else
                            this.mainScript.layer9Class.WarningPuppet.gameObject.SetActive(false);

                        // Delay pra as pisca soarem mais urgentes;
                        yield return new WaitForSeconds(0.15f);
                    }
                    else
                    {
                        // Checa se a figura de aviso está visível no escritório e caso esteja, desabilita;
                        if (this.mainScript.layer9Class.WarningPuppet.isActiveAndEnabled)
                            this.mainScript.layer9Class.WarningPuppet.gameObject.SetActive(false);
                
                        // Checa se a figura de aviso está visível nas câmeras e caso esteja, desabilita;
                        if (this.mainScript.layer5Class.WarningPuppet.isActiveAndEnabled)
                            this.mainScript.layer5Class.WarningPuppet.gameObject.SetActive(false);
                    }

                    // Se a câmera do jogador for a câmera 18, ele atualiza o sprite;
                    if (this.mainScript.CameraValues == 18)
                        this.mainScript.layer5Class.Counter.sprite = this.mainScript.layer5Class.Counters[Math.Clamp(this._musicBox_Counter / 95, 0, 21)];
                }
                else
                {
                    if (this._marionette_Stage <= 2)
                    {
                        // Adiciona 1 ao marionetteStage caso encontre a chave e o valor for menor ou igual a 0 em PossibleCameraCooldown;
                        if (this.mainScript.LocalCameraCooldown[_currentCamera].Value <= 0 && this._marionette_Cooldown)
                        {
                            _marionette_Stage++;

                            yield return new WaitForSeconds(5f);
                        
                            _marionette_Cooldown = false;
                        }

                        // Checa se a figura de aviso está visível no escritório e caso esteja, desabilita;
                        if (this.mainScript.layer9Class.WarningPuppet.isActiveAndEnabled)
                            this.mainScript.layer9Class.WarningPuppet.gameObject.SetActive(false);
                
                        // Checa se a figura de aviso está visível nas câmeras e caso esteja, desabilita;
                        if (this.mainScript.layer5Class.WarningPuppet.isActiveAndEnabled)
                            this.mainScript.layer5Class.WarningPuppet.gameObject.SetActive(false);

                        // Faz a verificação dos valores dentro do dicionário:
                        // se possuir, ele faz a mudança / se não possuir, ele cria;
                        // ATENÇÃO, APENAS ATÉ 250!;
                        
                        if (!this._marionette_Cooldown)
                        {
                            this.mainScript.LocalCameraCooldown[_currentCamera].Value = this.mainScript.CameraValues == _currentCamera ? 100 : 50 + this.mainScript.LocalCameraCooldown[_currentCamera].Value;

                            this._marionette_Cooldown = true;
                        }
                            
                        // Retorna um delay de 1 segundos;
                        yield return new WaitForSeconds(1f);
                    }
                    else
                    {
                        // Se marionetteStage for maior que 2, inicia a função que cobre a marionette;
                        this.mainScript.StartCoroutine(this.OnAI());

                        // Quebra o loop
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Acionar a ação de rebobinar a caixa de música da marionette;
        /// </summary>
        /// <param name="hovering">O jogador está com o ponteiro do mouse acima?</param>
        /// <returns></returns>
        public IEnumerator RebootingCounter(bool hovering)
        {
            // Começa a rebobinar;
            this._musicBox_PlayerRebooting = hovering;

            // Se não houver cooldown, cria um novo com um tempo de 5 segundos para desabilitar;
            if (!this._musicBox_Cooldown)
            {
                this._musicBox_Cooldown = true;

                if (!hovering)
                {
                    yield return new WaitForSeconds(5f);
                }

                this._musicBox_Cooldown = false;
            }
        }
        #endregion
    }

    #endregion
}

/*{
            //while (gamescript.TimeWatch < 6 && gamescript.Power > 0f || gamescript.TimeWatch >= 10 && gamescript.Power > 0f)
            //{
            //    yield return new WaitForSeconds(aI_Value > 20 ? (5f - 1f * MathF.Round(aI_Value / 10, 2)) : 5f);
            //
            //    if (Random.Range(0, 50) + 1 <= aI_Value)
            //        check++;
            //}

            yield return null;
        }*/