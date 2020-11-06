using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTGE {

    public enum HOMELAND_ID : sbyte {
        Error = -128,
        Empty = -1,
        None = 0,
        Ingens_Cistalia,
        Ied_Kundetia,
        Quod_Sacrosanct,
        Cerkeran,
        Sid_Praternit,
        Zum_Tregaron,
        Nihilavera,
        Nan_Harapext,
        Kinslaunth,
        Grav_Septres,
        Loukuttens,
        Enam_Isacarathe,
    }

    public struct Homeland {
        public HOMELAND_ID id;
    }
}
