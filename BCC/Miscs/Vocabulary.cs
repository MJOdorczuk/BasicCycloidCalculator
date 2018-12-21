using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Miscs
{
    static class Vocabulary
    {
        enum Language { POLISH, ENGLISH}
        private static Language language = Language.ENGLISH;
        private static readonly string NOT_IMPLEMENTED_MESSAGE = "not implemented yet";

        public static string Geometry()
        {
            switch (language)
            {
                case Language.POLISH:
                    return "geometria";
                case Language.ENGLISH:
                    return "geometry";
            }
            return NOT_IMPLEMENTED_MESSAGE;
        }
        public static string Tension()
        {
            switch (language)
            {
                case Language.POLISH:
                    return "naprężenia";
                case Language.ENGLISH:
                    return "tension";
            }
            return NOT_IMPLEMENTED_MESSAGE;
        }
        public static string ProfileType()
        {
            switch (language)
            {
                case Language.POLISH:
                    return "Rodzaj zarysu";
                case Language.ENGLISH:
                    return "Profile type";
            }
            return NOT_IMPLEMENTED_MESSAGE;
        }
        public static string Epicycloid()
        {
            switch (language)
            {
                case Language.POLISH:
                    return "Epicykloidalny";
                case Language.ENGLISH:
                    return "Epicycloid";
            }
            return NOT_IMPLEMENTED_MESSAGE;
        }
        public static string Hipocycloid()
        {
            switch (language)
            {
                case Language.POLISH:
                    return "Hipocykloidalny";
                case Language.ENGLISH:
                    return "Hipocycloid";
            }
            return NOT_IMPLEMENTED_MESSAGE;
        }
    }
}