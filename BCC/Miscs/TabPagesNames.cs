namespace BCC.Miscs
{
    public static partial class Vocabulary
    {
        public struct TabPagesNames
        {
            /// <summary>
            /// Geometry
            /// </summary>
            /// <returns></returns>
            public static string Geometry()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Geometria";
                    case Language.ENGLISH:
                        return "Geometry";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
            /// <summary>
            /// Dimensioning
            /// </summary>
            /// <returns></returns>
            public static string Dimensioning()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Wymiarowanie";
                    case Language.ENGLISH:
                        return "Dimensioning";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
            /// <summary>
            /// Results
            /// </summary>
            /// <returns></returns>
            internal static string Results()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Wyniki";
                    case Language.ENGLISH:
                        return "Results";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
        }
    }
}