using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BCC.Core.Exceptions.WrongParameterValueExceptionPrototype;

namespace BCC.Core.Exceptions
{
    public enum Faults
    {
        None,
        RollDiameterNotPositive,
        TeethNumberNotPositive,
        MajorDiameterNotPositive,
        RootDiameterNotPositive,
        RollSpacingDiameterNotPositive,
        EccentricityNotPositive,
        ToothHeightNotPositive,
        MajorToRootDiameterDifferenceNegative
    }

    class WrongParameterValueExceptionPrototype
    {
        private readonly List<Faults> faultList = new List<Faults>();

        public WrongParameterValueExceptionPrototype() { }

        public void PushFault(Faults fault) { faultList.Add(fault); }

        public bool AllRight => faultList.Count == 0;

        public void Throw() => throw new WrongParameterValueException(faultList);
    }

    class WrongParameterValueException : Exception
    {
        private readonly List<Faults> faultList;

        internal WrongParameterValueException(List<Faults> faultList)
        {
            this.faultList = faultList;
        }

        public bool AreFaultsHandled => faultList.Count < 1;

        public Faults HandleNext()
        {
            if (AreFaultsHandled) return Faults.None;
            var ret = faultList[0];
            faultList.RemoveAt(0);
            return ret;
        }
    }
}
