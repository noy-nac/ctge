using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTGE {

    public enum GENDER_ID : sbyte {
        Error = -128,
        Empty = -1,
        None = 0,
        Male,
        Female,
        Other,
        Not_Specified,
        Boy,
        Girl,
        Man,
        Woman,
        Agender,
        Agenderflux,
        Androgyne,
        Aporagender,
        Bigender,
        Butch,
        Demiboy,
        Demigender,
        Demigirl,
        Enby,
        Femme,
        Female_To_X,
        Genderfluid,
        Genderflux,
        Genderless,
        Gender_Neutral,
        Genderqueer,
        Gendervoid,
        Intergender,
        Maverique,
        Male_To_X,
        Neutrois,
        Nonbinary,
        Polygender,
        Queer,
        Trans_Feminine,
        Transgender,
        Vaguegender,
        X_To_X,
    }

    public struct Gender {
        public GENDER_ID id;
    }
}
