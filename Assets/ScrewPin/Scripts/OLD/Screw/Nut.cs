using DG.Tweening;
using UnityEngine;

public class Nut : MonoBehaviour
{
    private bool isMouseDown = false;
    private ScrewInfo screwInfo;
    private CollectScrew[] collectScrews;
    private HoldScrew hold;
    private Board board;

    private Camera mainCamera;

    private BoardManager boardManager;
    public int targetBoardID; // ID của board mục tiêu cho Nut này

    private void Start()
    {
        screwInfo = GetComponent<ScrewInfo>();
        board = GetComponent<Board>();
        hold = FindAnyObjectByType<HoldScrew>();

        boardManager = FindObjectOfType<BoardManager>();
        mainCamera = Camera.main;
    }
    private void Update()
    {
        collectScrews = FindObjectsOfType<CollectScrew>();

        /*hold.MoveScrewsToCollect(collectScrews);*/
    }

    public void MoveObj()
    {
        if (isMouseDown) return;
        isMouseDown = true;

        foreach (var collectScrew in collectScrews)
        {
            if ((int)collectScrew.id == (int)screwInfo.id)
            {
                Transform targetPosition = collectScrew.GetFirstPosition();
                if (targetPosition != null)
                {
                    Vector3 newPosition = targetPosition.position;

                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(transform.DOMove(new Vector3(0f, 2f, 5f), 0.5f))
                            .Join(transform.DORotate(new Vector3(-60f, 0f, 180f), 0.5f))
                            .Append(transform.DOMove(newPosition + new Vector3(0f, 0f, 1f), 0.5f))
                            .Append(transform.DORotate(Vector3.zero, 0.5f))
                            .Append(transform.DOMove(newPosition, 0.5f))
                            .OnComplete(() =>
                            {
                                transform.SetParent(collectScrew.transform);  // Set the parent to the corresponding CollectScrew object
                                collectScrew.AddScrew(transform);
                                
                                //collectScrew.PushObjectAside();
                                //isMouseDown = false;
                            });

                    collectScrew.RemoveFirstPosition();
                    

                    if (boardManager != null)
                    {
                        Board targetBoard = boardManager.GetBoardByID(targetBoardID);
                        targetBoard?.RemoveScrew(gameObject);
                        //board.RemoveScrew(gameObject);
                    }

                    break;
                }
            }
            if ((int)collectScrew.id != (int)screwInfo.id)
            {
                Transform holdPosition = hold.GetFirstPosition();
                if (holdPosition != null)
                {
                    Vector3 newPosition = holdPosition.position;

                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(transform.DOMove(new Vector3(0f, 2f, 5f), 0.5f))
                            .Join(transform.DORotate(new Vector3(-60f, 0f, 180f), 0.5f))
                            .Append(transform.DOMove(newPosition + new Vector3(0f, 0f, 1f), 0.5f))
                            .Append(transform.DORotate(Vector3.zero, 0.5f))
                            .Append(transform.DOMove(newPosition, 0.5f))
                            .OnComplete(() =>
                            {
                                hold.AddScrew(transform);
                                transform.SetParent(hold.transform);  // Set the parent to the HoldScrew object


                                //isMouseDown = false;
                            });

                    hold.RemoveFirstPosition();

                    if (boardManager != null)
                    {
                        Board targetBoard = boardManager.GetBoardByID(targetBoardID);
                        targetBoard?.RemoveScrew(gameObject);
                        
                        //board.RemoveScrew(gameObject);
                    }
                    break;
                }
            }

            // Kiểm tra và di chuyển các screws từ HoldScrew đến CollectScrew
            //hold.MoveScrewsToCollect(collectScrews);
        }

    }


}
