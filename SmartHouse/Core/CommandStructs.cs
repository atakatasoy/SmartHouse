using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    public static class CommandStructs
    {
        private static string Merged { get; set; } = String.Empty;

        private static List<string> Splitted = new List<string>();

        public static int NumberOfWords;

        public static void CreateCommandStructs(ReadOnlyCollection<RecognizedWordUnit> words)
        {
            
            Splitted= words.Select(word => word.Text).ToList();

            NumberOfWords = Splitted.Count;

            foreach(string each in Splitted)
            {
                Merged += each + " ";
            }

            AnalyzeRequest();
        }

        private static bool RemoveAndDecrease(this List<string> list,string item)
        {

            if (list.Remove(item))
            {
                NumberOfWords--;

                return true;
            }
            else return false;
            
        }

        private static string GetMergedWords()
        {
            return Merged;
        }

        private static DecideType StateDecide()
        {
            foreach(string each in Splitted)
            {
                if (each == "on" || each == "open")
                {
                    Splitted.RemoveAndDecrease(nameof(DecideType.On));
                    return DecideType.On;
                }
                else if (each == "off" || each == "close")
                {
                    Splitted.RemoveAndDecrease(nameof(DecideType.Off));
                    return DecideType.Off;
                }
            }
            return DecideType.Nothing;
        }

        private static EquipmentType WhatToDo()
        {
            foreach(string each in Splitted)
            {
                switch (each)
                {
                    case nameof(EquipmentType.@lock):
                        Splitted.RemoveAndDecrease(nameof(EquipmentType.@lock));
                        return EquipmentType.@lock;

                    case nameof(EquipmentType.lights):
                        Splitted.RemoveAndDecrease(nameof(EquipmentType.lights));
                        return EquipmentType.lights;

                    case nameof(EquipmentType.sleepmode):
                        Splitted.RemoveAndDecrease(nameof(EquipmentType.sleepmode));
                        return EquipmentType.sleepmode;
                }
            }
            return EquipmentType.none;
        }

        private static RoomTypes WhichRoom()
        {
            foreach(string each in Splitted)
            {
                switch (each)
                {
                    case nameof(RoomTypes.bedroom):
                        Splitted.RemoveAndDecrease(nameof(RoomTypes.bedroom));
                        return RoomTypes.bedroom;
                        
                    case nameof(RoomTypes.kitchen):
                        Splitted.RemoveAndDecrease(nameof(RoomTypes.kitchen));
                        return RoomTypes.kitchen;

                    case nameof(RoomTypes.toilet):
                        Splitted.RemoveAndDecrease(nameof(RoomTypes.toilet));
                        return RoomTypes.toilet;

                    case nameof(RoomTypes.home):
                        Splitted.RemoveAndDecrease(nameof(RoomTypes.home));
                        return RoomTypes.home;

                    case nameof(RoomTypes.all):
                        Splitted.RemoveAndDecrease(nameof(RoomTypes.all));
                        return RoomTypes.all;
                }
            }
            return RoomTypes.none;
            
        }

        private static RoomTypes WhichRoom(string text)
        {
            switch (text)
            {
                case nameof(RoomTypes.bedroom):
                    Splitted.RemoveAndDecrease(nameof(RoomTypes.bedroom));
                    return RoomTypes.bedroom;

                case nameof(RoomTypes.kitchen):
                    Splitted.RemoveAndDecrease(nameof(RoomTypes.kitchen));
                    return RoomTypes.kitchen;

                case nameof(RoomTypes.toilet):
                    Splitted.RemoveAndDecrease(nameof(RoomTypes.toilet));
                    return RoomTypes.toilet;

                case nameof(RoomTypes.home):
                    Splitted.RemoveAndDecrease(nameof(RoomTypes.home));
                    return RoomTypes.home;

                case nameof(RoomTypes.all):
                    Splitted.RemoveAndDecrease(nameof(RoomTypes.all));
                    return RoomTypes.all;
            }
            return RoomTypes.none;

        }

        public static EquipmentType WhatToDo(string text)
        {
            switch (text)
            {
                case nameof(EquipmentType.@lock):
                    Splitted.RemoveAndDecrease(nameof(EquipmentType.@lock));
                    return EquipmentType.@lock;

                case nameof(EquipmentType.lights):
                    Splitted.RemoveAndDecrease(nameof(EquipmentType.lights));
                    return EquipmentType.lights;

                case nameof(EquipmentType.sleepmode):
                    Splitted.RemoveAndDecrease(nameof(EquipmentType.sleepmode));
                    return EquipmentType.sleepmode;
            }
            return EquipmentType.none;
        }

        /*public static (EquipmentType equip,RoomTypes room) CheckForMultiple()
        {
            if (NumberOfWords == 2)
            {
                if (Splitted[0] == "and")
                {
                    Splitted.RemoveAndDecrease("and");
                    try
                    {

                        RoomTypes second = WhichRoom(Splitted[0]);

                        EquipmentType second1 = WhatToDo(Splitted[0]);

                        if (second != RoomTypes.none || second1 != EquipmentType.none)
                        {
                            return (second1, second);
                        }
                        else return (EquipmentType.none, RoomTypes.none);

                    }
                    catch
                    {
                        return (EquipmentType.none, RoomTypes.none);
                    }
                }

            }
            return (EquipmentType.none, RoomTypes.none);
        }
        */

        public static void AnalyzeRequest()
        {
            EquipmentType equipment = WhatToDo();

            RoomTypes room = WhichRoom();

            DecideType decide = StateDecide();

            //var Result = CheckForMultiple();

            DecisionMechanism.ExecuteRequest(equipment,room,decide);

            /*if (Result.equip != EquipmentType.none)
            {
                equipment = Result.equip;

                DecisionMechanism.ExecuteRequest(equipment, room, decide);
            }
            else if (Result.room != RoomTypes.none)
            {
                room = Result.room;

                DecisionMechanism.ExecuteRequest(equipment, room, decide);
            }
            */
        }

    }
}
