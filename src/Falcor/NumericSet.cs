﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Falcor
{
    public sealed class NumericSet : KeySegment, IEnumerable<int>
    {
        private readonly List<NumberRange> _ranges = new List<NumberRange>();
        public override KeyType KeyType { get; } = KeyType.RangeSet;
        public override NumericSet AsNumericSet() => this;


        public NumericSet(IEnumerable<int> numericKeys)
        {
            _ranges.AddRange(numericKeys.Select(k => new NumberRange(k)));
        }

        public NumericSet(params NumericKey[] numericKeys)
        {
            _ranges.AddRange(numericKeys.Select(k => k.AsRange()));
        }



        [DebuggerStepThrough]
        public IEnumerator<int> GetEnumerator()
        {
            return _ranges.SelectMany(r => r.AsEnumerable()).GetEnumerator();
        }

        [DebuggerStepThrough]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator List<int>(NumericSet set) => set.ToList();
    }
}