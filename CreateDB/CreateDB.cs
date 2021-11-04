using Get_Unicode_EastAsianWidth;
using System;
using System.Collections.Generic;
using static Get_Unicode_EastAsianWidth.DB;

namespace CreateDB
{
    internal class CreateDB
    {
        public static List<RangeInfo> RDBs = new List<RangeInfo>();

        public static void Parse(List<string> OriginalData)
        {
            for (int i = 0; i < OriginalData.Count; i++)
            {
                string[] raw = OriginalData[i].Split(';');

                RangeInfo tmp_rinf = new RangeInfo();
                tmp_rinf.Type = StrEAW[raw[1]]; // Get EAW Value From EAW File
                if (raw[0].Contains(".."))
                {
                    string[] range = raw[0].Replace("..", ":").Split(':');
                    tmp_rinf.Start_index = Convert.ToInt32(range[0], 16);
                    tmp_rinf.End_index = Convert.ToInt32(range[1], 16);
                }
                else
                {
                    tmp_rinf.Start_index = Convert.ToInt32(raw[0], 16);
                    tmp_rinf.End_index = tmp_rinf.Start_index;
                }

                RDBs.Add(tmp_rinf);
            }

            return;
        }

        public static string CreateTextFile()
        {
            string tmp = "";

            foreach (RangeInfo rif in RDBs)
                if (rif.Start_index == rif.End_index)
                    tmp += ($"{rif.Start_index},{rif.Type};");
                else
                    tmp += ($"{rif.Start_index},{rif.End_index},{rif.Type};");

            tmp = tmp.Substring(0,tmp.Length-1);

            return tmp;
        }
    }
}