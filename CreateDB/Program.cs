using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CreateDB
{
    class Program
    {
        private const string UCD_EAW_URL = "https://www.unicode.org/Public/UCD/latest/ucd/EastAsianWidth.txt";

        private static HttpClient httpClient = new HttpClient();
        
        static async Task Main(string[] args)
        {
            string EAW_File_Path = null;

            if (System.IO.File.Exists("ucd-eastasianwidth.txt"))
                EAW_File_Path = "ucd-eastasianwidth.txt";

            string EAW_DBFile_Path = "eawdb.txt";

            if (args.Length == 2)
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
            List<string> EAW_File_Meat = new List<string>();
            if (EAW_File_Path != null)
            {
                EAW_File_Meat = System.IO.File.ReadAllLines(EAW_File_Path).ToList();
            }
            else
            {
                string str = await httpClient.GetStringAsync(UCD_EAW_URL);
                str = str.Replace("\r\n", "\n");
                EAW_File_Meat = str.Split('\n').ToList();
            }

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

            Console.WriteLine("Parsing");
            string dbstr = CreateDB.Parse(EAW_File_Meat);

            Console.WriteLine("Saving");

            System.IO.File.WriteAllText(EAW_DBFile_Path, dbstr, System.Text.Encoding.ASCII);

            Console.WriteLine();
            Console.WriteLine("Success");
            Console.WriteLine($"DB File: \"{EAW_DBFile_Path}\"");
        }

        static string Cut_Unuse_Content(string str)
        {
            string tmp = str;
            tmp = Regex.Replace(tmp,@"#.*$",""); // Delete Comment
            tmp = Regex.Replace(tmp,@"\s+"," "); // Delete Space
            tmp = Regex.Replace(tmp,@"\s+$",""); // Delete Space in Front
            tmp = Regex.Replace(tmp,@"^\s+", ""); // Delete Space in Back
            return tmp;
        }
    }
}
