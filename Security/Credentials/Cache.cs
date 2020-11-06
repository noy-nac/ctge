using S = System;
using IO = System.IO;
using C = System.Security.Cryptography;
using T = System.Text;

namespace CTGE.Security.Credentials {
    public class Cache {

        public const byte RANDOM_ROUNDS = 7;

        public const byte MINIMUM_RANDOM = 0;
        public const byte MAXIMUM_RANDOM = 1;
        public const double HALF_RANDOM = 0.5;

        public const ushort SALT_LENGTH = 512;

        public const byte ID_INITIAL = 0;
        public const byte TOKEN_INITIAL = 0;
        public const byte ACTIVE_INITIAL = 0;

        public const byte OWNER_KEY_MINIMUM = 4;
        public const byte OWNER_KEY_MAXIMUM = 64;

        public readonly string CACHE_PATH = @"concilics\ctge\credentials\cache\";

        public readonly string LAST_ID_NAME = "lastid";
        public readonly string ACTIVE_NAME = "active";
        public readonly string SESSION_NAME = @"\session";

        public readonly string ID_PATH = @"concilics\ctge\credentials\cache\id\";
        public readonly string TOKEN_PATH = @"concilics\ctge\credentials\cache\token\";

        public readonly string OWNER_PATH = @"concilics\ctge\credentials\cache\owner\";

        public const bool IN_PHASE_NULL = false;
        public const bool OKAY_NULL = true;
        public const bool ACTIVE_NULL = false;
        public const bool OPEN_NULL = false;

        protected C.RandomNumberGenerator random;

        protected C.HashAlgorithm sha256;

        protected ulong? id;
        protected ulong? token;

        protected ulong? visa;

        protected uint? pin;

        protected string owner;
        protected string key;

        protected string salt;
        protected string hash;

        protected bool? inphase;
        protected bool? okay;
        protected bool? active;
        protected bool? open;

        protected double _Random {
            get {
                double cumulation = MINIMUM_RANDOM;

                byte[] state = new byte[RANDOM_ROUNDS];

                random.GetBytes(state);

                for(byte i = 0; i < RANDOM_ROUNDS; i++)
                    cumulation += state[i] / S.Math.Pow(byte.MaxValue + 1, i + 1);

                return cumulation;
            }
        }

        protected ulong? _ID {
            get { return id; }
            set { id = value; }
        }

        public ulong? ID {
            get { return id; }
        }

        protected ulong? _Token {
            get { return token; }
            set { token = value; }
        }

        public ulong? Token {
            get { return token; }
        }

        protected ulong? _Visa {
            get { return visa; }
            set { visa = value; }
        }

        protected uint? _PIN {
            get { return pin; }
            set { pin = value; }
        }

        protected string _Owner {
            get { return owner; }
            set {
                if(value == null || (value.Length >= OWNER_KEY_MINIMUM && value.Length <= OWNER_KEY_MAXIMUM))
                    owner = value;
            }
        }

        public string Owner {
            get { return owner; }
        }

        protected string _Key {
            get { return key; }
            set {
                if(value == null || (value.Length >= OWNER_KEY_MINIMUM && value.Length <= OWNER_KEY_MAXIMUM))
                    key = value;
            }
        }

        protected string _Salt {
            get { return salt; }
            set { salt = value; }
        }

        public string Salt {
            get { return salt; }
        }

        protected string _Hash {
            get { return hash; }
            set { hash = value; }
        }

        protected bool? _InPhase {
            get { return inphase; }
            set { inphase = value; }
        }

        protected bool? _Okay {
            get { return okay; }
            set { okay = value; }
        }

        protected bool? _Active {
            get { return active; }
            set { active = value; }
        }

        public bool? Active {
            get { return active; }
        }

        protected bool? _Open {
            get { return open; }
            set { open = value; }
        }

        public bool? Open {
            get { return open; }
        }

        public Cache(string owner, string key, uint pin) {
            random = new C.RNGCryptoServiceProvider();

            sha256 = new C.SHA256CryptoServiceProvider();

            Initalize(owner, key, pin);
        }

        protected void Initalize(string owner, string key, uint pin) {
            Nullify();

            _PIN = pin;

            _Owner = owner;
            _Key = key;

            GenerateSalt();
            GenerateHash();
        }

        public void Dispose() {
            random.Dispose();

            sha256.Dispose();
        }

