using BricksAndBalls.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Mechanics
{

    public class BrickSpawner : MonoBehaviour
    {
        [Header("Tweakable Properties For Bricks")]
        [SerializeField]
        GameObject brickPrefab;

        [SerializeField]
        int bricksPerLayer = 5;

        [SerializeField]
        Color[] brickColors;

        [SerializeField]
        float ySpacing = -1.1f;

        [Header("Tweakable Properties For Power Ups")]
        [SerializeField]
        GameObject[] powerUpPrefabs;
        [SerializeField]
        [Range(0f, 1f)]
        float powerUpProbability = .1f;

        [Header("Non-Tweakable Properties")]
        [SerializeField]
        List<Transform> brickLayers = new List<Transform>();

        [Header("Debugging")]
        [SerializeField]
        private Vector2 screenSize;

        private void Awake()
        {
            Main.Instance.brickSpawner = this;
        }

        internal void SpawnLayer()
        {
            Vector3 cameraPos = Camera.main.transform.position;
            screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
            screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.position = new Vector3(child.position.x, child.position.y + ySpacing);
            }

            var brickLayerContent = new GameObject();
            brickLayerContent.transform.parent = transform;
            brickLayerContent.name = "Layer" + transform.childCount;

            var xDistance = screenSize.x * 2;
            var incrementBetweenBricks = xDistance / bricksPerLayer;
            var randomColor = brickColors[Random.Range(0, brickColors.Length - 1)];
            for (int i = 0; i < bricksPerLayer; i++)
            {
                //Spawn powerup
                if (Random.Range(0f, 1f) < powerUpProbability)
                {
                    SpawnPowerUp(brickLayerContent, incrementBetweenBricks, i);
                }
                else
                {
                    SpawnBrick(brickLayerContent, incrementBetweenBricks, randomColor, i);
                }
            }
            brickLayers.Add(brickLayerContent.transform);
        }

        //Spawns a power up
        private void SpawnPowerUp(GameObject brickLayerContent, float incrementBetweenBricks, int index)
        {
            var powerUp = Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length - 1)], brickLayerContent.transform);
            powerUp.transform.position = new Vector3(-screenSize.x + incrementBetweenBricks * (index + .5f),
                                                   screenSize.y - 1 + powerUp.transform.localScale.y / 3f);
        }

        /// <summary>
        /// Spawns a brick
        /// </summary>
        /// <param name="parentGo">To whom the GO will be parented</param>
        /// <param name="incrementBetweenBricks"> space to be left between</param>
        /// <param name="randomColor"> Color of the brick</param>
        /// <param name="index"> Index of the brick on respsective layer</param>
        private void SpawnBrick(GameObject parentGo, float incrementBetweenBricks, Color randomColor, int index)
        {
            var brick = Instantiate(brickPrefab, parentGo.transform);
            //Set their positions
            brick.transform.position = new Vector3(-screenSize.x + incrementBetweenBricks * (index + .5f),
                                                   screenSize.y - 1 + brick.transform.localScale.y / 3f);
            //Scale them to fit
            brick.transform.localScale = new Vector3(incrementBetweenBricks * .95f, 1);

            brick.GetComponent<Brick>().Init(transform.childCount, randomColor);
        }
    }
}
