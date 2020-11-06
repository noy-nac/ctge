using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Engine.IO {
    public class LineFile {

        public const string EXTENTION = ".clf";
        public const string VERSION = "CLF v1.0.0.0";

        protected string extention;

        protected string name;
        protected string path;
        protected string id;

        protected string[] line;

        protected int position;

        protected bool saved;
        protected bool loaded;

        public string[] Line {
            get { return line; }
        }

        public string ID {
            get { return id; }
        }

        public string Name {
            get { return name; }
            set {
                if(saved || loaded) {
                    Copy(value, path);
                    Delete();
                }
                name = value;
                id = path + name + extention;
            }
        }

        public string Path {
            get { return path; }
            set {
                if(saved || loaded) {
                    Copy(name, value);
                    Delete();
                }
                path = value;
                id = path + name + extention;
            }
        }

        public string Extention {
            get { return extention; }
        }

        public int LineCount {
            get { return line.Length; }
        }

        public bool Saved {
            get { return saved; }
        }

        public bool Loaded {
            get { return loaded; }
        }

        public int CurrentLine {
            get { return position; }
            set {
                if(value > line.Length) {
                    Allocate(value - line.Length);

                    position = value;
                }
                else if(value < 0) {
                    position = 0;
                }
                else {
                    position = value;
                }
            }
        }

        public LineFile(string name, string path, bool load) {
            extention = EXTENTION;

            this.name = name;
            this.path = path;
            id = path + name + extention;

            line = new string[0];

            saved = false;
            loaded = false;

            if(load) {
                Load();
            }
        }

        public void CreatePath() {
            Directory.CreateDirectory(path);
        }

        public void Delete() {
            File.Delete(id);
        }

        public void Clear() {
            line = new string[0];
        }

        public LineFile Copy(string copyname, string copypath) {
            LineFile copy = new LineFile(copyname, copypath, false);

            copy.CreatePath();
            copy.Overwrite(0, line);

            return copy;
        }

        public void Allocate(int lines) {
            string[] lastline = new string[line.Length];

            for(int i = 0; i < lastline.Length; i++) {
                lastline[i] = line[i];
            }
            line = new string[line.Length + lines];

            for(int i = 0; i < (lastline.Length < line.Length ? lastline.Length : line.Length); i++) {
                line[i] = lastline[i];
            }
        }

        public void Overwrite(int startline, params string[] nextline) {
            position = startline;

            if(startline + nextline.Length > line.Length) {
                Allocate(startline + nextline.Length - line.Length);
            }
            for(int i = 0; i < nextline.Length; i++) {
                line[position++] = nextline[i];
            }
        }

        public void Append(params string[] nextline) {
            Overwrite(line.Length, nextline);
        }

        public void BlindWrite(params string[] nextline) {
            Overwrite(position, nextline);
        }

        public void Load() {
            line = File.ReadAllLines(id);

            position = line.Length - 1;

            loaded = true;
        }

        public void Save() {
            File.WriteAllLines(id, line);

            saved = true;
        }
    }
}
