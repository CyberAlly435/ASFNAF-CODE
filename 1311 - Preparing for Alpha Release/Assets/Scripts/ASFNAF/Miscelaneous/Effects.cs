using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ASFNAF.Miscelaneus;

public static class Effects
{
    // # IMPORTADO DE ANTIGO WARNING AUXILIATOR

    /// <summary>
    ///     Provê a animação Fade a um objeto, com controle em In ou Out e duração.
    /// </summary>
    /// 
    /// <param name="fadeobj">Objeto UI em que irá receber o fade. Exemplo: Text, Image e RawImage.</param>
    /// <param name="isEnd">Este Bool informa se o efeito deve ser aplicado no início ou fim.</param>
    /// <param name="duration">A duração do efeito antes de acabar em float.</param>
    /// 
    /// <returns>
    ///     O float duration é o único que é Opcional!
    /// </returns>
    public static IEnumerator Fade(Graphic fadeobj, bool isEnd, float duration = 1.5f)
    {
        /// <summary>
        /// 12/24/2025: Vibecoding não faz muito sentido ao meu ver.
        /// 
        /// No início do projeto eu estava usando IA para me ajudar a fazer grande parte dos scripts, maasss...
        /// Eu dei uma olhada no Masterpiece do Warning Auxiliator e eu não achei sentido usar uma função tão grande
        /// que o chatgpt fez. Eu dei uma pesquisada e o próprio Unity possui um FadeIn e FadeOut próprio na qual é
        /// até que mais eficiente do que a MERDA do script abaixo. Eu separei o que o chatgpt fez e o que eu fiz
        /// para um teste antes de continuar para ver se devo seguir a recomendação de uma IA ou a minha própria.
        /// 
        /// Resultado: O meu script simples é bem melhor do que o código do chatgpt,
        ///     
        ///     Ganhei com 330FPS na alta do Editor contra 290FPS na alta do Editor usando o chatgpt.
        ///     (Target Frame Time: 320FPS)O tempo de CPU aumentou, chengando a máxima de 3ms do processador, mas
        ///     em compensação, o FPS está alto e segue as normas do que eu busco.
        ///     
        ///     Usando o Shader padrão do Unity
        /// </summary>

        // Esse aqui foi feito pelo chatgpt:
        /*
        StartAlpha = !isEnd ? 1f : 0f;
        EndAlpha = !isEnd ? 0f : 1f;

        elapsed2 = 0f;

        var renderer = FadeImage.canvasRenderer;

        renderer.SetAlpha(StartAlpha);

        while (elapsed2 < duration)
        {
            elapsed2 += Time.deltaTime;

            float timeX = Mathf.Clamp01(elapsed2 / duration);
            float alphaX = Mathf.Lerp(StartAlpha, EndAlpha, timeX);

            renderer.SetAlpha(alphaX);

            yield return null;
        }
        */

        // Esse foi feito por mim:

        fadeobj.CrossFadeAlpha(isEnd ? 1 : 0, duration, false);

        yield return null;
    }

    /// <param name="asyncLoadScene">String contendo o nome da cena para ser carregada.</param>
    /// 
    /// <returns>
    ///     Obrigatórios: asyncLoadScene;
    ///     Opcionais: needFade;
    /// </returns>
    public static IEnumerator LoadScene(string asyncLoadScene, float duration = 1.5f)
    {
        yield return new WaitForSeconds(duration);

        AsyncOperation loadScene;

        loadScene = SceneManager.LoadSceneAsync(asyncLoadScene);
        loadScene.allowSceneActivation = false;

        while (loadScene.progress < 0.9f)
            yield return new WaitForSeconds(0.1f);

        loadScene.allowSceneActivation = true;
    }

    /// <summary>
    ///     Provê o início e final de animação Fade a um objeto, com controle em In ou Out, duração,
    ///     Intervalos, Solicitações para pular intervalos e Carregamento de Cena com String.
    /// </summary>
    /// 
    /// <param name="fadeobj">Objeto UI em que irá receber o fade. Exemplo: Text, Image e RawImage.</param>
    /// 
    /// <param name="duration">Dá a duração do efeito via valor Float.</param>
    /// <param name="interval">Dá um intervalo necessário para que inicie o próximo efeito.</param>
    /// <param name="requestToSkip">Fornece um booleano para pular o intervalo, trazendo de volta o bool para o script que solicitou o IEnumerador.</param>
    /// <param name="asyncLoadScene">Fornece uma string para passar para outra cena no final dos efeitos.</param>
    /// 
    /// <returns>
    ///     Obrigatórios: fadeobj;
    ///     Opcionais: duration, interval, requestToSkip, asyncLoadScene.
    ///     
    ///     Excessões: Se interval existir, requestToSkip também deve existir.
    /// </returns>
    public static IEnumerator Fade_Play(Graphic fadeobj, float? duration, float? interval, System.Func<bool> requestToSkip, string asyncLoadScene)
    {
        yield return Fade(fadeobj, false, duration ?? 1.5f);
        yield return new WaitForSeconds(duration ?? 1.5f);

        if (interval != null)
        {
            var elapsed = 0f;

            while (elapsed < interval && !requestToSkip())
            {
                elapsed += Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }
        }

        yield return Fade(fadeobj, true, duration ?? 1.5f);

        if (asyncLoadScene != null)
            yield return LoadScene(asyncLoadScene, duration ?? 1.5f);
    }
}