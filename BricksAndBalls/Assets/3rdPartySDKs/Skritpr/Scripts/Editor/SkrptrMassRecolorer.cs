#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Editor Window for the mass colorizer!
/// </summary>
public class SkrptrMassRecolorer : EditorWindow
{
    /// <summary>
    /// If checked, it will only replace specific colors, if not, everything that has a color and is checked.
    /// </summary>
    private bool replaceOnlySpecificColor;

    /// <summary>
    /// Components in which the color shall be replaced
    /// </summary>
    private bool replaceInImage, replaceInText, replaceInRawImage;

    /// <summary>
    /// Checkbox that indicates wether or not children transforms will be considered or not.
    /// </summary>
    private bool includeChildren;

    /// <summary>
    /// Color which will be replaced
    /// </summary>
    private Color colorToReplace;

    /// <summary>
    /// New color which will be replaced with.
    /// </summary>
    private Color replacementColor;

     
    [MenuItem("Window/Skrptr Mass Colorer")]
    public static void ShowWindow()
    {
        GetWindow<SkrptrMassRecolorer>("Skrptr Mass Recolorer");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("This allows you to switch colors from within your editor selection with ease :) ");

        //Only specific Color
        replaceOnlySpecificColor = GUILayout.Toggle(replaceOnlySpecificColor, "Replace Only Specific Color?");
        if (replaceOnlySpecificColor)
            colorToReplace = EditorGUILayout.ColorField("Color To Replace: ", colorToReplace);

        replacementColor = EditorGUILayout.ColorField("Replacement Color: ", replacementColor);

        //What to replace
        GUILayout.BeginHorizontal();
        replaceInImage = GUILayout.Toggle(replaceInImage, "Replace in Image");
        replaceInText = GUILayout.Toggle(replaceInText, "Replace in Text");
        GUILayout.EndHorizontal();
        replaceInRawImage = GUILayout.Toggle(replaceInRawImage, "Replace in RawImage");

        includeChildren = GUILayout.Toggle(includeChildren, "Include Children of selection?");

        //Let's color!
        if (GUILayout.Button("Colorize"))
        {
            //Populate Lists
            List<Transform> transformsToColor = new List<Transform>();
            List<Transform> children = new List<Transform>();
            transformsToColor.AddRange(Selection.transforms);
            if(includeChildren)
            {
                foreach (var tf in transformsToColor)
                {
                    children.AddRange(tf.GetComponentsInChildren<Transform>());
                }
                transformsToColor.AddRange(children);
            }

            //Start recoloring
            foreach (var tf in transformsToColor)
            {
                //Replace Images
                if (replaceInImage)
                {
                    if (tf.GetComponent<Image>() != null)
                    {
                        Image img = tf.GetComponent<Image>();
                        if (replaceOnlySpecificColor)
                        {
                            if (colorToReplace == img.color)
                                img.color = replacementColor;
                        }
                        else
                        {
                            img.color = replacementColor;
                        }

                    }
                }
                //Replace Texts
                if (replaceInText)
                {
                    if (tf.GetComponent<Text>() != null)
                    {
                        Text txt = tf.GetComponent<Text>();
                        if (replaceOnlySpecificColor)
                        {
                            if (colorToReplace == txt.color)
                                txt.color = replacementColor;
                        }
                        else
                        {
                            txt.color = replacementColor;
                        }
                    }
                }
                //Replace Raw Image
                if (replaceInRawImage)
                {
                    if (tf.GetComponent<RawImage>() != null)
                    {
                        RawImage rawImg = tf.GetComponent<RawImage>();
                        if (replaceOnlySpecificColor)
                        {
                            if (colorToReplace == rawImg.color)
                                rawImg.color = replacementColor;
                        }
                        else
                        {
                            rawImg.color = replacementColor;
                        }
                    }
                }
            }//end foreach
            GameObject go = new GameObject();
            go.transform.parent = transformsToColor[0];
            DestroyImmediate(go);
        }
    }


}
#endif