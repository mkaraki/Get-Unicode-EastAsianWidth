using Get_Unicode_EastAsianWidth;
using System;
using System.Collections.Generic;
using static Get_Unicode_EastAsianWidth.DB;

namespace CreateDB
{
    internal class CreateDB
    {
        public static List<RangeInfo> RDBs = new List<RangeInfo>();

        public static string Parse(List<string> OriginalData)
        {
            List<string> dbstrdata = new List<string>();

            for (int i = 0; i < OriginalData.Count; i++)
            {
                string[] raw = OriginalData[i].Split(';');
                string[] range = raw[0].Replace("..", ":").Split(':');

                string sttcode = range[0].TrimStart('0');
                if (sttcode.Length == 0) sttcode = "0";

                if (range.Length == 1)
                    dbstrdata.Add($"{sttcode},{raw[1]}");
                else if (range.Length == 2)
                    dbstrdata.Add($"{sttcode},{range[1].TrimStart('0')},{raw[1]}");
            }

            return string.Join(';', dbstrdata);
        }
    }
}