using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.IO {
    public class CSV : LineFile {

        public new const string EXTENTION = ".csv";
        public new const string VERSION = "C/CSV v1.0.0.0";

        public const char SEPARATOR = ',';

        public CSV(string name, string path, bool load) : base(name, path, false) {
            extention = EXTENTION;

            id = path + name + extention;

            if(load) {
                Load();
            }
        }

        public void OverwriteSubblock(int startline, double[][] subblock) {
            string[] nextline = new string[subblock.Length];

            for(int i = 0; i < nextline.Length; i++) {
                for(int j = 0; j < subblock[i].Length; j++) {
                    nextline[i] += subblock[i][j].ToString() + (j < subblock[i].Length - 1 ? SEPARATOR.ToString() : string.Empty);
                }
            }
            Overwrite(startline, nextline);
        }

        public void OverwriteSet(int atline, params double[] set) {
            OverwriteSubblock(atline, new double[][] { set });
        }

        public void AppendSubblock(double[][] subblock) {
            OverwriteSubblock(line.Length, subblock);
        }

        public void AppendSet(params double[] set) {
            AppendSubblock(new double[][] { set });
        }

        public double[][] ParseBlock(double scale) {
            double[][] block = new double[line.Length][];

            for(int i = 0; i < block.Length; i++) {
                block[i] = ParseSet(i, scale);
            }
            return block;
        }

        public double[] ParseSet(int atline, double scale) {
            string[] split = line[atline].Split(SEPARATOR);

            double[] set = new double[split.Length];

            for(int i = 0; i < split.Length; i++) {
                if(split[i].Contains(SEPARATOR)) {
                    split[i] = split[i].Remove(split[i].IndexOf(SEPARATOR), 1);
                }
                set[i] = double.Parse(split[i]) * scale;
            }
            return set;
        }
    }
}
