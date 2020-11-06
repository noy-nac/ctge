using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine.IO;
using Engine.Data.Enums;
using Engine.Data.Structs;

namespace Engine.Data {
    public class Attributes : Stats {

        public const string FILE_NAME = @"attribute-array";

        public const string ROM_PATH = @"data\rom\property\attribute-stats\";
        public const string FILE_PATH = @"data\ram\save\property\attribute-stats\";

        public const double MINIMUM = 0.0001;
        public const double MAXIMUM = 9999.9;

        public const double CLASS_SCALE = 1.66;
        public const double GENDER_SCALE = 1;
        public const double NATION_SCALE = 1.15;
        public const double RACE_SCALE = 1.25;
        public const double BIOME_SCALE = 0.75;

        public const byte SIZE = 14;

        public double[] Attribute {
            get { return set; }
        }

        public double Strength {
            get { return set[(byte)ATTRIBUTE_ID.Strength]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Strength] = value;
            }
        }

        public double Vitality {
            get { return set[(byte)ATTRIBUTE_ID.Vitality]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Vitality] = value;
            }
        }

        public double Stamina {
            get { return set[(byte)ATTRIBUTE_ID.Stamina]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Stamina] = value;
            }
        }

        public double Intellect {
            get { return set[(byte)ATTRIBUTE_ID.Intellect]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Intellect] = value;
            }
        }

        public double Acumen {
            get { return set[(byte)ATTRIBUTE_ID.Acumen]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Acumen] = value;
            }
        }

        public double Wisdom {
            get { return set[(byte)ATTRIBUTE_ID.Wisdom]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Wisdom] = value;
            }
        }

        public double Speed {
            get { return set[(byte)ATTRIBUTE_ID.Speed]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Speed] = value;
            }
        }

        public double Agility {
            get { return set[(byte)ATTRIBUTE_ID.Agility]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Agility] = value;
            }
        }

        public double Dexterity {
            get { return set[(byte)ATTRIBUTE_ID.Dexterity]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Dexterity] = value;
            }
        }

        public double Solidity {
            get { return set[(byte)ATTRIBUTE_ID.Solidity]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Solidity] = value;
            }
        }

        public double Magnetism {
            get { return set[(byte)ATTRIBUTE_ID.Magnetism]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Magnetism] = value;
            }
        }

        public double Karma {
            get { return set[(byte)ATTRIBUTE_ID.Karma]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Karma] = value;
            }
        }

        public double Tenacity {
            get { return set[(byte)ATTRIBUTE_ID.Tenacity]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Tenacity] = value;
            }
        }

        public double Luck {
            get { return set[(byte)ATTRIBUTE_ID.Luck]; }
            set {
                value = Restrict(value);

                set[(byte)ATTRIBUTE_ID.Luck] = value;
            }
        }

        public Attributes() : base(SIZE) {
            minimum = MINIMUM;
            maximum = MAXIMUM;

            file = new CSV(FILE_NAME, FILE_PATH, false);
        }

        public void Generate(Class @class, Race race, Nation nation, Biome biome, Gender gender) {
            GenerateBase(@class);

            Randomize(2);

            RaceModifier(race);
            NationModifier(nation);
            BiomeModifier(biome);
            GenderModifier(gender);
        }

        public void GenderModifier(Gender gender) {
            CSV modifier = new CSV("gender", ROM_PATH, true);

            _Edit(modifier.ParseSet((int)gender.id, GENDER_SCALE));
        }

        public void RaceModifier(Race race) {
            CSV modifier = new CSV("race", ROM_PATH, true);

            _Edit(modifier.ParseSet((int)race.id, RACE_SCALE));
        }

        public void BiomeModifier(Biome biome) {
            CSV modifier = new CSV("biome", ROM_PATH, true);

            _Edit(modifier.ParseSet((int)biome.id, BIOME_SCALE));
        }

        public void NationModifier(Nation nation) {
            CSV modifier = new CSV("nation", ROM_PATH, true);

            _Edit(modifier.ParseSet((int)nation.id, NATION_SCALE));
        }

        public void GenerateBase(Class @class) {
            CSV @base = new CSV("class", ROM_PATH, true);

            _Set(@base.ParseSet((int)@class.id, CLASS_SCALE));
        }

        public string[] List() {
            const byte PADDING = 9;

            string[] list = new string[SIZE];

            for(byte i = 0; i < SIZE; i++) {
                list[i] = ((ATTRIBUTE_ID)i).ToString().PadLeft(PADDING) + " " + ((short)set[i]).ToString();
            }
            return list;
        }

        public double Restrict(double value) {
            value = value > MAXIMUM ? MAXIMUM : value;
            value = value < MINIMUM ? MINIMUM : value;

            return value;
        }
        
    }
}
