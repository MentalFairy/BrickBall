using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{

    public class BrickSpawner : MonoBehaviour
    {
        [Header("Tweakable Properties")]
        [SerializeField]
        GameObject brickPrefab;

        [SerializeField]
        int bricksPerLayer = 5;

        [SerializeField]
        Color[] brickColors;

        [Header("Non-Tweakable Properties")]
        [SerializeField]
        List<Transform> brickLayers = new List<Transform>();

        [Header("Debugging")]
        [SerializeField]
        private Vector2 screenSize;

        // Start is called before the first frame update
        void Start()
        {
            Vector3 cameraPos = Camera.main.transform.position;
            screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
            screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
            SpawnLayer();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnLayer()
        {
            var brickLayerContent = new GameObject();
            brickLayerContent.transform.parent = transform;
            brickLayerContent.name = "Layer" + transform.childCount;

            var xDistance = screenSize.x * 2;
            var incrementBetweenBricks = xDistance / bricksPerLayer;
            var randomColor = brickColors[Random.Range(0, brickColors.Length - 1)];
            for (int i = 0; i < bricksPerLayer; i++)
            {
                var brick = Instantiate(brickPrefab, brickLayerContent.transform).GetComponent<Brick>();
                //Set their positions
                brick.transform.position = new Vector3(-screenSize.x + incrementBetweenBricks * (i+.5f),
                                                       screenSize.y - 1 + brick.transform.localScale.y / 3f);
                //Scale them to fit
                brick.transform.localScale = new Vector3(incrementBetweenBricks * .95f, 1);

                brick.Init(transform.childCount, randomColor);
            }

            brickLayers.Add(brickLayerContent.transform);
        }
    }
}
