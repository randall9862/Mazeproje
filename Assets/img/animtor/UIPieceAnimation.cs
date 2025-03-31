using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIPieceAnimation : MonoBehaviour
{
    public Image imgPiece; // 指定碎片 UI
    public Transform targetUI; // 目標 UI 位置 (例如背包)

    private void Start()
    {
        PlayPieceAnimation();
    }

    public void PlayPieceAnimation()
    {
        // **1. 初始狀態：透明、縮小**
        imgPiece.transform.localScale = Vector3.zero;
        imgPiece.color = new Color(1, 1, 1, 0);

        // **2. 旋轉 + 漸顯 + 放大**
        imgPiece.transform.DOLocalRotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360);
        imgPiece.DOFade(1f, 1f).From(0);
        imgPiece.transform.DOScale(1f, 1f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            // **3. 彈跳入場**
            imgPiece.transform.DOLocalMoveY(imgPiece.transform.localPosition.y + 50, 0.5f)
                .SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBounce);

            // **4. 放大 + 發光閃爍**
            imgPiece.transform.DOScale(1.2f, 0.3f).SetLoops(2, LoopType.Yoyo);

            // **5. 追蹤到 UI 目標點（例如背包）**
            if (targetUI != null)
            {
                imgPiece.transform.DOShakePosition(1f, 10f, 10, 90, false, true);
                imgPiece.transform.DOMove(targetUI.position, 1f).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    // **6. 最終消失**
                    imgPiece.DOFade(0, 0.5f);
                    imgPiece.transform.DOScale(0, 0.5f);
                });
            }
        });
    }
}
