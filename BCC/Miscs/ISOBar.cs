using System;

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
                    case Vocabulary.Language.POLISH:
                        return "Edycja";
                    case Vocabulary.Language.ENGLISH:
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
                    case Vocabulary.Language.POLISH:
                        return "Zamknij";
                    case Vocabulary.Language.ENGLISH:
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
                    case Vocabulary.Language.POLISH:
                        return "Plik";
                    case Vocabulary.Language.ENGLISH:
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
                    case Vocabulary.Language.POLISH:
                        return "Pomoc";
                    case Vocabulary.Language.ENGLISH:
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
                    case Vocabulary.Language.POLISH:
                        return "Nowy";
                    case Vocabulary.Language.ENGLISH:
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
                    case Vocabulary.Language.POLISH:
                        return "Otwórz";
                    case Vocabulary.Language.ENGLISH:
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
                    case Vocabulary.Language.POLISH:
                        return "Zapisz";
                    case Vocabulary.Language.ENGLISH:
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
                    case Vocabulary.Language.POLISH:
                        return "Widok";
                    case Vocabulary.Language.ENGLISH:
                        return "View";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string Language()
            {
                switch (language)
                {
                    case Vocabulary.Language.POLISH:
                        return "Język";
                    case Vocabulary.Language.ENGLISH:
                        return "Language";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string Polish()
            {
                switch (language)
                {
                    case Vocabulary.Language.POLISH:
                        return "Polski";
                    case Vocabulary.Language.ENGLISH:
                        return "Polish";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string English()
            {
                switch (language)
                {
                    case Vocabulary.Language.POLISH:
                        return "Angielski";
                    case Vocabulary.Language.ENGLISH:
                        return "English";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
        }
    }
}