namespace Get_Unicode_EastAsianWidth
{
    public class EAWCheck
    {
        public static bool IsInited = false;

        public static bool IsFullWidth(char Character, bool EAW_A_To_Full = false)
        {
            if (!IsInited) { InternalDB.InitDB(); IsInited = true; }
            int charid = Character;

            CharSize defchar = CharSize.HalfWidth;
            if (EAW_A_To_Full)
                defchar = CharSize.FullWidth;
            CharSize csize = CharSize.Unknown;

            foreach (DB.RangeInfo rif in InternalDB.RDBs)
            {
                if (charid >= rif.Start_index && charid <= rif.End_index)
                {
                    csize = DB.GetSize(rif.Type, defchar);
                    break;
                }
            }

            if (csize == CharSize.FullWidth)
                return true;
            else
                return false;
        }

        public static bool IsHalfWidth(char Character, bool EAW_A_To_Full = false)
        {
            if (!IsInited) { InternalDB.InitDB(); IsInited = true; }
            int charid = Character;

            CharSize defchar = CharSize.HalfWidth;
            if (EAW_A_To_Full)
                defchar = CharSize.FullWidth;
            CharSize csize = CharSize.Unknown;

            foreach (DB.RangeInfo rif in InternalDB.RDBs)
            {
                if (charid >= rif.Start_index && charid <= rif.End_index)
                {
                    csize = DB.GetSize(rif.Type, defchar);
                    break;
                }
            }

            if (csize == CharSize.HalfWidth)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Alias of GetStrLenWithEAW
        /// </summary>
        public static int LenB(string Text, bool EAW_A_To_Full = false) => GetStrLenWithEAW(Text, EAW_A_To_Full);

        public static int GetStrLenWithEAW(string Text, bool EAW_A_To_Full = false)
        {
            if (!IsInited) { InternalDB.InitDB(); IsInited = true; }

            int toret = 0;

            CharSize defchar = CharSize.HalfWidth;
            if (EAW_A_To_Full)
                defchar = CharSize.FullWidth;

            foreach (char t_char in Text)
                foreach (DB.RangeInfo rif in InternalDB.RDBs)
                {
                    if (t_char >= rif.Start_index && t_char <= rif.End_index)
                    {
                        if (DB.GetSize(rif.Type, defchar) == CharSize.FullWidth)
                            toret += 2;
                        else
                            toret += 1;
                        break;
                    }
                }

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