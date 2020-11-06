
namespace Engine.Entity.Property {
    public class Name {

        private string prefix;
        private string honor;
        private string first;
        private string nick;
        private string middle;
        private string preposition;
        private string last;
        private string suffix;

        public string Prefix {
            get { return prefix; }
            set { prefix = value; }
        }

        public string Honor {
            get { return honor; }
            set { honor = value; }
        }

        public string First {
            get { return first; }
            set { first = value; }
        }

        public string Nick {
            get { return nick; }
            set { nick = value; }
        }

        public string Middle {
            get { return middle; }
            set { middle = value; }
        }

        public string Preposition {
            get { return preposition; }
            set { preposition = value; }
        }

        public string Last {
            get { return last; }
            set { last = value; }
        }

        public string Suffix {
            get { return suffix; }
            set { suffix = value; }
        }

        public Name(string prefix, string honor, string first, string nick, string middle, string preposition, string last, string suffix) {
            this.prefix = prefix;
            this.honor = honor;
            this.first = first;
            this.nick = nick;
            this.middle = middle;
            this.preposition = preposition;
            this.last = last;
            this.suffix = suffix;
        }
    }
}
