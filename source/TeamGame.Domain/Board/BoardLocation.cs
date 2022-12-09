namespace TeamGame.Domain.Board;

public struct BoardLocation
{
    public readonly int RowIndex;
    public readonly int ColumnIndex;

    private BoardLocation(
        int rowIndex, 
        int columnIndex)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
    }

    public static BoardLocation Create(
        int rowIndex,
        int columnIndex)
    {
        return new BoardLocation(rowIndex, columnIndex);
    }
}