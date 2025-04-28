using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player;
    public List<PathCondition> pathConditions = new List<PathCondition>();
    public List<Transform> pivots;

    public Transform[] objectsToHide;

    public int Relics ;
    public ButtonHandler buttonHandler;
    public GameObject prefab;         
    public JellyBounce[] jellyBounces;
    public Walkable[] jellywalkble;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Relics = 0;
    }
    void Update()
    {
        foreach (PathCondition pc in pathConditions)
        {
             if (pc.checkWalkableTag && pc.walkableToCheck != null)
        {           
            if (pc.walkableToCheck.blueShard || pc.walkableToCheck.greenShard || pc.walkableToCheck.redShard)
            {
                continue; // 如果標籤是開啟的，跳過這個 PathCondition
            }
                else
                {
                    // 如果三個碎片都沒開啟，就各自獨立檢查
                    if (!pc.walkableToCheck.blueShard)
                    {
                        jellyBounces[0].Shakeanddrop();
                        jellyBounces[1].Shakeanddrop();
                    }
                                   
                }
            }            

            int count = 0;
            for (int i = 0; i < pc.conditions.Count; i++)//確認高度的選項和旋轉
            {
                // �ھ� checkHeight �ܼƨӨM�w�O�_�ˬd����
                if (pc.checkHeight)
                {
                    if (pc.conditions[i].conditionObject.position.y == pc.conditions[i].eulerAngle.y)
                    {
                        count++;
                    }
                }
                else
                {
                    // �p�G���ˬd���סA�o�̥i�H�K�[��L�ˬd�޿�
                    // �Ҧp�G�ˬd��L����
                }
            }
            foreach (SinglePath sp in pc.paths)
                sp.block.possiblePaths[sp.index].active = (count == pc.conditions.Count);
        }
        if (!jellywalkble[0].greenShard)
        {
            jellyBounces[2].Shakeanddrop();
        }
        if (!jellywalkble[1].redShard)
        {
            jellyBounces[3].Shakeanddrop();
        }
        if (player.walking)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int multiplier = Input.GetKey(KeyCode.RightArrow) ? 1 : -1;
            pivots[0].DOComplete();
            pivots[0].DORotate(new Vector3(0, 90 * multiplier, 0), .6f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        }

        foreach (Transform t in objectsToHide)
        {
            t.gameObject.SetActive(pivots[0].eulerAngles.y > 45 && pivots[0].eulerAngles.y < 90 + 45);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
        if (Relics==3)
        {
            buttonHandler.SetButtonState(true);
            prefab.SetActive(true);
            Relics = 0;
        }
    }

    public void RotateRightPivot()
    {
        pivots[1].DOComplete();
        pivots[1].DORotate(new Vector3(0, 0, 90), .6f).SetEase(Ease.OutBack);
    }
}

[System.Serializable]
public class PathCondition
{ public string pathConditionName;
    public List<Condition> conditions;
    public List<SinglePath> paths;
    public bool checkHeight; // �s�W�����L�ܼ�
    [Header("Booleans")]
    public bool checkWalkableTag; // 新增的布爾變量，用於指示是否需要檢查地板標籤
    public Walkable walkableToCheck; // 需要檢查的地板
}

[System.Serializable]
public class Condition
{
    public Transform conditionObject;
    public Vector3 eulerAngle;
}

[System.Serializable]
public class SinglePath
{
    public Walkable block;
    public int index;
}