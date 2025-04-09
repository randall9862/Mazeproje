using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    public bool walking = false;

    public bool canMove = true;

    [Space]
    public Transform currentCube;
    public Transform clickedCube;
    //public Transform indicator;

    [Space]
    public List<Transform> finalPath = new List<Transform>();

    private float blend;
    private Animator animator;

    [SerializeField] private GameObject[] managedObjects;
    private ObjectSwitcher objectSwitcher;

    public FloatingObject floatingObject;
    public RelicMagnification relicMagnification;
    public FragmentMerge fragmentMerge;

    public GameObject Endimg;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        RayCastDown();
        objectSwitcher = new ObjectSwitcher(managedObjects);
    }

    void Update()
    {
        // GET CURRENT CUBE (UNDER PLAYER)
        RayCastDown();

        if (currentCube != null && currentCube.GetComponent<Walkable>().movingGround)
        {
            transform.parent = currentCube.parent;
        }
        else
        {
            transform.parent = null;
        }
       // 增加檢查 canMove
        if (!canMove) return;
        // CLICK ON CUBE
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                if (mouseHit.transform.GetComponent<Walkable>() != null)
                {
                    clickedCube = mouseHit.transform;
                    DOTween.Kill(gameObject.transform);
                    finalPath.Clear();
                    FindPath();
                    blend = transform.position.y - clickedCube.position.y > 0 ? -1 : 1;
                   /* indicator.position = mouseHit.transform.GetComponent<Walkable>().GetWalkPoint();
                    Sequence s = DOTween.Sequence();
                    s.AppendCallback(() => indicator.GetComponentInChildren<ParticleSystem>().Play());
                    s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.white, .1f));
                    s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.black, .3f).SetDelay(.2f));
                    s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.clear, .3f));*/
                }
            }
        }
    }

    void FindPath()
    {
        List<Transform> nextCubes = new List<Transform>();
        List<Transform> pastCubes = new List<Transform>();

        foreach (WalkPath path in currentCube.GetComponent<Walkable>().possiblePaths)
        {
            if (path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = currentCube;
            }
        }

        pastCubes.Add(currentCube);
        ExploreCube(nextCubes, pastCubes);
        BuildPath();
    }

    void ExploreCube(List<Transform> nextCubes, List<Transform> visitedCubes)
    {
        if (nextCubes.Count == 0)
            return;

        Transform current = nextCubes.First();
        nextCubes.Remove(current);

        if (current == clickedCube)
        {
            return;
        }

        foreach (WalkPath path in current.GetComponent<Walkable>().possiblePaths)
        {
            if (!visitedCubes.Contains(path.target) && path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = current;
            }
        }

        visitedCubes.Add(current);
        ExploreCube(nextCubes, visitedCubes);
    }

    void BuildPath()
    {
        Transform cube = clickedCube;
        while (cube != currentCube)
        {
            finalPath.Add(cube);
            if (cube.GetComponent<Walkable>().previousBlock != null)
                cube = cube.GetComponent<Walkable>().previousBlock;
            else
                return;
        }

        finalPath.Insert(0, clickedCube);
        FollowPath();
    }

    void FollowPath()
    {
        Sequence s = DOTween.Sequence();
        walking = true;
        animator.SetBool("isWalking", true);

        for (int i = finalPath.Count - 1; i > 0; i--)
        {
            float time = finalPath[i].GetComponent<Walkable>().isStair ? 1.5f : 1;
            s.Append(transform.DOMove(finalPath[i].GetComponent<Walkable>().GetWalkPoint(), .2f * time).SetEase(Ease.Linear));
            if (!finalPath[i].GetComponent<Walkable>().dontRotate)
                s.Join(transform.DOLookAt(finalPath[i].position, .1f, AxisConstraint.Y, Vector3.up));
        }

        if (clickedCube.GetComponent<Walkable>().isButton)
        {
            s.AppendCallback(() => GameManager.instance.RotateRightPivot());
        }

        if (clickedCube.GetComponent<Walkable>().blueShard)
        {
            s.AppendCallback(() => {
                clickedCube.GetComponent<Walkable>().blueShard = false;
                GameManager.instance.Relics++;
                objectSwitcher.TurnOnAt(0);
                relicMagnification.SwitchMagnification();
                fragmentMerge.StartEnlargeEffect(0);
                floatingObject.DisableRelicsbiue();
                floatingObject.SetObjectMaterial(0, 0);
            });
        }
        if (clickedCube.GetComponent<Walkable>().greenShard)
        {
            s.AppendCallback(() => {
                clickedCube.GetComponent<Walkable>().greenShard = false;
                GameManager.instance.Relics++;
                objectSwitcher.TurnOnAt(1);
                relicMagnification.SwitchMagnification();
                fragmentMerge.StartEnlargeEffect(1);
                floatingObject.DisableRelicsgreen();
                floatingObject.SetObjectMaterial(1, 1);
            });
        }
        if (clickedCube.GetComponent<Walkable>().redShard)
        {
            s.AppendCallback(() => {
                clickedCube.GetComponent<Walkable>().redShard = false;
                GameManager.instance.Relics++;
                objectSwitcher.TurnOnAt(2);
                relicMagnification.SwitchMagnification();
                fragmentMerge.StartEnlargeEffect(2);
                floatingObject.DisableRelicsred();
                floatingObject.SetObjectMaterial(2, 2);
            });
        }
        if (clickedCube.GetComponent<Walkable>().end)
        {
            s.AppendCallback(() => {
                clickedCube.GetComponent<Walkable>().end = false;
                Endimg.SetActive(true);
            });
        }
            s.AppendCallback(() => Clear());
    }

    void Clear()
    {
        foreach (Transform t in finalPath)
        {
            t.GetComponent<Walkable>().previousBlock = null;
        }
        finalPath.Clear();
        walking = false;
        animator.SetBool("isWalking", false);
    }

    public void RayCastDown()
    {
        if (transform.childCount > 0)
        {
            Ray playerRay = new Ray(transform.GetChild(0).position, -transform.up);
            RaycastHit playerHit;

            if (Physics.Raycast(playerRay, out playerHit))
            {
                if (playerHit.transform.GetComponent<Walkable>() != null)
                {
                    currentCube = playerHit.transform;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (transform.childCount > 0)
        {
            Ray ray = new Ray(transform.GetChild(0).position, -transform.up);
            Gizmos.DrawRay(ray);
        }
    }
 // 新增公開方法來控制玩家移動
    public void SetPlayerMovement(bool enabled)
    {
        canMove = enabled;
        // 如果禁用移動時玩家正在走路，強制停止當前移動
        if (!enabled && walking)
        {
            DOTween.Kill(gameObject.transform);
            Clear();
        }
    }
    void SetBlend(float x)
    {
        animator.SetFloat("Blend", x);
    }
}
