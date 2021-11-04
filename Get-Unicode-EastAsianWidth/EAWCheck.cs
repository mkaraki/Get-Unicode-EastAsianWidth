namespace Get_Unicode_EastAsianWidth
{
    public static class EAWCheck
    {
        private static InternalDB idb;

        public static void PreloadDB()
            => idb.InitDB();

        public static void SetDBString(string dbstr)
            => idb.InitDB(dbstr);

        private static CharSize GetCharSize(char target, bool EAW_A_To_Full = false)
        {
            if (!idb.DBReady) idb.InitDB();

            foreach (DB.RangeInfo rif in idb.RDBs)
            {
                if (target >= rif.Start_index && target <= rif.End_index)
                {
                    return DB.GetSize(rif.Type, EAW_A_To_Full ? CharSize.FullWidth : CharSize.HalfWidth);
                }
            }
            return CharSize.Unknown;
        }

        public static bool IsFullWidth(char Character, bool EAW_A_To_Full = false)
            => GetCharSize(Character, EAW_A_To_Full) == CharSize.FullWidth;

        public static bool IsHalfWidth(char Character, bool EAW_A_To_Full = false)
            => !IsFullWidth(Character, EAW_A_To_Full);

        /// <summary>
        /// Alias of GetStrLenWithEAW
        /// </summary>
        public static int LenB(string Text, bool EAW_A_To_Full = false) => GetStrLenWithEAW(Text, EAW_A_To_Full);

        public static int GetStrLenWithEAW(string Text, bool EAW_A_To_Full = false)
        {
            int toret = 0;

            foreach (char t_char in Text)
                toret += IsFullWidth(t_char, EAW_A_To_Full) ? 2 : 1;

            return toret;
        }
    }

    /// <summary>
    /// 0..1 : Full width character
    /// 2..4 : Harf width character
    /// 5 : Case by case (In this library. It will show in Harf width character)
    /// </summary>
    public enum EAW
    {
        F = 0,
        W = 1,
        H = 2,
        Na = 3,
        N = 4,
        A = 5,
    }

    /// <summary>
    /// CaseByCase is not used in currently
    /// Unknown is only used in internal
    /// </summary>
    public enum CharSize
    {
        FullWidth = 0,
        HalfWidth = 1,
        CaseByCase = 2,
        Unknown = 3,
    }
}