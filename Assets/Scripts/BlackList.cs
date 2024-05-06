using System.Collections.Generic;
using System.Linq;


namespace BlackListOfWords
{
    class BlackList
    {
        private HashSet<string> wordList;

        public BlackList(string filename)
        {
            wordList = System.IO.File.ReadAllLines(filename).ToHashSet<string>();
        }

        public bool Valid(string text)
        {
            string[] words = text.Split(' ');

            foreach (string word in words)
            {
                if (!ValidWord(word))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidWord(string word)
        {
            return !wordList.Contains(word.ToUpper());
        }
    };
}
