using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;


public class ColorFadeEffect : MonoBehaviour, ITimed
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Effect's Properties")]
    [Space(4)]
    [Range(speedTransitionMin, speedTransitionMax)]
    public float speedTransition = 1f;
    [Range(0.0f, 1.0f)]
    public float alphaIntensity = 1f;
    [Tooltip("Will the transition effetc always repeat itself?")]
    public bool repeat = true;
    [Tooltip("Fade In   Effetc = Black to Color [true].\nFade Out Effect = Color to Black [false].")]
    public bool applyFadeIn = true;

    [Space(5)]
    [Header("+ Options for the Render Component")]
    [Space(4)]
    [Tooltip("This color will be used only if the GameObject does not have the following components: Text and Image.")]
    public Color renderColor = Color.white;

    [Space(5)]
    [Header("+ Timer of Effect")]
    [Space(4)]
    [Tooltip("Wait Time to Start")]
    [Range(0.0f, 60.0f)]
    public float waitTimeToStart;
    [Tooltip("The Duration of the Effect")]
    [Range(0.0f, 10.0f)]
    public float effectRuntime;
    [Tooltip("Disable GameObject at end of effect runtime")]
    public bool disableGameObject;
    [Tooltip("Destroy GameObject at end of effect runtime")]
    public bool destroyGameObject;

    [Space(5)]
    [Header("+ Children's Elements")]
    [Space(4)]
    [Tooltip("Apply the same settings to all GameObject children")]
    public bool applySameProperties;
    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private const float speedTransitionMin = 0.1f;
    private const float speedTransitionMax = 10f;

    private Chronometer timerForRuntime;
    private Chronometer timerForWaitTime;
    private bool timerForRuntimeInitiated;
    private bool timerForWaitTimeInitiated;

    private Interpolate interpolate;

    private Color tempColor;
    private float h;
    private float s;
    private float v;

    //for check SpriteRenderer
    private bool existsSpriteRenderer;

    //for check changes
    private float lastSpeedTransition;
    private float lastAlphaIntensity;
    private bool lastRepeat;
    private bool lastApplyFadeIn;
    private float lastWaitTimeToStart;
    private float lastEffectRuntime;
    private bool lastDisableGameObject;
    private bool lastDestroyGameObject;
    private bool lastApplySameProperties;

    #endregion

    #region Messages Methods of MonoBehaviour    
    void Start()
    {
        UpdateChildren();

        #region timers
        timerForWaitTimeInitiated = false;
        timerForRuntimeInitiated = false;

        if (effectRuntime > 0)
        {
            timerForRuntime = new Chronometer(effectRuntime, this);
        }
        else
        {
            timerForRuntime = new Chronometer();
        }

        if (waitTimeToStart > 0)
        {
            timerForWaitTime = new Chronometer(waitTimeToStart);
        }
        else
        {
            timerForWaitTime = new Chronometer();
        }
        #endregion

        StartRenderColorsAndHSV();

        //initiated last attributes
        lastSpeedTransition = speedTransition;
        lastAlphaIntensity = alphaIntensity;
        lastRepeat = repeat;
        lastApplyFadeIn = applyFadeIn;
        lastWaitTimeToStart = waitTimeToStart;
        lastEffectRuntime = effectRuntime;
        lastDisableGameObject = disableGameObject;
        lastDestroyGameObject = destroyGameObject;
        lastApplySameProperties = applySameProperties;
    }

    void Update()
    {
        //CheckChangeAttributes();

        speedTransition = speedTransition > speedTransitionMax ? speedTransitionMax : speedTransition;
        speedTransition = speedTransition < speedTransitionMin ? speedTransitionMin : speedTransition;

        if (waitTimeToStart > 0 && !timerForWaitTime.IsReachTimeGoal())
        {
            if (!timerForWaitTimeInitiated)
            {
                timerForWaitTime.Restart();
                timerForWaitTimeInitiated = true;
            }
        }
        else if (waitTimeToStart <= 0 || timerForWaitTime.IsReachTimeGoal())
        {
            if (!timerForRuntimeInitiated)
            {
                timerForRuntime.Restart();
                timerForRuntimeInitiated = true;
                StartInterpolate();
            }

            Image imageComponent = GetComponent<Image>();
            if (imageComponent != null)
            {
                if (repeat && !timerForRuntime.IsReachTimeGoal())
                {
                    tempColor.a = interpolate.GetCurrentPingPongValue();
                    imageComponent.color = tempColor;
                }
                else if (!repeat && !timerForRuntime.IsReachTimeGoal())
                {
                    tempColor.a = interpolate.GetCurrentValue();
                    imageComponent.color = tempColor;
                }
            }

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                if (repeat && !timerForRuntime.IsReachTimeGoal())
                {
                    tempColor.a = interpolate.GetCurrentPingPongValue();
                    spriteRenderer.color = tempColor;
                }
                else if (!repeat && !timerForRuntime.IsReachTimeGoal())
                {
                    tempColor.a = interpolate.GetCurrentValue();
                    spriteRenderer.color = tempColor;
                }

                existsSpriteRenderer = true;
            }
            else
            {
                existsSpriteRenderer = false;
            }

            Text textComponent = GetComponent<Text>();
            if (textComponent != null)
            {
                if (repeat && !timerForRuntime.IsReachTimeGoal())
                {
                    tempColor.a = interpolate.GetCurrentPingPongValue();
                    textComponent.color = tempColor;
                }
                else if (!repeat && !timerForRuntime.IsReachTimeGoal())
                {
                    tempColor.a = interpolate.GetCurrentValue();
                    textComponent.color = tempColor;
                }
            }

            Renderer shaderRenderer = GetComponent<Renderer>();
            if (shaderRenderer != null && !existsSpriteRenderer)
            {                
                if (repeat && !timerForRuntime.IsReachTimeGoal())
                {
                    v = interpolate.GetCurrentPingPongValue();
                    tempColor = Color.HSVToRGB(h, s, v);
                    MaterialPropertiesHandler.SetAlbedoColor(shaderRenderer, tempColor);
                }
                else if (!repeat && !timerForRuntime.IsReachTimeGoal())
                {
                    v = interpolate.GetCurrentValue();
                    tempColor = Color.HSVToRGB(h, s, v);
                    MaterialPropertiesHandler.SetAlbedoColor(shaderRenderer, tempColor);
                }
            }

            //UpdateChildren();
        }

        #region debug tests
        // print(
        //    "[BASE] R (" + baseColor.r + ") - " +
        //    "G (" + baseColor.g + ") - " +
        //    "B (" + baseColor.b + ") - " +
        //    "A (" + baseColor.a + ")\n" +
        //    "--------------------------------\n" +
        //    "[TEMP] R (" + tempBaseColor.r + ") - " +
        //    "G (" + tempBaseColor.g + ") - " +
        //    "B (" + tempBaseColor.b + ") - " +
        //    "A (" + tempBaseColor.a + ")\n" +
        //    "--------------------------------\n" +
        //    "[" + cont + "] Timer: " + timer.GetElapsed() + "s"
        //);
        #endregion

        CheckChangeAttributes();
    }


    void OnEnable()
    {
        Start();
    }
    #endregion

    #region Other Methods: CheckChangeAttributes(), OnReachTimeGoal(), StartBaseColorsAndHSV(), StartInterpolate(), UpdateChildren(), BecomeBlack()
    private void CheckChangeAttributes()
    {
        if (lastSpeedTransition != speedTransition ||
            lastAlphaIntensity != alphaIntensity ||
            lastRepeat != repeat ||
            lastApplyFadeIn != applyFadeIn ||
            lastWaitTimeToStart != waitTimeToStart ||
            lastEffectRuntime != effectRuntime ||
            lastDisableGameObject != disableGameObject ||
            lastDestroyGameObject != destroyGameObject ||
            lastApplySameProperties != applySameProperties)
        {
            Start();

            #region debug tests
            // print(
            //    gameObject.name + "\n" +
            //     "lastSpeedTransition: " + lastSpeedTransition + " - speedTransition: " + speedTransition + "\n" +
            //    "lastAlphaIntensity: " + lastAlphaIntensity + " - alphaIntensity: " + alphaIntensity + "\n" +
            //    "lastRepeat: " + lastRepeat + " - repeat: " + repeat + "\n" +
            //    "lastApplyFadeIn: " + lastApplyFadeIn + " - applyFadeIn: " + applyFadeIn + "\n" +
            //    "lastWaitTimeToStart: " + lastWaitTimeToStart + " - waitTimeToStart: " + waitTimeToStart + "\n" +
            //    "lastEffectRuntime: " + lastEffectRuntime + " - effectRuntime: " + effectRuntime + "\n" +
            //    "lastDisableGameObject: " + lastDisableGameObject + " - disableGameObject: " + disableGameObject + "\n" +
            //    "lastDestroyGameObject: " + lastDestroyGameObject + " - destroyGameObject: " + destroyGameObject + "\n" +
            //    "lastApplySameProperties: " + lastApplySameProperties + " - applySameProperties: " + applySameProperties
            //);
            #endregion
        }
    }

    public void OnReachTimeGoal()
    {
        if (disableGameObject)
        {
            gameObject.SetActive(false);
        }

        if (destroyGameObject)
        {
            Destroy(gameObject);
        }

        if (!disableGameObject || !destroyGameObject)
        {
            BecomeBlack();
        }
    }

    private void StartRenderColorsAndHSV()
    {
        Image imageComponent = GetComponent<Image>();
        if (imageComponent != null)
        {
            tempColor = imageComponent.color;
            
            if (applyFadeIn)
            {
                tempColor.a = 0;
            }
            else
            {
                tempColor.a = alphaIntensity;
            }

            imageComponent.color = tempColor;
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            tempColor = spriteRenderer.color;

            if (applyFadeIn)
            {
                tempColor.a = 0;
            }
            else
            {
                tempColor.a = alphaIntensity;
            }

            spriteRenderer.color = tempColor;

            existsSpriteRenderer = true;
        }
        else
        {
            existsSpriteRenderer = false;
        }

        Text textComponent = GetComponent<Text>();
        if (textComponent != null)
        {
            tempColor = textComponent.color;

            if (applyFadeIn)
            {
                tempColor.a = 0;
            }
            else
            {
                tempColor.a = alphaIntensity;
            }

            textComponent.color = tempColor;
        }

        Renderer shaderRenderer = GetComponent<Renderer>();
        if (shaderRenderer != null && !existsSpriteRenderer)
        {
            Color.RGBToHSV(renderColor, out h, out s, out v);

            if (applyFadeIn)
            {
                v = 0;
            }
            else
            {
                v = alphaIntensity;
            }

            tempColor = Color.HSVToRGB(h, s, v);
            MaterialPropertiesHandler.SetAlbedoColor(shaderRenderer, tempColor);
        }
    }

    private void StartInterpolate()
    {
        if (applyFadeIn)
        {
            interpolate = new Interpolate(0, alphaIntensity, speedTransition);
        }
        else
        {
            interpolate = new Interpolate(alphaIntensity, 0, speedTransition);
        }
    }

    private void UpdateChildren()
    {
        if (applySameProperties)
        {
            for (int indexChild = 0; indexChild < transform.childCount; indexChild++)
            {
                if (transform.GetChild(indexChild).gameObject.GetComponent<ColorFadeEffect>() == null)
                {
                    transform.GetChild(indexChild).gameObject.AddComponent<ColorFadeEffect>();
                }

                ColorFadeEffect colorFadeEffect = transform.GetChild(indexChild).gameObject.GetComponent<ColorFadeEffect>();
                colorFadeEffect.renderColor = renderColor;
                colorFadeEffect.speedTransition = speedTransition;
                colorFadeEffect.alphaIntensity = alphaIntensity;
                colorFadeEffect.repeat = repeat;
                colorFadeEffect.applyFadeIn = applyFadeIn;
                colorFadeEffect.waitTimeToStart = waitTimeToStart;
                colorFadeEffect.effectRuntime = effectRuntime;
                colorFadeEffect.disableGameObject = disableGameObject;
                colorFadeEffect.destroyGameObject = destroyGameObject;
                colorFadeEffect.applySameProperties = applySameProperties;
            }
        }
    }

    public void BecomeBlack()
    {
        tempColor.a = 0;

        Image imageComponent = GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.color = tempColor;
        }

        Text textComponent = GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.color = tempColor;
        }

        Renderer shaderRenderer = GetComponent<Renderer>();
        if (shaderRenderer != null)
        {
            v = 0;
            tempColor = Color.HSVToRGB(h, s, v);
            MaterialPropertiesHandler.SetAlbedoColor(shaderRenderer, tempColor);
        }
    }

    public void ResetComponent()
    {
        Start();
    }

    #endregion

}