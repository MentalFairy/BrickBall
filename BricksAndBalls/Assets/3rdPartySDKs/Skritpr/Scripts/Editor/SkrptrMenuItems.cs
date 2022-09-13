#if UNITY_EDITOR
using Skrptr.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
namespace Skrptr
{
    /// <summary>
    /// Some commands that might simplify the development process:
    /// </summary>
    public static class SkrptrMenuItems
    {
        /// <summary>
        /// Anchors selection to current rect sizes (Really useful and time saving if you wish to build scalable UI's
        /// </summary>
        [MenuItem("Skrptr/Extra/Auto-Anchor %h")]
        private static void AutoAnchor()
        {
            if (Selection.transforms.Length > 1)
            {
                foreach (var tf in Selection.gameObjects)
                {
                    AnchorTransform(tf.GetComponent<RectTransform>());
                }
            }
            else
            {
                AnchorTransform(Selection.activeTransform as RectTransform);
            }
        }

        [MenuItem("Skrptr/Extra/Set Source Transform &c")]
        private static void SetSourceTransform()
        {
            if (Selection.transforms.Length > 1)
            {
                sourceTransform = Selection.gameObjects[0].GetComponent<RectTransform>();
            }
            else
            {
                sourceTransform = Selection.activeGameObject.GetComponent<RectTransform>();
            }
            if (Application.isEditor)
                Debug.Log($"Set Source Copy Transform to {sourceTransform.gameObject.name}");
        }
        [MenuItem("Skrptr/Extra/Paste Source Transform Values &v")]
        private static void PasteSourceTransformIntoTargets()
        {
            if (Selection.transforms.Length > 1)
            {
                foreach (var tf in Selection.gameObjects)
                {
                    DeepPasteValuesIntoTransform(tf.GetComponent<RectTransform>());
                }
            }
            else
            {
                DeepPasteValuesIntoTransform(Selection.activeGameObject.GetComponent<RectTransform>());
            }
        }

        private static RectTransform sourceTransform;
        private static void DeepPasteValuesIntoTransform(RectTransform target)
        {

            if (Application.isEditor)
            {
                if(sourceTransform == null)
                {
                    Debug.LogError("Source Transform not set, use CTRL + ALT + C to copy a target source transform.");
                    return;
                }

                Debug.Log($"Pasting Transform from Source {sourceTransform.gameObject.name} to {target.gameObject.name}");
            }
            target.anchorMin = sourceTransform.anchorMin;
            target.anchorMax = sourceTransform.anchorMax;
            target.pivot = sourceTransform.pivot;
            target.anchoredPosition = sourceTransform.anchoredPosition;
            target.anchoredPosition3D = sourceTransform.anchoredPosition3D;
            target.sizeDelta = sourceTransform.sizeDelta;
        }

        /// <summary>
        /// Anchors transform to rect sizes.
        /// </summary>
        /// <param name="tf">Transform target</param>
        private static void AnchorTransform(RectTransform tf)
        {
            RectTransform pt = tf.parent as RectTransform;

            if (tf == null || pt == null) return;

            if (pt.rect.width != 0 && pt.rect.height != 0)
            {
                Vector2 newAnchorsMin = new Vector2(tf.anchorMin.x + tf.offsetMin.x / pt.rect.width,
                    tf.anchorMin.y + tf.offsetMin.y / pt.rect.height);
                Vector2 newAnchorsMax = new Vector2(tf.anchorMax.x + tf.offsetMax.x / pt.rect.width,
                    tf.anchorMax.y + tf.offsetMax.y / pt.rect.height);

                tf.anchorMin = newAnchorsMin;
                tf.anchorMax = newAnchorsMax;
                tf.offsetMin = tf.offsetMax = new Vector2(0, 0);
            }
        }
        /// <summary>
        /// All children will be added a KeyboardMapper and mapped horizontally. 
        /// </summary>
        [MenuItem("Skrptr/Neighbours/Neighbour horizontally")]
        private static void SetNeighboursHorizontally()
        {
            //Fetch Elements
            Transform[] tfs = Selection.transforms.OrderBy(tf => tf.gameObject.name).ToArray();
            SkrptrKeyboardMapper[] mappers = new SkrptrKeyboardMapper[tfs.Length];
            for (int i = 0; i < tfs.Length; i++)
            {
                if (tfs[i].GetComponent<SkrptrKeyboardMapper>() == null)
                {
                    tfs[i].gameObject.AddComponent<SkrptrKeyboardMapper>();
                    mappers[i] = tfs[i].GetComponent<SkrptrKeyboardMapper>();
                }
                else
                {
                    mappers[i] = tfs[i].GetComponent<SkrptrKeyboardMapper>();
                }
                if (mappers[i].neighbours == null)
                    mappers[i].neighbours = new List<SkrptrNeighbour>();
            }

            //Mapping
            mappers[0].neighbours.Add(new SkrptrNeighbour(NeighbourDirection.Right, mappers[1].gameObject));
            mappers[mappers.Length - 1].neighbours.Add(new SkrptrNeighbour(NeighbourDirection.Left, mappers[mappers.Length - 2].gameObject));
            for (int i = 1; i < mappers.Length-1; i++)
            {
                mappers[i].neighbours.Add(new SkrptrNeighbour(NeighbourDirection.Right, mappers[i + 1].gameObject));
                mappers[i].neighbours.Add(new SkrptrNeighbour(NeighbourDirection.Left, mappers[i - 1].gameObject));
            }
        }

        /// <summary>
        /// All children will be added a KeyboardMapper and mapped vertically. 
        /// </summary>
        [MenuItem("Skrptr/Neighbours/Neighbour Vertically ")]
        private static void SetNeighboursVertically()
        {
            //Fetch Elements
            Transform[] tfs = Selection.transforms.OrderBy(tf => tf.gameObject.name).ToArray();
            SkrptrKeyboardMapper[] mappers = new SkrptrKeyboardMapper[tfs.Length];
            for (int i = 0; i < tfs.Length; i++)
            {
                if (tfs[i].GetComponent<SkrptrKeyboardMapper>() == null)
                {
                    tfs[i].gameObject.AddComponent<SkrptrKeyboardMapper>();
                    mappers[i] = tfs[i].GetComponent<SkrptrKeyboardMapper>();
                }
                else
                {
                    mappers[i] = tfs[i].GetComponent<SkrptrKeyboardMapper>();
                }
                if (mappers[i].neighbours == null)
                    mappers[i].neighbours = new List<SkrptrNeighbour>();
            }
            
            //Mapping
            mappers[0].neighbours.Add(new SkrptrNeighbour(NeighbourDirection.Down, mappers[1].gameObject));
            mappers[mappers.Length - 1].neighbours.Add(new SkrptrNeighbour(NeighbourDirection.Up, mappers[mappers.Length - 2].gameObject));
            for (int i = 1; i < mappers.Length - 1; i++)
            {
                mappers[i].neighbours.Add(new SkrptrNeighbour(NeighbourDirection.Down, mappers[i + 1].gameObject));
                mappers[i].neighbours.Add(new SkrptrNeighbour(NeighbourDirection.Up, mappers[i - 1].gameObject));
            }
        }
    }
}
#endif