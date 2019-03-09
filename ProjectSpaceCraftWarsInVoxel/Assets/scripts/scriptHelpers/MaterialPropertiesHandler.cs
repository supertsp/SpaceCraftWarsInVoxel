using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class MaterialPropertiesHandler : MonoBehaviour
{

    #region SetTextures...
    public static void SetAlbedoTexture(Renderer objectRenderer, Texture newTexture)
    {
        objectRenderer.material.SetTexture("_MainTex", newTexture);
    }

    public static void SetMetallicTexture(Renderer objectRenderer, Texture newTexture)
    {
        objectRenderer.material.SetTexture("_MetallicGlossMap", newTexture);
    }

    public static void SetNormalMapTexture(Renderer objectRenderer, Texture newTexture)
    {
        objectRenderer.material.SetTexture("_BumpMap", newTexture);
    }

    public static void SetOcclusionTexture(Renderer objectRenderer, Texture newTexture)
    {
        objectRenderer.material.SetTexture("_OcclusionMap", newTexture);
    }

    public static void SetHeightMapTexture(Renderer objectRenderer, Texture newTexture)
    {
        objectRenderer.material.SetTexture("_ParallaxMap", newTexture);
    }

    public static void SetDetailMaskTexture(Renderer objectRenderer, Texture newTexture)
    {
        objectRenderer.material.SetTexture("_DetailMask", newTexture);
    }

    public static void SetEmissionTexture(Renderer objectRenderer, Texture newTexture)
    {
        //Activates Keyword but when the Material/Shader component expands this Keyword 
        //is automatically disabled.
        //The best solution is to manually activate the Emission.
        objectRenderer.material.EnableKeyword("_EMISSION");
        objectRenderer.material.SetTexture("_EmissionMap", newTexture);
    }
    #endregion

    #region GetTextures...
    public static Texture GetAlbedoTexture(Renderer objectRenderer)
    {
        return objectRenderer.material.GetTexture("_MainTex");
    }

    public static Texture GetMetallicTexture(Renderer objectRenderer)
    {
        return objectRenderer.material.GetTexture("_MetallicGlossMap");
    }

    public static Texture GetNormalMapTexture(Renderer objectRenderer)
    {
        return objectRenderer.material.GetTexture("_BumpMap");
    }

    public static Texture GetOcclusionTexture(Renderer objectRenderer)
    {
        return objectRenderer.material.GetTexture("_OcclusionMap");
    }

    public static Texture GetHeightMapTexture(Renderer objectRenderer)
    {
        return objectRenderer.material.GetTexture("_ParallaxMap");
    }

    public static Texture GetDetailMaskTexture(Renderer objectRenderer)
    {
        return objectRenderer.material.GetTexture("_DetailMask");
    }

    public static Texture GetEmissionTexture(Renderer objectRenderer)
    {
        return objectRenderer.material.GetTexture("_EmissionMap");
    }
    #endregion

    #region SetColors...
    public static void SetAlbedoColor(Renderer objectRenderer, Color newColor)
    {
        objectRenderer.material.SetColor("_Color", newColor);
    }

    public static void SetEmissionColor(Renderer objectRenderer, Color newColor)
    {
        objectRenderer.material.SetColor("_EmissionColor", newColor);
    }
    #endregion

    #region GetColors...
    public static Color GetAlbedoColor(Renderer objectRenderer)
    {
        return objectRenderer.material.GetColor("_Color");
    }

    public static Color SetEmissionColor(Renderer objectRenderer)
    {
        return objectRenderer.material.GetColor("_EmissionColor");
    }
    #endregion

    #region SetValues...
    public static void SetOffset(Renderer objectRenderer, float x, float y)
    {
        objectRenderer.material.SetTextureOffset("_MainTex", new Vector2(x, y));
    }

    public static void SetTiling(Renderer objectRenderer, float x, float y)
    {
        objectRenderer.material.SetTextureScale("_MainTex", new Vector2(x, y));
    }

    public static void SetMetallicScale(Renderer objectRenderer, float newValue)
    {
        objectRenderer.material.SetFloat("_Metallic", newValue);
    }

    public static void SetSmoothnessScale(Renderer objectRenderer, float newValue)
    {
        objectRenderer.material.SetFloat("_Glossiness", newValue);
    }

    public static void SetNormalMapValue(Renderer objectRenderer, float newValue)
    {
        objectRenderer.material.SetFloat("_BumpScale", newValue);
    }

    public static void SetHeightMapScale(Renderer objectRenderer, float newValue)
    {
        objectRenderer.material.SetFloat("_Parallax", newValue);
    }

    public static void SetOcclusionScale(Renderer objectRenderer, float newValue)
    {
        objectRenderer.material.SetFloat("_OcclusionStrength", newValue);
    }

    public static void SetEmissionValue(Renderer objectRenderer, float newValue)
    {
        Color tempColor = objectRenderer.material.GetColor("_EmissionColor");
        tempColor *= newValue;
        SetEmissionColor(objectRenderer, tempColor);
    }
    #endregion

    #region GetValues...
    public static Vector2 GetOffset(Renderer objectRenderer)
    {
        return objectRenderer.material.GetTextureOffset("_MainTex");
    }

    public static Vector2 GetTiling(Renderer objectRenderer)
    {
        return objectRenderer.material.GetTextureScale("_MainTex");
    }

    public static float GetMetallicScale(Renderer objectRenderer)
    {
        return objectRenderer.material.GetFloat("_Metallic");
    }

    public static float GetSmoothnessScale(Renderer objectRenderer)
    {
        return objectRenderer.material.GetFloat("_Glossiness");
    }

    public static float GetNormalMapValue(Renderer objectRenderer)
    {
        return objectRenderer.material.GetFloat("_BumpScale");
    }

    public static float GetHeightMapScale(Renderer objectRenderer)
    {
        return objectRenderer.material.GetFloat("_Parallax");
    }

    public static float GetOcclusionScale(Renderer objectRenderer)
    {
        return objectRenderer.material.GetFloat("_OcclusionStrength");
    }

    public static float GetEmissionValue(Renderer objectRenderer)
    {
        //The values of Color.r, Color.g and Color.b are the same.
       return objectRenderer.material.GetColor("_EmissionColor").r;
    }
    #endregion

}