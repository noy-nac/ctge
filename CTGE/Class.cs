using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTGE {

    public enum CLASS_ID : sbyte {
        Error = -128,
        Empty = -1,
        None = 0,
        Super,
        Developer,
        Barbarian,
        Archer,
        Rogue,
        Wizard,
        Thief,
        Chevalier,
        Pirate,
        Ranger,
        Assassin,
        Mage,
        Doctor,
        Bandit,
        Druid,
    }

    public struct Class {
        public CLASS_ID id;
    }

}