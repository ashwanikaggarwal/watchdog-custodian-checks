﻿using System.Collections.Generic;

namespace Watchdog.Entities
{
    class Fund
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Isin { get; }
        public string CustodyAccountNumber { get; }
        public Currency Currency { get; }
        public List<Position> Positions { get; }
        public List<Rule> Rules { get; }
        public AssetAllocation AssetAllocation { get; }

        public Fund(int id, string name, string isin, string custodyAccountNumber, Currency currency)
        {
            Id = id;
            Name = name;
            Isin = isin;
            CustodyAccountNumber = custodyAccountNumber;
            Currency = currency;
            Positions = new List<Position>();
            Rules = new List<Rule>();
            AssetAllocation = new AssetAllocation();
        }
    }
}
