﻿using System.Collections.Generic;
using Watchdog.Persistence;

namespace Watchdog.Entities
{
    public class RatingAgency : Persistable
    {
        private static readonly string tableName = "wdt_rating_agencies";
        private static readonly Dictionary<string, string> tableMapping = new Dictionary<string, string>
        {
            {"name", "Name" }
        };
        private static readonly RatingAgency defaultValue = new RatingAgency();
        public string Name { get; set; }
        public double Index { get; set; }

        public RatingAgency() { }

        public RatingAgency(string name)
        {
            Name = name;
        }

        public string GetTableName()
        {
            return tableName;
        }

        public List<string> GetTableHeader()
        {
            return new List<string>(tableMapping.Keys);
        }

        public double GetIndex()
        {
            return Index;
        }

        public void SetIndex(double index)
        {
            Index = index;
        }
        public static Persistable GetDefaultValue()
        {
            return defaultValue;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return obj is RatingAgency agency &&
                   Name == agency.Name &&
                   Index == agency.Index;
        }

        public override int GetHashCode()
        {
            int hashCode = -823584703;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Index.GetHashCode();
            return hashCode;
        }

        public Dictionary<string, string> GetTableMapping()
        {
            return tableMapping;
        }
    }
}
