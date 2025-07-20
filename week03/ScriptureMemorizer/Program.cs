// Extra requirement: includes a library of scriptures and randomly selects one at runtime

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Scripture Memorizer Project!\n");

        List<(string book, int chapter, int verseStart, int? verseEnd, string text)> scriptureLibrary =
            new List<(string, int, int, int?, string)>
            {
                ("John", 3, 16, null, "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life"),
                ("Proverbs", 3, 5, 6, "Trust in the Lord with all thine heart and lean not unto thine own understanding"),
                ("Psalm", 23, 1, null, "The Lord is my shepherd I shall not want"),
                ("Mosiah", 2, 17, null, "When ye are in the service of your fellow beings ye are only in the service of your God"),
                ("Matthew", 5, 16, null, "Let your light so shine before men that they may see your good works and glorify your Father which is in heaven")
            };

        Random rnd = new Random();
        var chosen = scriptureLibrary[rnd.Next(scriptureLibrary.Count)];

        Reference reference = new Reference(chosen.book, chosen.chapter, chosen.verseStart, chosen.verseEnd);
        Scripture scripture = new Scripture(reference, chosen.text);

        while (!scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.Trim().ToLower() == "quit")
                break;

            scripture.HideRandomWords(3); 
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nAll words are hidden or you quit. Program ended.");
    }
}

class Reference
{
    private string _book;
    private int _chapter;
    private int _verseStart;
    private int? _verseEnd;

    public Reference(string book, int chapter, int verseStart, int? verseEnd = null)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verseStart;
        _verseEnd = verseEnd;
    }

    public string GetDisplayText()
    {
        if (_verseEnd.HasValue)
            return $"{_book} {_chapter}:{_verseStart}-{_verseEnd}";
        else
            return $"{_book} {_chapter}:{_verseStart}";
    }
}

class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        if (_isHidden)
            return new string('_', _text.Length);
        else
            return _text;
    }
}

class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        foreach (string word in text.Split(' '))
        {
            _words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        Random rnd = new Random();

        for (int i = 0; i < numberToHide; i++)
        {
            int index = rnd.Next(_words.Count);
            _words[index].Hide();
        }
    }

    public string GetDisplayText()
    {
        string wordsText = string.Join(" ", _words.ConvertAll(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()} — {wordsText}";
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
                return false;
        }
        return true;
    }
}