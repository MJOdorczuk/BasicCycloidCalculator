using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Miscs
{
    public static class Vocabulary
    {
        public enum Language { POLISH, ENGLISH}
        private static Language language = Language.POLISH;
        private static readonly string NOT_IMPLEMENTED_MESSAGE = "Translation not implemented yet";
        private static readonly List<Action> nameCalls = new List<Action>();

        public static void AddNameCall(Action action) => nameCalls.Add(action);
        public static void UpdateAllNames()
        {
            foreach(var call in nameCalls)
            {
                call();
            }
        }

        public static void SetLanguage(Language language)
        {
            Vocabulary.language = language;
            Vocabulary.UpdateAllNames();
        }
        public static Language GetLanguage() => Vocabulary.language;

        internal static string NotImplementedYet()
        {
            return NOT_IMPLEMENTED_MESSAGE;
        }

        public struct TabPagesNames
        {
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
            public static string Load()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Obciążenie";
                    case Language.ENGLISH:
                        return "Load";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
        }
        public struct ParameterLabels
        {
            public struct Geometry
            {
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
                public static string TeethQuantity()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Liczba zębów";
                        case Language.ENGLISH:
                            return "Teeth quantity";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string RollRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień rolki";
                        case Language.ENGLISH:
                            return "Roll radius";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string MajorDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica wierzchołkowa";
                        case Language.ENGLISH:
                            return "Majord diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string RootDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica koła stóp";
                        case Language.ENGLISH:
                            return "Root diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string RollSpacingDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica rozmieszczenia rolek";
                        case Language.ENGLISH:
                            return "Roll spacing diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string Eccentricity()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Mimośród";
                        case Language.ENGLISH:
                            return "Eccentricity";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string ToothHeight()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Wysokość zęba";
                        case Language.ENGLISH:
                            return "Tooth height";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string ToothHeightFactor()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Współczynnik wysokości zęba";
                        case Language.ENGLISH:
                            return "Tooth height factor";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string PinSpacingDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica rozmieszczenia sworzni";
                        case Language.ENGLISH:
                            return "Pin spacing diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string RollingCircleDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica okręgu toczącego";
                        case Language.ENGLISH:
                            return "Rolling circle diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string BaseDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica koła zasadniczego";
                        case Language.ENGLISH:
                            return "Base diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
            }
        }
        public struct BubbleMessages
        {
            public struct Geometry
            {
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