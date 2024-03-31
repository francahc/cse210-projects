
using System.Text.RegularExpressions;




 
public class ScriptureRef
{
    public int Chap { get; private set; }
    public int FVerse { get; private set; }
    public int EVerse { get; private set; }

    public ScriptureRef(int chap, int verse)
    {
        Chap = chap;
        FVerse = verse;
        EVerse = verse;
    }

    public ScriptureRef(int chapter, int fVerse , int eVerse)
    {
        Chap = chapter;
        FVerse  = fVerse;
        EVerse = eVerse;
    }

    public override string ToString()
    {
        if (FVerse == EVerse)
            return $"{Chap}:{FVerse}";
        else
            return $"{Chap}:{FVerse}-{EVerse}";
    }
}
public class Word
{
    public string Text { get; private set; }

    public Word(string text)
    {
        Text = text;
    }

    public void Hide()
    {
        Text = new string('_', Text.Length);
    }

    public bool IsHidden()
    {
        return Text.All(char.IsPunctuation) || Text.All(char.IsWhiteSpace);
    }

    public override string ToString()
    {
        return Text;
    }
}



public class Scripture
{
    public ScriptureRef Reference { get; private set; }
    private List<Word> Words { get; set; }

    public Scripture(ScriptureRef reference, string text)
    {
        Reference = reference;
        Words = new List<Word>();
        string[] wordArray = Regex.Split(text, @"(\s+|\W+)");
        foreach (string wordText in wordArray)
        {
            if (!string.IsNullOrWhiteSpace(wordText))
                Words.Add(new Word(wordText));
        }
    }

    public void HideRandomWords(int numToHide)
    {
        var visibleWords = Words.Where(word => !word.IsHidden()).ToList();
        var indicesToHide = Enumerable.Range(0, visibleWords.Count).OrderBy(x => Guid.NewGuid()).Take(numToHide);
        foreach (var index in indicesToHide)
        {
            visibleWords[index].Hide();
        }
    }

    public bool AllWordsHidden()
    {
        return Words.All(word => word.IsHidden());
    }

    public override string ToString()
    {
        string scriptureText = string.Join(" ", Words);
        return $"{Reference}: {scriptureText}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        ScriptureRef ref1 = new ScriptureRef(3, 5);
        ScriptureRef ref2 = new ScriptureRef(3, 5, 6);

        Scripture script1 = new Scripture(ref1, "Proverbs. Trust in the Lord with all thine heart; and lean not unto thine own understanding.");
        Scripture script2 = new Scripture(ref2, "Proverbs.  Trust in the Lord with all thine heart; and lean not unto thine own understanding. 6. In all thy ways acknowledge him, and he shall direct thy paths.");

        DisplayScripture(script1);
        DisplayScripture(script2);

        


        Console.WriteLine("\nPress Enter to reveal or type 'quit' to exit: ");

        while (true)
        {
            if (Console.ReadLine().ToLower() == "quit")
                break;

            script1.HideRandomWords(3);
            DisplayScripture(script1);
            if (script1.AllWordsHidden())
                break;
        }

    }

    static void DisplayScripture(Scripture scripture)
    {
        Console.Clear();
        Console.WriteLine(scripture);
    }
}