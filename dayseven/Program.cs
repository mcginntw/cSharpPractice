using System;

namespace LPS
{

    class LPS
    {

        public static string PalCheck(string s, int len = 0)
        {
            char firstPos;
            char lastPos;
            string palVerification;
            string palVerificationII;
            len++;
            // Console.WriteLine($"{s}, {len}");
            // string subString;
            for (int i = 0; i < s.Length / 2; i++)
            {
                firstPos = s[i];
                lastPos = s[s.Length - 1 - i];
                if (firstPos != lastPos)
                {
                    palVerification = PalCheck(s[1..], len);
                    if (palVerification.Length == s.Length - 1)
                    {
                        return palVerification;
                    }
                    palVerificationII = PalCheck(s[..(s.Length - 1)], len);
                    if (palVerificationII.Length > palVerification.Length)
                    {
                        return palVerificationII;
                    }
                    return palVerification;
                }
            }
            if (s.Length == 1)
            {
                return "";
            }
            // Console.WriteLine(s);
            return s;
        }

        public static string PalForCheck(string s, int len = 2)
        {
            char firstPos;
            char lastPos;
            string palVerification = "";
            bool broke = false;
            // Thread.Sleep(500);
            for (int start = 0; start < s.Length - 1; start++)
            {
                for (int end = s.Length - 1 - start; end > start; end--)
                {
                    broke = false;
                    for (int i = 0; i < s[start..end].Length / 2; i++)
                    {
                        firstPos = s[i + start];
                        lastPos = s[end - i - 1];
                        if (firstPos != lastPos)
                        {
                            broke = true;
                            break;
                        }
                    }
                    if ((!broke) && (palVerification.Length < s[start..end].Length))
                    {
                        palVerification = s[start..end];
                    }
                }
            }
            return palVerification;
        }

            public static void printSubStr(String str, int low, int high)
            {
                for (int i = low; i <= high; ++i)
                    Console.Write(str[i]);
            }

            // This function prints the
            // longest palindrome subString
            // It also returns the length
            // of the longest palindrome
            static int longestPalSubstr(String str)
            {
                // get length of input String
                int n = str.Length;

                // All subStrings of length 1
                // are palindromes
                int maxLength = 1, start = 0;

                // Nested loop to mark start and end index
                for (int i = 0; i < str.Length; i++)
                {
                    for (int j = i; j < str.Length; j++)
                    {
                        int flag = 1;

                        // Check palindrome
                        for (int k = 0; k < (j - i + 1) / 2; k++)
                            if (str[i + k] != str[j - k])
                                flag = 0;

                        // Palindrome
                        if (flag != 0 && (j - i + 1) > maxLength)
                        {
                            start = i;
                            maxLength = j - i + 1;
                        }
                        Console.WriteLine(str[i..j]);
                    }
                }

                Console.Write("longest palindrome subString is: ");
                printSubStr(str, start, start + maxLength - 1);

                // return length of LPS
                return maxLength;
            }

            // Driver Code
            //     public static void Main(String[] args)
            //     {
            //         String str = "forgeeksskeegfor";
            //         Console.Write("\nLength is: "
            //             + longestPalSubstr(str));
            //     }
            // }

            // This code is contributed by shikhasingrajput
            public static void Main()
            {
                string s = "banana";
                // Console.WriteLine(s[..(s.Length - 1)]);
                // if (PalCheck(s)) {
                //         Console.WriteLine("Palindrome :)");
                //     } else {
                //         Console.WriteLine("Not palindrome :(");
                //     }
                Console.WriteLine(PalForCheck(s));
            }
        }

    }
