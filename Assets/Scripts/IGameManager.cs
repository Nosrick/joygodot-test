using System;
using Code.Collections;
using Godot;
using JoyLib.Code.Cultures;
using JoyLib.Code.Graphics;
using JoyLib.Code.Helpers;
using JoyLib.Code.Managers;
using JoyLib.Code.Rollers;
using JoyLib.Code.States;
using JoyLib.Code.Unity.GUI;

namespace JoyLib.Code
{
    public interface IGameManager : IDisposable
    {
        bool Initialised { get; }
        int LoadingPercentage { get; }
        
        string LoadingMessage { get; }
        
        //CheatInterface Cheats { get; set; }
        
        ActionLog ActionLog { get; }
        IObjectIconHandler ObjectIconHandler { get; }
        ICultureHandler CultureHandler { get; }
        IGUIManager GUIManager { get; }
        
        GUIDManager GUIDManager { get; }
        
        RNG Roller { get; }
        
        Node MyNode { get; }
        void SetNextState(IGameState nextState = null);

        void Reset();
    }
}