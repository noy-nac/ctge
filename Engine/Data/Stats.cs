using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine.IO;

namespace Engine.Data {
    public class Stats {

        protected CSV file;

        protected double minimum;
        protected double maximum;

        protected double[] set;

        public Stats(params double[] set) {
            this.set = set;
        }

        public Stats(int size) {
            set = new double[size];
        }

        public double SampleVariance() {
            double summed = 0;

            for(int i = 0; i < set.Length; i++) {
                summed += Math.Pow(set[i] - ArithmeticMean(), 2);
            }
            return summed / (set.Length - 1);
        }

        public double ArithmeticMean() {
            return Sum() / set.Length;
        }

        public double GeometricMean() {
            return Math.Pow(Math.Abs(Product()), 1.0 / set.Length);
        }

        public double HarmonicMean() {
            double[] setreciprocal = new double[set.Length];

            for(int i = 0; i < set.Length; i++) {
                setreciprocal[i] = 1.0 / set[i];
            }
            return set.Length / new Stats(setreciprocal).Sum();
        }

        public double VariationCoefficient() {
            return StandardDeviation() / ArithmeticMean();
        }

        public double StandardDeviation() {
            return Math.Sqrt(SampleVariance());
        }

        public double Sum() {
            double sum = 0;

            for(int i = 0; i < set.Length; i++) {
                sum += set[i];
            }
            return sum;
        }

        public double Product() {
            double product = 1;

            for(int i = 0; i < set.Length; i++) {
                product *= set[i];
            }
            return product;
        }

        public void Clear() {
            for(int i = 0; i < set.Length; i++) {
                set[i] = minimum;
            }
        }

        public void Save() {
            file.OverwriteSet(0, set);

            file.CreatePath();
            file.Save();
        }

        public void Load() {
            file.Load();

            set = file.ParseSet(0, 1);
        }

        public void Randomize(double restriction) {
            const double MINIMUM_RESTRICTION = 0.0001;
            const double MAXIMUM_RESTRICTION = 100000;

            const double REDUCTION_THRESHOLD = 0.45;

            Random random = new Random();

            double overflow = 0;

            double[] randomset = new double[set.Length];

            restriction = Math.Abs(restriction) > MAXIMUM_RESTRICTION ? MAXIMUM_RESTRICTION : restriction;
            restriction = Math.Abs(restriction) < MINIMUM_RESTRICTION ? MINIMUM_RESTRICTION : restriction;

            for(short i = 0; i < (set.Sum() + Math.Log(Math.Abs(restriction) + 1)) / set.Length; i++) {
                for(byte j = 0; j < set.Length; j++) {
                    randomset[j] = random.NextDouble() / Math.Abs(restriction);

                    if(random.NextDouble() < REDUCTION_THRESHOLD) {
                        randomset[j] *= -1;
                    }
                    randomset[j] += overflow / set.Length;
                    overflow -= overflow / set.Length;
                }
                overflow += _Edit(randomset);
            }
        }

        protected void _Set(params double[] set) {
            for(int i = 0; i < set.Length; i++) {
                this.set[i] = set[i];
            }
        }

        protected double _Edit(params double[] set) {
            double overflow = 0;

            for(int i = 0; i < set.Length; i++) {
                if(this.set[i] + set[i] < minimum) {
                    this.set[i] = minimum;

                    overflow += this.set[i] + set[i];
                }
                else if(this.set[i] + set[i] > maximum) {
                    this.set[i] = maximum;

                    overflow += this.set[i] + set[i];
                }
                else {
                    this.set[i] += set[i];
                }
            }
            return overflow;
        }
    }
}
