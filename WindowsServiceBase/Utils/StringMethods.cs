using System;
using System.Collections;
using System.Collections.Generic;

namespace Services.Utils
{
    public static class StringMethods
    {
        public static string RemoveEscape(string value)
        {
            int insertCount = 0;

            try
            {
                var indexes = StringMethods.FindAllOccurrences('\a', value);
                if (indexes != null && indexes.Length > 0)
                {
                    foreach (var index in indexes)
                        value = value.Insert(index + insertCount++ + 1, "a");

                    value = value.Replace('\a', '\\');
                }
            }
            catch { }
            finally
            {
                insertCount = 0;
            }

            try
            {
                var indexes = StringMethods.FindAllOccurrences('\b', value);
                if (indexes != null && indexes.Length > 0)
                {
                    foreach (var index in indexes)
                        value = value.Insert(index + insertCount++ + 1, "b");

                    value = value.Replace('\b', '\\');
                }
            }
            catch { }
            finally
            {
                insertCount = 0;
            }

            try
            {
                var indexes = StringMethods.FindAllOccurrences('\f', value);
                if (indexes != null && indexes.Length > 0)
                {
                    foreach (var index in indexes)
                        value = value.Insert(index + insertCount++ + 1, "f");

                    value = value.Replace('\f', '\\');
                }
            }
            catch { }
            finally
            {
                insertCount = 0;
            }

            try
            {
                var indexes = StringMethods.FindAllOccurrences('\n', value);
                if (indexes != null && indexes.Length > 0)
                {
                    foreach (var index in indexes)
                        value = value.Insert(index + insertCount++ + 1, "n");

                    value = value.Replace('\n', '\\');
                }
            }
            catch { }
            finally
            {
                insertCount = 0;
            }

            try
            {
                var indexes = StringMethods.FindAllOccurrences('\r', value);
                if (indexes != null && indexes.Length > 0)
                {
                    foreach (var index in indexes)
                        value = value.Insert(index + insertCount++ + 1, "r");

                    value = value.Replace('\r', '\\');
                }
            }
            catch { }
            finally
            {
                insertCount = 0;
            }

            try
            {
                var indexes = StringMethods.FindAllOccurrences('\t', value);
                if (indexes != null && indexes.Length > 0)
                {
                    foreach (var index in indexes)
                        value = value.Insert(index + insertCount++, "t");

                    value = value.Replace('\t', '\\');
                }
            }
            catch { }
            finally
            {
                insertCount = 0;
            }

            try
            {
                var indexes = StringMethods.FindAllOccurrences('\v', value);
                if (indexes != null && indexes.Length > 0)
                {
                    foreach (var index in indexes)
                        value = value.Insert(index + insertCount++, "v");

                    value = value.Replace('\v', '\\');
                }
            }
            catch { }
            finally
            {
                insertCount = 0;
            }

            value = value.Replace("\\", "\\\\");

            return value;

        }
        public static IEnumerable<int> AllIndexesOf(this string str, string searchstring)
        {
            int minIndex = str.IndexOf(searchstring);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
            }
        }

        public static int[] FindAllOccurrences(char matchChar, string source)
        {
            return (FindAllOccurrences(matchChar, source, -1, false));
        }

        public static int[] FindAllOccurrences(char matchChar, string source,
                                                   int maxMatches)
        {
            return (FindAllOccurrences(matchChar, source, maxMatches, false));
        }

        public static int[] FindAllOccurrences(char matchChar, string source,
                                                   bool caseSensitivity)
        {
            return (FindAllOccurrences(matchChar, source, -1, caseSensitivity));
        }

        public static int[] FindAllOccurrences(char matchChar, string source,
                                                   int maxMatches, bool caseSensitivity)
        {
            ArrayList occurrences = new ArrayList();
            int foundPos = -1;   // -1 represents not found
            int numberFound = 0;
            int startPos = 0;
            char tempMatchChar = matchChar;
            string tempSource = source;

            if (!caseSensitivity)
            {
                tempMatchChar = char.ToUpper(matchChar);
                tempSource = source.ToUpper();
            }

            do
            {
                foundPos = tempSource.IndexOf(matchChar, startPos);
                if (foundPos > -1)
                {
                    startPos = foundPos + 1;
                    numberFound++;

                    if (maxMatches > -1 && numberFound > maxMatches)
                    {
                        break;
                    }
                    else
                    {
                        occurrences.Add(foundPos);
                    }
                }
            } while (foundPos > -1);

            return ((int[])occurrences.ToArray(typeof(int)));
        }
    }

}
