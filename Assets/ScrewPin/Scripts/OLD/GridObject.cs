public class GridObject<T>
{
    GridSystem2D<GridObject<T>> grid;
    int width;
    int height;
    T blockTile;

    public GridObject(GridSystem2D<GridObject<T>> grid, int width, int height)
    {
        this.grid = grid;
        this.width = width;
        this.height = height;
    }

    public void SetValue(T blockTile)
    {
        this.blockTile = blockTile;
    }

    public T GetValue() => this.blockTile;
}
