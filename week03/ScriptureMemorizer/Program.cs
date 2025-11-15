using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // -------------------------------
    // Reference Class
    // -------------------------------
    public class Reference
    {
        private string _book;
        private int _chapter;
        private int _startVerse;
        private int? _endVerse; // optional for multiple verses

        public Reference(string book, int chapter, int verse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = verse;
        }

        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        public override string ToString()
        {
            return _endVerse == null
                ? $"{_book} {_chapter}:{_startVerse}"
                : $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }

    // -------------------------------
    // Word Class
    // -------------------------------
    public class Word
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
            return _isHidden ? new string('_', _text.Length) : _text;
        }
    }

    // -------------------------------
    // Scripture Class
    // -------------------------------
    public class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text.Split(' ')
                         .Select(word => new Word(word))
                         .ToList();
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine(_reference.ToString());
            Console.WriteLine(string.Join(" ", _words.Select(w => w.GetDisplayText())));
        }

        public bool HideRandomWords(int count = 3)
        {
            Random rand = new Random();
            var visibleWords = _words.Where(w => !w.IsHidden()).ToList();

            if (!visibleWords.Any()) return false;

            for (int i = 0; i < count && visibleWords.Any(); i++)
            {
                int index = rand.Next(visibleWords.Count);
                visibleWords[index].Hide();
                visibleWords.RemoveAt(index);
            }

            return _words.Any(w => !w.IsHidden());
        }

        public bool AllHidden()
        {
            return _words.All(w => w.IsHidden());
        }
    }

    // -------------------------------
    // Program Class
    // -------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            // Example scripture (creativity: multiple verses supported)
            Reference reference = new Reference("John", 3, 16);
            string text = "For God so loved the world that he gave his one and only Son " +
                          "that whoever believes in him shall not perish but have eternal life.";

            Scripture scripture = new Scripture(reference, text);

            // Main loop
            while (true)
            {
                scripture.Display();
                Console.WriteLine("\nPress ENTER to hide more words or type 'quit' to exit.");
                string input = Console.ReadLine();

                if (input?.ToLower() == "quit")
                    break;

                if (!scripture.HideRandomWords())
                {
                    Console.WriteLine("\nAll words are hidden. Program will now terminate.");
                    break;
                }
            }

            Console.WriteLine("\nThank you for using ScriptureMemorizer!");
        }
    }
}