        public bool Create(string owner, string key) {
            if(ValidStates(IN_PHASE_NULL, OKAY_NULL, ACTIVE_NULL, OPEN_NULL) && ValidIdentity(owner, key)) {
                if(/*_ID == null && _Token == null && _Visa == null*/ ValidValues(false, false, false) && GenerateID()) {
                    _InPhase = IN_PHASE_NULL;
                    _Okay = OKAY_NULL;
                    _Active = ACTIVE_NULL;
                    _Open = !OPEN_NULL;

                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                _Okay = !OKAY_NULL;

                return false;
            }
        }

        public ulong? Login(string owner, string key, uint? pin = null) {
            if(ValidStates(IN_PHASE_NULL, null, ACTIVE_NULL, !OPEN_NULL) && ValidIdentity(owner, key) && (pin == _PIN || ((_Okay ?? OKAY_NULL) && pin == null))) {
                if(/*_ID != null && _Token == null && _Visa == null*/ ValidValues(true, false, false) && GenerateToken() && GenerateVisa()) {
                    _Okay = OKAY_NULL;
                    _Active = !ACTIVE_NULL;

                    return _Visa;
                }
                else {
                    return null;
                }
            }
            else {
                _Okay = !OKAY_NULL;

                return null;
            }
        }

        public bool Logout(ulong visa) {
            if(ValidStates(IN_PHASE_NULL, null, !ACTIVE_NULL, !OPEN_NULL) && visa == _Visa) {
                if(/*_ID != null && _Token != null*/ ValidValues(true, true, null) && DeleteToken() && DeleteVisa()) {
                    _Active = ACTIVE_NULL;

                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                _Okay = !OKAY_NULL;

                return false;
            }
        }

        public bool Delete(string owner, string key, ulong visa, uint pin) {
            if(ValidStates(IN_PHASE_NULL, null, !ACTIVE_NULL, !OPEN_NULL) && ValidIdentity(owner, key) && visa == _Visa && pin == _PIN) {
                if(/*_ID != null && _Token != null*/ ValidValues(true, true, null)) {
                    Logout(visa);

                    _Open = OPEN_NULL;

                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                _Okay = !OKAY_NULL;

                return false;
            }
        }

        public bool Restore(string owner, string key) {
            if(ValidStates(IN_PHASE_NULL, null, ACTIVE_NULL, OPEN_NULL) && ValidIdentity(owner, key)) {
                if(/*_ID != null && _Token == null && _Visa == null*/ ValidValues(true, false, false)) {
                    _Open = !OPEN_NULL;

                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                _Okay = !OKAY_NULL;

                return false;
            }
        }

        protected bool ValidIdentity(string owner, string key) {
            return owner.Equals(_Owner) && key.Equals(_Key);
        }

        protected bool ValidStates(bool? inphase, bool? okay, bool? active, bool? open) {
            return (inphase == null ? true : inphase == (_InPhase ?? IN_PHASE_NULL)) && (okay == null ? true : okay == (_Okay ?? OKAY_NULL)) && (active == null ? true : active == (_Active ?? ACTIVE_NULL)) && (open == null ? true : open == (_Open ?? OPEN_NULL));
        }

        protected bool ValidValues(bool? id, bool? token, bool? visa) {
            return (id == null ? true : id == (_ID != null)) && (token == null ? true : token == (_Token != null)) && (visa == null ? true : visa == (_Visa != null));
        }

        protected bool GenerateID() {
            if(ValidValues(false, null, null)) {
                if(IO.Directory.Exists(CACHE_PATH)) {
                    if(IO.Directory.Exists(ID_PATH)) {
                        if(IO.File.Exists(CACHE_PATH + LAST_ID_NAME)) {
                            _ID = S.Convert.ToUInt64(IO.File.ReadAllText(CACHE_PATH + LAST_ID_NAME));

                            IO.File.WriteAllText(CACHE_PATH + LAST_ID_NAME, (_ID + 1).ToString());

                            IO.Directory.CreateDirectory(ID_PATH + _ID);
                        }
                        else {
                            IO.File.WriteAllText(CACHE_PATH + LAST_ID_NAME, ID_INITIAL.ToString());

                            GenerateID();
                        }
                    }
                    else {
                        IO.Directory.CreateDirectory(ID_PATH);

                        GenerateID();
                    }
                }
                else {
                    IO.Directory.CreateDirectory(CACHE_PATH);

                    GenerateID();
                }
                return true;
            }
            else {
                return false;
            }
        }

        protected bool GenerateToken() {
            if(ValidValues(true, false, null)) {
                if(IO.Directory.Exists(CACHE_PATH)) {
                    if(IO.Directory.Exists(TOKEN_PATH)) {
                        if(IO.File.Exists(CACHE_PATH + ACTIVE_NAME)) {
                            IO.File.WriteAllText(CACHE_PATH + ACTIVE_NAME, (S.Convert.ToUInt64(IO.File.ReadAllText(CACHE_PATH + ACTIVE_NAME)) + 1).ToString());

                            for(ulong i = TOKEN_INITIAL; ; i++) {
                                if(!IO.File.Exists(TOKEN_PATH + i)) {
                                    IO.File.Create(TOKEN_PATH + i).Close();

                                    _Token = i;

                                    break;
                                }
                            }
                            IO.File.WriteAllText(ID_PATH + _ID + SESSION_NAME, _Token.ToString());
                        }
                        else {
                            IO.File.WriteAllText(CACHE_PATH + ACTIVE_NAME, ACTIVE_INITIAL.ToString());

                            GenerateToken();
                        }
                    }
                    else {
                        IO.Directory.CreateDirectory(TOKEN_PATH);

                        GenerateToken();
                    }
                }
                else {
                    IO.Directory.CreateDirectory(CACHE_PATH);

                    GenerateToken();
                }
                return true;
            }
            else {
                return false;
            }
        }

        protected bool DeleteToken() {
            if(ValidValues(true, true, null)) {
                if(IO.Directory.Exists(CACHE_PATH)) {
                    if(IO.Directory.Exists(TOKEN_PATH)) {
                        if(IO.File.Exists(CACHE_PATH + ACTIVE_NAME)) {
                            IO.File.WriteAllText(CACHE_PATH + ACTIVE_NAME, (S.Convert.ToUInt64(IO.File.ReadAllText(CACHE_PATH + ACTIVE_NAME)) - 1).ToString());

                            IO.File.Delete(TOKEN_PATH + _Token);

                            _Token = null;

                            IO.File.WriteAllText(ID_PATH + _ID + SESSION_NAME, _Token.ToString());
                        }
                        else {
                            IO.File.WriteAllText(CACHE_PATH + ACTIVE_NAME, ACTIVE_INITIAL.ToString());

                            DeleteToken();
                        }
                    }
                    else {
                        IO.Directory.CreateDirectory(TOKEN_PATH);

                        DeleteToken();
                    }
                }
                else {
                    IO.Directory.CreateDirectory(CACHE_PATH);

                    DeleteToken();
                }
                return true;
            }
            else {
                return false;
            }
        }

        protected bool GenerateVisa() {
            if(ValidValues(true, null, false)) {
                _Visa = (ulong)(_Random * ulong.MaxValue) + (_Random >= HALF_RANDOM ? MAXIMUM_RANDOM : MINIMUM_RANDOM);

                return true;
            }
            else {
                return false;
            }
        }

        protected bool DeleteVisa() {
            if(ValidValues(true, null, true)) {
                _Visa = null;

                return true;
            }
            else {
                return false;
            }
        }

        protected void GenerateSalt() {
            byte[] state = new byte[SALT_LENGTH];

            random.GetBytes(state);

            _Salt = S.BitConverter.ToString(state);
        }

        protected void GenerateHash() {
            _Hash = S.BitConverter.ToString(sha256.ComputeHash(T.Encoding.UTF8.GetBytes(_Owner + _Key + _Salt)));
        }

        public void TstPnt() {
            S.Console.WriteLine(
                "\n     ID: " + id +
                "\n  Token: " + token +
                "\n   Visa: " + visa +
                "\n    PIN: " + pin +
                "\n  Owner: " + owner +
                "\n    Key: " + key +
                "\n   Salt: " + salt +
                "\n   Hash: " + hash +
                "\nInPhase: " + inphase +
                "\n   Okay: " + okay +
                "\n Active: " + active +
                "\n   Open: " + open
                );
        }

        protected void Nullify() {
            _ID = null;
            _Token = null;
            _Visa = null;

            _PIN = null;

            _Owner = null;
            _Key = null;

            _Salt = null;
            _Hash = null;

            _InPhase = null;
            _Okay = null;
            _Active = null;
            _Open = null;
        }
    }
}
