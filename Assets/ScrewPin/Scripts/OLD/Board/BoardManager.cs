using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public List<Board> boards = new List<Board>();

    void Start()
    {
        SetIDBoard();
    }

    public void SetIDBoard()
    {
        for (int i = 0; i < boards.Count; i++)
        {
            boards[i].boardID = i; 
        }
    }

    public Board GetBoardByID(int id)
    {
        return boards.Find(board => board.boardID == id);
    }
}
