using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RestInPhysbonesEditorWindow : EditorWindow
{
    private int selectedLang = 0;

    private Vector2 scrollPos;

    [MenuItem("Tools/chocopoi/RestInPhysbones", false, 0)]
    public static void InitWindow()
    {
        RestInPhysbonesEditorWindow window = new RestInPhysbonesEditorWindow();
        window.titleContent = new GUIContent("RIP");
        window.Show();
    }

    /// <summary>
    /// Draws a horizontal line
    /// Reference: https://forum.unity.com/threads/horizontal-line-in-editor-window.520812/#post-3416790
    /// </summary>
    /// <param name="i_height">The line height</param>
    void DrawHorizontalLine(int i_height = 1)
    {
        EditorGUILayout.Separator();
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.Separator();
    }

    private void DrawLanguageSelectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Language 語言 言語:");
        selectedLang = GUILayout.Toolbar(selectedLang, new string[] { "English", "中文", "日本語" });

        if (selectedLang == 0)
        {
            //t.SetLocale("en");
        }
        else if (selectedLang == 1)
        {
            //t.SetLocale("zh");
        }
        else if (selectedLang == 2)
        {
            //t.SetLocale("jp");
        }
        GUILayout.EndHorizontal();
    }

    private void DrawToolHeaderGUI()
    {
        GUIStyle titleLabelStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 24
        };

        GUIStyle subtitleLabelStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 12
        };
        EditorGUILayout.LabelField("Rest In Physbones", titleLabelStyle, GUILayout.ExpandWidth(true), GUILayout.Height(30));
        EditorGUILayout.LabelField("????-?? ~ 2022-04 RIP", subtitleLabelStyle, GUILayout.ExpandWidth(true));

        EditorGUILayout.Separator();

        DrawHorizontalLine();
    }

    private void DrawToolFooterGUI()
    {
        DrawHorizontalLine();

        GUILayout.Label("Tool version");

        EditorGUILayout.SelectableLabel("https://github.com/poi-vrc/RestInPhysbones");
    }

    private static readonly float MIN_INTENSITY = 0.0f;

    private static readonly float MAX_INTENSITY = 10.0f;

    private VRC.SDK3.Avatars.Components.VRCAvatarDescriptor avatarToTweak;

    private GameObject dbDataRefObj;

    private float dampingIntensity = 1.0f; //spring 

    private float elasticityIntensity = 1.0f; //pull

    private float stiffnessIntensity = 1.0f;

    private float inertIntensity = 1.0f; //immobile

    private float radiusIntensity = 1.0f;

    private void DrawToolContentGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        avatarToTweak = (VRC.SDK3.Avatars.Components.VRCAvatarDescriptor)EditorGUILayout.ObjectField("Avatar To Tweak", avatarToTweak, typeof(VRC.SDK3.Avatars.Components.VRCAvatarDescriptor), true);

        EditorGUILayout.HelpBox("You can specify an object / prefab containing DynamicBones for referencing. It must contain a similar Armature with the avatar. Leave this empty to reference from the currrent avatar only.", MessageType.Info);

        dbDataRefObj = (GameObject)EditorGUILayout.ObjectField("DB Reference", dbDataRefObj, typeof(GameObject), true);

        dampingIntensity = EditorGUILayout.Slider("Damping/Spring/Momentum Intensity", dampingIntensity, MIN_INTENSITY, MAX_INTENSITY);

        elasticityIntensity = EditorGUILayout.Slider("Elasticity/Pull Intensity", elasticityIntensity, MIN_INTENSITY, MAX_INTENSITY);

        stiffnessIntensity = EditorGUILayout.Slider("Stiffness Intensity", stiffnessIntensity, MIN_INTENSITY, MAX_INTENSITY);

        inertIntensity = EditorGUILayout.Slider("Inert/Immobile Intensity", inertIntensity, MIN_INTENSITY, MAX_INTENSITY);

        radiusIntensity = EditorGUILayout.Slider("Radius Intensity", radiusIntensity, MIN_INTENSITY, MAX_INTENSITY);

        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// Renders the window
    /// </summary>
    void OnGUI()
    {
        DrawLanguageSelectorGUI();
        DrawToolHeaderGUI();

        if (Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Exit play mode", MessageType.Warning);
        }
        else
        {
            DrawToolContentGUI();
        }

        DrawToolFooterGUI();
    }
}
