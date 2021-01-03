using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JigCSharp.Parser.SyntaxData.Common
{
    public abstract class Enumeration : IComparable
    {
        public string Name {get;}
        public int Id {get;}
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override string ToString() => Name;



        public override bool Equals(object other)
        {
            var otherValue = other as Enumeration;

            if (other == null)
            {
                return false;
            }

            var typeMatches = GetType() == other.GetType(); 
            var valueMatches = otherValue != null && Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }
        
        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public override int GetHashCode()
        {
            int hashCode = 1460282102;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            return hashCode;
        }
    }
}