using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Data.Enums {
    public enum GENDER_ID : sbyte {
        Error = -128,
        Empty = -1,
        None = 0,
        Male,
        Female,
        Other,
    }
}
