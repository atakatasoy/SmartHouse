using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    public abstract class BaseRoom<Parent>
    {
        private delegate void Changed(string name,bool state);

        private event Changed ValueChanged;

        private bool IsDoorClosed { get; set; } = true;

        private bool mLights = false;

        private bool mDoorLock = false;

        private bool Lights
        {
            get
            {
                return mLights;
            }
            set
            {
                if (mLights != value) ValueChanged(nameof(Lights), value);

                mLights = value;
                
            }
        }

        private bool Lock
        {
            get
            {
                return mDoorLock;
            }
            set
            {
                if (mDoorLock != value) ValueChanged(nameof(Lock), value);

                mDoorLock = value;
                
            }
        }

        public BaseRoom()
        {
            Changed Forward_ValueChangedDelegate = new Changed(Forward_ValueChanged);

            ValueChanged += Forward_ValueChangedDelegate;
        }

        public void Forward_ValueChanged(string name,bool state)
        {
            switch (name)
            {
                case nameof(Lock):
                    //Let the Microprocesser know the change
                    Console.WriteLine("Microprocesser started the "+ (state ? "On":"Off")+" process of "+ nameof(Lock)+" at "+typeof(Parent));
                    break;
                case nameof(Lights):
                    Console.WriteLine("Microprocesser started the " + (state ? "On":"Off") + " process of "+nameof(Lights)+" at "+typeof(Parent));
                    break;
            }
        }
        public void Light(bool decide)
        {
            Lights = decide;
        }
        public void Door(bool decide)
        {
            if (IsDoorClosed) Lock = decide;

            else Console.WriteLine("I cant lock the door while it's open !");
        }
    }
}
