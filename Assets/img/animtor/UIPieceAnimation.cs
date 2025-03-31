using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIPieceAnimation : MonoBehaviour
{
    public Image imgPiece; // ���w�H�� UI
    public Transform targetUI; // �ؼ� UI ��m (�Ҧp�I�])

    private void Start()
    {
        PlayPieceAnimation();
    }

    public void PlayPieceAnimation()
    {
        // **1. ��l���A�G�z���B�Y�p**
        imgPiece.transform.localScale = Vector3.zero;
        imgPiece.color = new Color(1, 1, 1, 0);

        // **2. ���� + ���� + ��j**
        imgPiece.transform.DOLocalRotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360);
        imgPiece.DOFade(1f, 1f).From(0);
        imgPiece.transform.DOScale(1f, 1f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            // **3. �u���J��**
            imgPiece.transform.DOLocalMoveY(imgPiece.transform.localPosition.y + 50, 0.5f)
                .SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBounce);

            // **4. ��j + �o���{�{**
            imgPiece.transform.DOScale(1.2f, 0.3f).SetLoops(2, LoopType.Yoyo);

            // **5. �l�ܨ� UI �ؼ��I�]�Ҧp�I�]�^**
            if (targetUI != null)
            {
                imgPiece.transform.DOShakePosition(1f, 10f, 10, 90, false, true);
                imgPiece.transform.DOMove(targetUI.position, 1f).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    // **6. �̲׮���**
                    imgPiece.DOFade(0, 0.5f);
                    imgPiece.transform.DOScale(0, 0.5f);
                });
            }
        });
    }
}
