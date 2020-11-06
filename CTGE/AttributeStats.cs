using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTGE {

    public enum ATTRIBUTE_ID : byte {
        Strength,
        Vitality,
        Stamina,
        Intellect,
        Acumen,
        Wisdom,
        Speed,
        Agility,
        Dexterity,
        Solidity,
        Magnetism,
        Karma,
        Tenacity,
        Luck,
    }

    public class AttributeStats {

        public const double MINIMUM = 0.0001;
        public const double MAXIMUM = 9999.9;

        public const byte SIZE = 14;

        private double[] attribute;

        public AttributeStats() {
            attribute = new double[SIZE];
        }

        public string[] List() {
            const string DISPLAY_FORMAT = "0.0";

            string[] list = new string[SIZE];

            for(byte i = 0; i < SIZE; i++) {
                list[i] = attribute[i].ToString(DISPLAY_FORMAT) + " " + (ATTRIBUTE_ID)i;
            }
            return list;
        }

        public double Sum() {
            double sum = 0;

            for(byte i = 0; i < SIZE; i++) {
                sum += attribute[i];
            }
            return sum;
        }

        public double Product() {
            double product = 1;

            for(byte i = 0; i < SIZE; i++) {
                product *= attribute[i];
            }
            return product;
        }

        public void Generate(Class @class, Race race, Homeland homeland, Region region) {
            const double RANDOMIZATION_RESTRICTION = 5;

            GenerateBase(@class);
            RaceModifier(race);
            HomelandModifier(homeland);
            RegionModifier(region);
            Randomize(RANDOMIZATION_RESTRICTION);
        }

        public void RaceModifier(Race race) {
            switch(race.id) {
                case RACE_ID.Error:
                    _Edit(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);
                    break;
                case RACE_ID.Empty:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case RACE_ID.None:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2);
                    break;
                case RACE_ID.Noynac:
                    _Edit(5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5);
                    break;
                case RACE_ID.Human:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 1); // 30
                    break;
                case RACE_ID.Android:
                    _Edit(2, 2, 2, 4, 2, 4, 2, 2, 2, 2, 2, 2, 2, 0); // 30
                    break;
                case RACE_ID.Cyborg:
                    _Edit(4, 4, 2, 2, 3, 0, 2, 1, 1, 2, 2, 1, 4, 2); // 30
                    break;
                case RACE_ID.Robot:
                    _Edit(4, 4, 4, 4, 3, 0, 2, 0, 0, 2, 3, 0, 4, 0); // 30
                    break;
                case RACE_ID.Amnther:
                    _Edit(1, 1, 2, 4, 5, 4, 2, 3, 3, 1, 1, 1, 2, 0); // 30
                    break;
                case RACE_ID.Victus:
                    _Edit(2, 2, 2, 4, 3, 1, 4, 1, 1, 1, 0, 3, 1, 5); // 30
                    break;
                case RACE_ID.Meliusculus:
                    _Edit(2, 1, 1, 4, 2, 0, 2, 2, 3, 1, 3, 1, 5, 3); // 30
                    break;
                case RACE_ID.Tripecus:
                    _Edit(4, 5, 2, 2, 2, 1, 3, 4, 0, 2, 2, 1, 2, 0); // 30
                    break;
                case RACE_ID.Doppelganger:
                    _Edit(2, 2, 2, 3, 2, 1, 2, 2, 2, 2, 0, 1, 5, 4); // 30
                    break;
                case RACE_ID.Elf:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Drow:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Halfling:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Hobbit:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Dwarve:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Gnome:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Gnoll:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Imp:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Giant:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Goblin:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Orc:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Ogre:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Troll:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Lycanthrope:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Minotaur:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Satyr:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                case RACE_ID.Centaur:
                    _Edit(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4);
                    break;
                default:
                    goto case RACE_ID.Error;
            }
        }

        public void RegionModifier(Region region) {
            switch(region.id) {
                case REGION_ID.Error:
                    _Edit(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);
                    break;
                case REGION_ID.Empty:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.None:
                    _Edit(0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35, 0.35);
                    break;
                case REGION_ID.Polar_Desert:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Tundra:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Taiga:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Temperate_Broadleaf_Forest:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Temperate_Steppe:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Subtropical_Rainforest:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Mediterranean_Vegetation:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Monsoon_Forest:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Arid_Desert:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Xeric_Shrubland:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Dry_Steppe:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Semiarid_Desert:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Grass_Savanna:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Tree_Savanna:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Subtropical_Dry_Forest:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Tropical_Rainforest:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Alpine_Tundra:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case REGION_ID.Montane_Forests:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                default:
                    goto case REGION_ID.Error;
            }
        }

        public void HomelandModifier(Homeland homeland) {
            switch(homeland.id) {
                case HOMELAND_ID.Error:
                    _Edit(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);
                    break;
                case HOMELAND_ID.Empty:
                    _Edit(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case HOMELAND_ID.None:
                    _Edit(0.14, 0.14, 0.14, 0.14, 0.14, 0.14, 0.14, 0.14, 0.14, 0.14, 0.14, 0.14, 0.14, 0.14);
                    break;
                case HOMELAND_ID.Ingens_Cistalia:
                    _Edit(-0.5, -0.5, -0.5, 1, 0.5, -0.25, -0.25, 0.25, 0.25, -0.5, 1, 0, 0.5, 1); // 2
                    break;
                case HOMELAND_ID.Ied_Kundetia:
                    _Edit(0.5, 0.25, 0.5, -0.5, -0.25, -0.5, 0.5, 0.25, 0.25, 0.25, -0.25, -0.5, 1, 0.5); // 2
                    break;
                case HOMELAND_ID.Quod_Sacrosanct:
                    _Edit(0.5, 0.5, 0.25, -1, 0.25, 1, -0.5, -0.25, -0.25, 1, -0.25, 1, -0.5, 0.25); // 2
                    break;
                case HOMELAND_ID.Cerkeran:
                    _Edit(-0.25, -0.25, 0, -0.5, 1, -0.25, 0.25, 0.5, 0.5, 0.25, 0.25, 0.25, -0.25, 0.5); // 2
                    break;
                case HOMELAND_ID.Sid_Praternit:
                    _Edit(1, -0.25, 0.5, -0.5, -0.25, 0.25, 0, 0, 0.25, 0.75, -0.25, -0.25, 1, -0.25); // 2
                    break;
                case HOMELAND_ID.Zum_Tregaron:
                    _Edit(0.25, 0.25, 0.25, -1, 0.25, -0.5, 0.25, 0.25, 0.25, 0.5, 0.25, 0.5, 0.5, 0); // 2
                    break;
                case HOMELAND_ID.Nihilavera:
                    _Edit(-0.25, -0.25, 0.25, 1, 0.25, 1, 0.25, -0.25, -0.25, 0.25, -0.5, 0.5, 0.5, -0.5); // 2
                    break;
                case HOMELAND_ID.Nan_Harapext:
                    _Edit(0.25, -0.25, 0, 0.25, 0.5, 0.5, -0.5, -0.25, -0.25, 0.5, 0.25, 0.5, 0.75, -0.25); // 2
                    break;
                case HOMELAND_ID.Kinslaunth:
                    _Edit(0.5, -0.25, 0.25, -0.5, 0.25, -0.25, 0, -0.25, -0.25, 0.5, 1, 0.25, 0.5, 0.25); // 2
                    break;
                case HOMELAND_ID.Grav_Septres:
                    _Edit(-0.25, -0.25, -0.25, 0.5, 0.25, 0.5, 0.25, -0.25, -0.25, 0, 0.5, 0.75, 0.25, 0.25); // 2
                    break;
                case HOMELAND_ID.Loukuttens:
                    _Edit(0.25, 0.25, 0.25, -0.5, 0.25, 0.5, -0.25, 0.25, 0.25, -0.25, -0.25, -0.5, 0.75, 1); // 2
                    break;
                case HOMELAND_ID.Enam_Isacarathe:
                    _Edit(1, -0.25, 0, 0.25, -0.25, -0.75, 0.25, -0.25, 0.75, -0.25, 0.25, -0.25, 1, 0.5); // 2
                    break;
                default:
                    goto case HOMELAND_ID.Error;
            }
        }

        public void GenerateBase(Class @class) {
            switch(@class.id) {
                case CLASS_ID.Error:
                    _Set(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);
                    break;
                case CLASS_ID.Empty:
                    _Set(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
                case CLASS_ID.None:
                    _Set(3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3);
                    break;
                case CLASS_ID.Super:
                    _Set(10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);
                    break;
                case CLASS_ID.Developer:
                    _Set(5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5);
                    break;
                case CLASS_ID.Barbarian:
                    _Set(5, 5, 5, 1, 3, 1, 3, 1, 2, 2, 5, 2, 4, 5); // 39
                    break;
                case CLASS_ID.Archer:
                    _Set(2, 2, 4, 3, 4, 3, 4, 5, 5, 3, 2, 3, 2, 2); // 42
                    break;
                case CLASS_ID.Rogue:
                    _Set(3, 3, 5, 2, 5, 1, 4, 4, 3, 2, 5, 2, 3, 2); // 42
                    break;
                case CLASS_ID.Wizard:
                    _Set(2, 5, 2, 5, 1, 5, 2, 1, 2, 4, 1, 5, 5, 4); // 40
                    break;
                case CLASS_ID.Thief:
                    _Set(3, 2, 4, 2, 4, 1, 4, 5, 5, 1, 5, 1, 2, 5); // 39
                    break;
                case CLASS_ID.Chevalier:
                    _Set(4, 5, 3, 3, 1, 3, 2, 1, 2, 5, 5, 5, 4, 1); // 43
                    break;
                case CLASS_ID.Pirate:
                    _Set(4, 2, 3, 2, 4, 1, 3, 5, 5, 2, 4, 1, 3, 5); // 39
                    break;
                case CLASS_ID.Ranger:
                    _Set(3, 4, 4, 3, 3, 2, 4, 2, 3, 5, 2, 3, 2, 4); // 40
                    break;
                case CLASS_ID.Assassin:
                    _Set(4, 1, 1, 4, 5, 1, 4, 4, 5, 1, 3, 1, 5, 5); // 39
                    break;
                case CLASS_ID.Mage:
                    _Set(2, 5, 2, 5, 1, 5, 2, 1, 2, 4, 1, 5, 5, 4); // 40
                    break;
                case CLASS_ID.Doctor:
                    _Set(2, 5, 2, 4, 3, 4, 2, 1, 5, 5, 2, 5, 2, 2); // 42
                    break;
                case CLASS_ID.Bandit:
                    _Set(5, 4, 4, 1, 4, 2, 3, 2, 4, 2, 4, 1, 3, 5); // 39
                    break;
                case CLASS_ID.Druid:
                    _Set(1, 5, 2, 5, 2, 5, 1, 1, 2, 5, 1, 5, 4, 5); // 39
                    break;
                default:
                    goto case CLASS_ID.Error;
            }
        }

        public void Randomize(double restriction) {
            const double MINIMUM_RESTRICTION = 0.1;
            const double MAXIMUM_RESTRICTION = 10;

            const double REDUCTION_THRESHOLD = 0.5;

            Random random = new Random();

            double overflow = 0;

            double[] randomset = new double[SIZE];

            restriction = Math.Abs(restriction) > MAXIMUM_RESTRICTION ? MAXIMUM_RESTRICTION : restriction;
            restriction = Math.Abs(restriction) < MINIMUM_RESTRICTION ? MINIMUM_RESTRICTION : restriction;

            for(long i = 0; i < (Sum() + Math.Log(Math.Abs(restriction) + 1)) / SIZE; i++) {
                for(byte j = 0; j < SIZE; j++) {
                    randomset[j] = random.NextDouble() / Math.Abs(restriction);

                    if(random.NextDouble() < REDUCTION_THRESHOLD) {
                        randomset[j] *= -1;
                    }
                    randomset[j] += overflow / SIZE;
                    overflow -= overflow / SIZE;
                }
                overflow += _Edit(randomset);
            }  
        }

        public void Clear() {
            for(byte i = 0; i < SIZE; i++) {
                attribute[i] = MINIMUM;
            }
        }

        public void Modify(ATTRIBUTE_ID id, double quantity) {
            attribute[(int)id] += quantity;
        }

        public double Read(ATTRIBUTE_ID id) {
            return attribute[(int)id];
        }

        // quickly SET all attributes in a single line
        private void _Set(params double[] attribute) {
            for(byte i = 0; i < SIZE; i++) {
                this.attribute[i] = attribute[i];
            }
        }

        // quickly EDIT all attributes in a single line
        private double _Edit(params double[] attribute) { 
            double overflow = 0;

            for(byte i = 0; i < SIZE; i++) {
                if(this.attribute[i] + attribute[i] < MINIMUM) {
                    this.attribute[i] = MINIMUM;

                    overflow += this.attribute[i] + attribute[i];
                }
                else if(this.attribute[i] + attribute[i] > MAXIMUM) {
                    this.attribute[i] = MAXIMUM;

                    overflow += this.attribute[i] + attribute[i];
                }
                else {
                    this.attribute[i] += attribute[i];
                }
            }
            return overflow;
        }

    }
}
