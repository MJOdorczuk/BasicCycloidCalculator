namespace BCC.Miscs
{
    public static partial class Vocabulary
    {
        public struct BubbleMessages
        {
            public struct Geometry
            {
                /// <summary>
                /// The clique is improper
                /// When current choosen parameters are not allowed in such group
                /// </summary>
                /// <returns></returns>
                public static string TheCliqueIsImproper()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Ta klika jest niepoprawna";
                        case Language.ENGLISH:
                            return "The clique is improper";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Possible cliques are
                /// For list of possible groups of choosen parameters
                /// </summary>
                /// <returns></returns>
                internal static string PossibleCliquesAre()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Możliwymi klikami są";
                        case Language.ENGLISH:
                            return "Possible cliques are:";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
            }
        }
    }
}