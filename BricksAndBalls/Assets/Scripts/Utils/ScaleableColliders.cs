using BricksAndBalls.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Utils
{
    [ExecuteInEditMode]
    public class ScaleableColliders : MonoBehaviour
    {
        [Header("Tweakable values")]
        [SerializeField]
        float collidersThickness = 1f;
        /// <summary>
        /// Represents the z offset of colliders in world space
        /// </summary>
        [SerializeField]
        float zOffsetForCollider = 0f;

        [Header("Non-tweakable Properties")]
        Dictionary<string, Transform> colliders = new Dictionary<string, Transform>();

        [SerializeField]
        Vector2 lastScreenResolution;

        [Header("Debugging")]
        [SerializeField]
        private Vector2 screenSize;

        void Awake()
        {
            lastScreenResolution = Vector2.zero;
            Main.Instance.scaleableColliders = this;
        }

        private void Update()
        {
            if(lastScreenResolution.x != Screen.width ||
               lastScreenResolution.y != Screen.height)
            {
                lastScreenResolution = new Vector2(Screen.width, Screen.height);
                StartCoroutine(BuildColliders());
            }
        }
        private void ClearColliders()
        {
            for (int i = transform.childCount-1; i >= 0; i--)
            {
                if (Application.isPlaying)
                    Destroy(transform.GetChild(i).gameObject);
                else
                    DestroyImmediate(transform.GetChild(i).gameObject);
            }
            colliders.Clear();
        }
        private IEnumerator BuildColliders()
        {
            Debug.Log($"[ScalableColliders] Rebuilding colliders...");
            //Clear
            ClearColliders();
            yield return null;

            //Generate colliders
            colliders.Add("Top", new GameObject().transform);
            colliders.Add("Bottom", new GameObject().transform);
            colliders.Add("Right", new GameObject().transform);
            colliders.Add("Left", new GameObject().transform);

            //Grab the world-space position values of the start and end positions of the screen, then calculate the distance between them and store it as half,
            //since we only need half that value for distance away from the camera to the edge
            Vector3 cameraPos = Camera.main.transform.position;
            screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f; 
            screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

            foreach (var valPair in colliders)
            {
                //Setup
                valPair.Value.gameObject.AddComponent<BoxCollider2D>(); 
                valPair.Value.name = valPair.Key + "Collider"; 
                valPair.Value.parent = transform;

                //Scale
                if (valPair.Key == "Left" || valPair.Key == "Right") 
                    valPair.Value.localScale = new Vector3(collidersThickness, screenSize.y * 2, collidersThickness);
                else
                    valPair.Value.localScale = new Vector3(screenSize.x * 2, collidersThickness, collidersThickness);
            }

            //Change positions to align perfectly with outter-edge of screen, adding the world-space values of the screen we generated earlier,
            //and adding/subtracting them with the current camera position, as well as add/subtracting half out objects size so it's not just half way off-screen
            colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["Right"].localScale.x * 0.5f), cameraPos.y, zOffsetForCollider);
            colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["Left"].localScale.x * 0.5f), cameraPos.y, zOffsetForCollider);
            colliders["Top"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (colliders["Top"].localScale.y * 0.5f), zOffsetForCollider);
            colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["Bottom"].localScale.y * 0.5f), zOffsetForCollider);

            colliders["Bottom"].gameObject.AddComponent<DestroyBallOnImpact>();
        }
    }
}