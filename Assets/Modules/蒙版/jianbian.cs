using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class RefinedColorTransition : MonoBehaviour
{
    [Header("Post-Processing Settings")]
    public PostProcessVolume targetVolume;
    [Range(0.1f, 10f)] public float eachPhaseDuration = 2f;

    [Header("Color Settings")]
    [ColorUsage(false)] public Color paleGreen = new Color(0.3f, 1f, 0.3f); // 淡绿色
    [ColorUsage(false)] public Color paleBlue = new Color(0.3f, 0.3f, 1f);   // 淡蓝色
    [Range(-100f, 100f)] public float desaturatedValue = -100f; // 完全去饱和

    private ColorGrading colorGrading;
    private Color initialColor;
    private float initialSaturation;
    public GameObject shiwu;

    void Start()
    {
        if (targetVolume == null)
        {
            Debug.LogError("Post-Processing Volume未分配!");
            return;
        }

        if (!targetVolume.profile.TryGetSettings(out colorGrading))
        {
            Debug.LogError("无法找到Color Grading效果!");
            return;
        }

        // 保存初始状态
        initialColor = colorGrading.colorFilter.value;
        initialSaturation = colorGrading.saturation.value;
    }

    public void StartFullTransition()
    {
        StartCoroutine(FullTransitionCoroutine());
    }

    private IEnumerator FullTransitionCoroutine()
    {
        shiwu.SetActive(true);
        // 阶段3: 降低饱和度到-100 (完全黑白)
        yield return TransitionSaturationCoroutine(initialSaturation, desaturatedValue, eachPhaseDuration);

        // 等待4秒
        yield return new WaitForSeconds(5f);

        // 阶段4: 恢复饱和度
        yield return TransitionSaturationCoroutine(desaturatedValue, initialSaturation, eachPhaseDuration * 0.5f);

        // 阶段5: 淡蓝色 → 初始颜色
        yield return TransitionColorCoroutine(paleBlue, initialColor, eachPhaseDuration);
        shiwu.SetActive(false);

        Debug.Log("完整颜色过渡完成!");
    }

    private IEnumerator TransitionColorCoroutine(Color startColor, Color endColor, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);
            colorGrading.colorFilter.value = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }
        colorGrading.colorFilter.value = endColor;
    }

    private IEnumerator TransitionSaturationCoroutine(float startSat, float endSat, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);
            colorGrading.saturation.value = Mathf.Lerp(startSat, endSat, progress);
            yield return null;
        }
        colorGrading.saturation.value = endSat;
    }

    public void ResetToInitial()
    {
        if (colorGrading != null)
        {
            colorGrading.colorFilter.value = initialColor;
            colorGrading.saturation.value = initialSaturation;
        }
    }

    // 在Inspector中显示当前颜色预览
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            paleGreen = new Color(
                Mathf.Clamp(paleGreen.r, 0f, 1f),
                Mathf.Clamp(paleGreen.g, 0f, 1f),
                Mathf.Clamp(paleGreen.b, 0f, 1f)
            );

            paleBlue = new Color(
                Mathf.Clamp(paleBlue.r, 0f, 1f),
                Mathf.Clamp(paleBlue.g, 0f, 1f),
                Mathf.Clamp(paleBlue.b, 0f, 1f)
            );

            desaturatedValue = Mathf.Clamp(desaturatedValue, -100f, 100f);
        }
    }
}