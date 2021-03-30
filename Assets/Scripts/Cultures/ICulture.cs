using System.Collections.Generic;
using Godot;
using JoyLib.Code.Rollers;

namespace JoyLib.Code.Cultures
{
    public interface ICulture
    {
        string GetRandomName(string genderRef);

        string GetNameForChain(int chain, string gender, int group);

        void ClearLastGroup();

        int GetStatVariance(string statistic);
        
        string Tileset { get; }
        
        int LastGroup { get; }
        
        string[] Inhabitants { get; }
        
        string CultureName { get; }
        
        string[] RulerTypes { get; }
        
        string[] Crimes { get; }
        
        string[] RelationshipTypes { get; }
        
        string[] RomanceTypes { get; }
        
        string[] Sexes { get; }
        
        string[] Sexualities { get; }
        
        string[] Genders { get; }
        
        string[] Jobs { get; }
        
        int NonConformingGenderChance { get; }
        
        NameData[] NameData { get; }
        
        RNG Roller { get; }
        
        IDictionary<string, IDictionary<string, Color>> CursorColours { get; }
        IDictionary<string, IDictionary<string, Color>> BackgroundColours { get; }
        
        IDictionary<string, Color> FontColours { get; }
    }
}