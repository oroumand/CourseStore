using System;
using System.Collections.Generic;
using System.Text;

namespace CourseStore.Core.Domain.ValueObjects
{
    public abstract class BaseValueObject<T>
                where T : BaseValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;

            if (ReferenceEquals(valueObject, null))
                return false;

            return IsEqual(valueObject);
        }


        protected abstract bool IsEqual(T other);


        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }


        protected abstract int GetHashCodeCore();


        public static bool operator ==(BaseValueObject<T> a, BaseValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }


        public static bool operator !=(BaseValueObject<T> a, BaseValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
