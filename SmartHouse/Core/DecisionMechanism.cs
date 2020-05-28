using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    public static class DecisionMechanism
    {
        static Home home = new Home();

        static Kitchen kitchen = new Kitchen();

        static Toilet toilet = new Toilet();

        static Bedroom bedroom = new Bedroom();

        public static void ExecuteRequest(EquipmentType equip,RoomTypes room,DecideType decide)
        {
            switch (equip)
            {
                case EquipmentType.sleepmode:
                    switch (decide)
                    {
                        case DecideType.On:

                            home.Door(true);

                            home.Light(false);

                            kitchen.Light(false);

                            bedroom.Light(false);

                            toilet.Light(false);

                            break;

                        case DecideType.Off:

                            home.Door(false);

                            break;
                    }
                    break;
            }
            switch(room)
            {
                case RoomTypes.kitchen:
                    switch (equip)
                    {
                        case EquipmentType.@lock:
                            switch (decide)
                            {
                                case DecideType.On:
                                    kitchen.Door(true);
                                    break;
                                case DecideType.Off:
                                    kitchen.Door(false);
                                    break;
                            }
                            break;
                        case EquipmentType.lights:
                            switch (decide)
                            {
                                case DecideType.On:
                                    kitchen.Light(true);
                                    break;
                                case DecideType.Off:
                                    kitchen.Light(false);
                                    break;
                            }
                            break;
                    }
                    break;

                case RoomTypes.bedroom:
                    switch (equip)
                    {
                        case EquipmentType.@lock:
                            switch (decide)
                            {
                                case DecideType.On:
                                    bedroom.Door(true);
                                    break;
                                case DecideType.Off:
                                    bedroom.Door(false);
                                    break;
                            }
                            break;
                        case EquipmentType.lights:
                            switch (decide)
                            {
                                case DecideType.On:
                                    bedroom.Light(true);
                                    break;
                                case DecideType.Off:
                                    bedroom.Light(false);
                                    break;
                            }
                            break;
                    }
                    break;

                case RoomTypes.toilet:
                    switch (equip)
                    {
                        case EquipmentType.@lock:
                            switch (decide)
                            {
                                case DecideType.On:
                                    toilet.Door(true);
                                    break;
                                case DecideType.Off:
                                    toilet.Door(false);
                                    break;
                            }
                            break;
                        case EquipmentType.lights:
                            switch (decide)
                            {
                                case DecideType.On:
                                    toilet.Light(true);
                                    break;
                                case DecideType.Off:
                                    toilet.Light(false);
                                    break;
                            }
                            break;
                    }
                    break;

                case RoomTypes.home:
                    switch (equip)
                    {
                        case EquipmentType.lights:
                            switch (decide)
                            {
                                case DecideType.On:

                                    home.Light(true);

                                    break;

                                case DecideType.Off:

                                    home.Light(false);

                                    break;
                            }
                            break;

                        case EquipmentType.@lock:
                            switch (decide)
                            {
                                case DecideType.On:

                                    home.Door(true);

                                    break;

                                case DecideType.Off:

                                    home.Door(false);

                                    break;
                            }
                            break;
                    }
                    break;
                case RoomTypes.all:
                    switch (equip)
                    {
                        case EquipmentType.lights:

                            switch (decide)
                            {
                                case DecideType.On:

                                    kitchen.Light(true);

                                    bedroom.Light(true);

                                    toilet.Light(true);

                                    home.Light(true);

                                    break;

                                case DecideType.Off:

                                    kitchen.Light(false);

                                    bedroom.Light(false);

                                    toilet.Light(false);

                                    home.Light(false);

                                    break;
                            }
                            break;
                    }
                    break;

            }
        }
    }
}
