using System;
using System.Collections.Generic;
using System.Text;

namespace Get_Unicode_EastAsianWidth
{
    public class DB
    {
        public static readonly Dictionary<string, EAW> StrEAW = new Dictionary<string, EAW>() {
            { "F",EAW.F },
            { "H",EAW.H },
            { "W",EAW.W },
            { "Na",EAW.Na },
            { "A",EAW.A },
            { "N",EAW.N },
        };

        public class RangeInfo
        {
            public int Start_index;
            public int End_index;
            public EAW Type;
        }

        public static CharSize GetSize(EAW eaw,CharSize EAW_A_To = CharSize.HalfWidth)
        {
            switch (eaw)
            {
                case EAW.F:
                case EAW.W:
                    return CharSize.FullWidth;
                case EAW.H:
                case EAW.Na:
                case EAW.N:
                    return CharSize.HalfWidth;
                case EAW.A:
                    return EAW_A_To;
                default:
                    throw new IndexOutOfRangeException("Unknown Char");
            }
        }
    }
}
