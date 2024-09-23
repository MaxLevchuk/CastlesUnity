using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuminousBlocks.Utils
{
    public static class Utils
    {

        public static int GetNextLevelNumber(string input)
        {
            string prefix = "Level";
            if (input.StartsWith(prefix))
            {
                if (int.TryParse(input.Substring(prefix.Length), out int levelNumber)) // delete word "Level" and string to int
                {
                    return levelNumber + 1;//next level
                }
            }

            return 0;
        }
        public static int GetActiveLevelNumber(string input)
        {
            string prefix = "Level";
            if (input.StartsWith(prefix))
            {
                if (int.TryParse(input.Substring(prefix.Length), out int levelNumber)) // delete word "Level" and string to int
                {
                    return levelNumber;
                }
            }

            return 0;
        }
    }
}
