﻿using System;
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

        internal static string Geometry()
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
        internal static string Tension()
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
        internal static string ProfileType()
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
        internal static string Epicycloid()
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
        internal static string Hipocycloid()
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
        internal static string TeethQuantity()
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
        internal static string RollDiameter()
        {
            switch (language)
            {
                case Language.POLISH:
                    return "Średnica rolki";
                case Language.ENGLISH:
                    return "Roll diameter";
            }
            return NOT_IMPLEMENTED_MESSAGE;
        }
        internal static string MajorDiameter()
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
        internal static string RootDiameter()
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
        internal static string RollSpacingDiameter()
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
        internal static string Eccentricity()
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
        internal static string ToothHeight()
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
        internal static string ToothHeightFactor()
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
        internal static string PinSpacingDiameter()
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
        internal static string RollingCircleDiameter()
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
        internal static string BaseDiameter()
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