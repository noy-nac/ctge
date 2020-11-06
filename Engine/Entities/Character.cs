using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine.Data;
using Engine.Events;

namespace Engine.Entities {
    public class Character : Lifeform {

        private Attributes attribute;

        private Experience experience;

        public Experience Experience {
            get { return experience; }
        }

        public Character() : base() {
            attribute = new Attributes();
            experience = new Experience();
        }

        public void Attack(Character target) {
            Battle battle = new Battle(this, target);
        }

    }
}
