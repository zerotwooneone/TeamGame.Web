namespace TeamGame.Domain.Board;

public sealed class Board : IDisposable
{
    public readonly int RowCount;
    public readonly int ColumnCount;
    public readonly IReadOnlyList<IReadOnlyList<Space>> Rows;
    public readonly int SpaceSize;

    private Board(
        int rowCount, 
        int columnCount, 
        IReadOnlyList<IReadOnlyList<Space>> rows, 
        int spaceSize)
    {
        RowCount = rowCount;
        ColumnCount = columnCount;
        Rows = rows;
        SpaceSize = spaceSize;
    }

    public static Board Create(
        int rowCount,
        int columnCount,
        IEnumerable<IEnumerable<Space>> rows,
        int spaceSize)
    {
        if (rowCount < 1)
        {
            throw new ArgumentException($"invalid row count {rowCount}", nameof(rowCount));
        }
        if (columnCount < 1)
        {
            throw new ArgumentException($"invalid column count {columnCount}", nameof(columnCount));
        }

        var rowList = rows.Select<IEnumerable<Space>,List<Space>>(r=>r.ToList()).ToList();
        if (rowList.Count < 1)
        {
            throw new ArgumentException($"invalid number of rows", nameof(rows));
        }

        if (rowList.Any(r => r.Count < 1))
        {
            throw new ArgumentException($"invalid number of columns", nameof(rows));
        }

        if (spaceSize < 1)
        {
            throw new ArgumentException($"invalid space size {spaceSize}", nameof(spaceSize));
        }

        return new Board(
            rowCount,
            columnCount,
            rowList,
            spaceSize);
    }

    public void Dispose()
    {
        foreach (var row in Rows)
        {
            foreach (var space in row)
            {
                space.Dispose();
            }
        }
    }
}