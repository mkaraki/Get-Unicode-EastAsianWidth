using System;
using System.Collections.Generic;
using System.Linq;

namespace CreateDB
{
    class Program
    {
        private static string EAW_File_Path = "";

        private static string EAW_DBFile_Path = "";

        private static List<string> EAW_File_Meat = new List<string>();

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                EAW_File_Path = @"D:\EastAsianWidth.txt";
                EAW_DBFile_Path = @"D:\EAWDB";
            }
            else
            {
                if (System.IO.File.Exists(args[0]))
                    EAW_File_Path = args[0];
                else
                {
                    Console.WriteLine("No File");
                    return;
                }

                EAW_DBFile_Path = args[1];
            }

            Console.WriteLine("Reading EAW File");
            EAW_File_Meat = System.IO.File.ReadAllLines(EAW_File_Path).ToList();

            List<int> todel = new List<int>();

            Console.WriteLine("Cleaning EAW File");
            for (int i = 0; i < EAW_File_Meat.Count; i++)
            {
                EAW_File_Meat[i] = Cut_Unuse_Content(EAW_File_Meat[i]);

                if (EAW_File_Meat[i] == "")
                    todel.Add(i);
            }

            Console.WriteLine("Cleaning Space");
            for (int i = todel.Count; i > 0; i--)
            {
                EAW_File_Meat.Remove("");
            }
            todel = null;
            GC.Collect();

            Console.WriteLine("Parsing");
            CreateDB.Parse(EAW_File_Meat);

            Console.WriteLine("Saving");
            string TextMeat = CreateDB.CreateTextFile();

            System.IO.File.WriteAllText(EAW_DBFile_Path,TextMeat,System.Text.Encoding.UTF8);

            Console.WriteLine();
            Console.WriteLine("Success");
            Console.WriteLine($"DB File: \"{EAW_DBFile_Path}\"");

            Console.ReadLine();
        }

        static string Cut_Unuse_Content(string str)
        {
            string tmp = str;
            tmp = System.Text.RegularExpressions.Regex.Replace(tmp,@"#.*$",""); // Delete Comment
            tmp = System.Text.RegularExpressions.Regex.Replace(tmp,@"\s+"," "); // Delete Space
            tmp = System.Text.RegularExpressions.Regex.Replace(tmp,@"\s+$",""); // Delete Space in Front
            tmp = System.Text.RegularExpressions.Regex.Replace(tmp,@"^\s+", ""); // Delete Space in Back
            return tmp;
        }
    }
}
