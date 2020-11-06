
namespace Engine.Entity {
    public class BasalEntity {

        protected ulong id;

        public ulong ID {
            get { return id; }
            set { id = value; }
        }

        public BasalEntity(ulong id) {
            this.id = id;
        }

    }
}
