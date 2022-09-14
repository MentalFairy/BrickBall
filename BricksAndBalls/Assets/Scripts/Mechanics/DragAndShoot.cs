using BricksAndBalls.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


namespace BricksAndBalls.Mechanics
{
    public class DragAndShoot : MonoBehaviour
    {
        [Header("Tweakable Properties")]
        public bool mayShoot = true;
        [SerializeField]
        float maxPower;
        [SerializeField]
        float shootPower;

        [SerializeField]
        public bool forwardDraging = true;
        [SerializeField]
        public bool showLineOnScreen = false;


        [SerializeField]
        Transform contentSpawnedBalls;

        [SerializeField]
        Transform direction;

        [SerializeField]
        GameObject ballPrefab;

        [SerializeField]
        private float delayBetweenBalls= .3f;

        [Header("Non-tweakable Properties")]
        [SerializeField]
        Rigidbody2D rb;
        [SerializeField]
        LineRenderer line;
        [SerializeField]
        LineRenderer screenLine;

        [Header("Debugging")]
        [SerializeField]
        Vector2 startPosition;
        [SerializeField]
        Vector2 targetPosition, startMousePos, currentMousePos;
    


        #region Unity Methods

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            line = GetComponent<LineRenderer>();
            screenLine = direction.GetComponent<LineRenderer>();
            Main.Instance.dragAndShooter = this;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DONT WANT TO IGNORE UI
                MouseClick();
            }
            if (Input.GetMouseButton(0))
            {
                // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DONT WANT TO IGNORE UI
                MouseDrag();
            }

            if (Input.GetMouseButtonUp(0))
            {
                // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DONT WANT TO IGNORE UI
                MouseRelease();
            }
        }
        #endregion

        #region Mouse Inputs
        void MouseClick()
        {

            if (mayShoot)
            {
                Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.right = dir * 1;

                startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        void MouseDrag()
        {
            if (mayShoot)
            {
                LookAtShootDirection();
                DrawLine();

                if (showLineOnScreen)
                    DrawScreenLine();

                float distance = Vector2.Distance(currentMousePos, startMousePos);

                if (distance > 1)
                {
                    line.enabled = true;

                    if (showLineOnScreen)
                        screenLine.enabled = true;
                }
            }
        }
        void MouseRelease()
        {
            if (mayShoot /*&& !EventSystem.current.IsPointerOverGameObject()*/)
            {
                StartCoroutine(Shoot());
                screenLine.enabled = false;
                line.enabled = false;
            }
        }
        #endregion


        // ACTIONS  
        void LookAtShootDirection()
        {
            Vector3 dir = startMousePos - currentMousePos;

            if (forwardDraging)
            {
                transform.right = dir * -1;
            }
            else
            {
                transform.right = dir;
            }

            float dis = Vector2.Distance(startMousePos, currentMousePos);
            dis *= 4;
            if (dis < maxPower)
            {
                direction.localPosition = new Vector2(dis / 6, 0);
                shootPower = dis;
            }
            else
            {
                shootPower = maxPower;
                direction.localPosition = new Vector2(maxPower / 6, 0);
            }

        }
        public IEnumerator Shoot()
        {
            mayShoot = false;
            int ballsToThrow = Main.Instance.gameStats.playerBallsCount;
            for (int i = 0; i < ballsToThrow; i++)
            {
                var ball = Instantiate(ballPrefab, contentSpawnedBalls).GetComponent<Ball>();
                ball.Throw(transform.right);
                yield return new WaitForSeconds(delayBetweenBalls);
            }
        }

        #region Line Drawing
        void DrawScreenLine()
        {
            screenLine.positionCount = 1;
            screenLine.SetPosition(0, startMousePos);

            screenLine.positionCount = 2;
            screenLine.SetPosition(1, currentMousePos);
        }

        void DrawLine()
        {
            startPosition = transform.position;

            line.positionCount = 1;
            line.SetPosition(0, startPosition);


            targetPosition = direction.transform.position;
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            line.positionCount = 2;
            line.SetPosition(1, targetPosition);
        }
        #endregion
    }
}