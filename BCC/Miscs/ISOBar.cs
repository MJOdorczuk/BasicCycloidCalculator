namespace BCC.Miscs
{
    public static partial class Vocabulary
    {
        public struct ISOBar
        {
            /// <summary>
            /// Edit
            /// </summary>
            /// <returns></returns>
            internal static string Edit()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Edycja";
                    case Language.ENGLISH:
                        return "Edit";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
            /// <summary>
            /// Exit
            /// </summary>
            /// <returns></returns>
            internal static string Exit()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Zamknij";
                    case Language.ENGLISH:
                        return "Exit";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
            /// <summary>
            /// File
            /// </summary>
            /// <returns></returns>
            internal static string File()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Plik";
                    case Language.ENGLISH:
                        return "File";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
            /// <summary>
            /// Help
            /// </summary>
            /// <returns></returns>
            internal static string Help()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Pomoc";
                    case Language.ENGLISH:
                        return "Help";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
            /// <summary>
            /// New
            /// </summary>
            /// <returns></returns>
            internal static string New()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Nowy";
                    case Language.ENGLISH:
                        return "New";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
            /// <summary>
            /// Open
            /// </summary>
            /// <returns></returns>
            internal static string Open()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Otwórz";
                    case Language.ENGLISH:
                        return "Open";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
            /// <summary>
            /// Save
            /// </summary>
            /// <returns></returns>
            internal static string Save()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Zapisz";
                    case Language.ENGLISH:
                        return "Save";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
            /// <summary>
            /// View
            /// </summary>
            /// <returns></returns>
            internal static string View()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Widok";
                    case Language.ENGLISH:
                        return "View";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
        }
    }
}