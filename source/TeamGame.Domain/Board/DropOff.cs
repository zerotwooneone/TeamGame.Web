namespace TeamGame.Domain.Board;

public class DropOff
{
    public readonly DropOffColor Color;
    public readonly char Letter;

    private DropOff(
        DropOffColor color, 
        char letter)
    {
        Color = color;
        Letter = letter;
    }

    public static DropOff Create(DropOffColor color, char letter)
    {
        if (!Enum.IsDefined(typeof(DropOffColor), color))
        {
            throw new ArgumentException($"invalid dropoff color {color}", nameof(color));
        }

        var capLetter = letter.ToString().ToUpper().First();
        if (letter < 'A' || letter > 'Z')
        {
            throw new ArgumentException($"invalid dropoff letter {capLetter}", nameof(letter));
        }

        return new DropOff(color, capLetter);
    }
}