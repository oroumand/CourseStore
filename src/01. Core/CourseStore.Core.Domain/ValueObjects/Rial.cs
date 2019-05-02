using CourseStore.Core.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseStore.Core.Domain.ValueObjects
{
    public class Rial : BaseValueObject<Rial>
    {
        public decimal Value { get; private set; }
        public bool IsZero => Value == 0;

        private Rial(decimal value)
        {
            Value = value;
        }
        public static Result<Rial> Create(decimal value)
        {
            if (value < 0)
            {
                Result.Fail("قیمت نمی‌تواند کمتر از 0 ریال باشد.");

            }
            return Result.Ok(new Rial(value));
        }
        public static Rial of(decimal value)
        {
            return (Rial)value;
        }
        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        protected override bool IsEqual(Rial other)
        {
            return Value == other.Value;
        }

        public static implicit operator decimal(Rial rial)
        {
            return rial.Value;
        }
        public static explicit operator Rial(decimal value)
        {
            return Create(value).Value;
        }

        public static Rial operator *(Rial rial, decimal multiplier)
        {
            return new Rial(rial.Value * multiplier);
        }

        public static Rial operator +(Rial left, Rial right)
        {
            return new Rial(left.Value + right.Value);
        }
    }
}
