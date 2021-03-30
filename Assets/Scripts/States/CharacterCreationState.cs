﻿using System.Linq;
using Godot;
using JoyGodot.Assets.Scripts.GUI.Managed_Assets;
using JoyLib.Code.Cultures;
using JoyLib.Code.Helpers;
using JoyLib.Code.Unity.GUI;

namespace JoyLib.Code.States
{
    public class CharacterCreationState : GameState
    {
        //protected CharacterCreationScreen CharacterCreationScreen { get; set; }
        
        protected Node Node { get; set; }

        public CharacterCreationState()
        {
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }

        public override void LoadContent()
        {
        }

        public override void SetUpUi()
        {
            GlobalConstants.ActionLog.Log("CHARACTER CREATION SCREEN");
            PackedScene scene = GD.Load<PackedScene>(
                GlobalConstants.GODOT_ASSETS_FOLDER +
                "Scenes/UI/Character Creation Part 1.tscn");

            this.Node = scene.Instance();
            
            GlobalConstants.GameManager.GUIManager.InstantiateUIScene(scene);
            ICulture culture = GlobalConstants.GameManager.Roller.SelectFromCollection(GlobalConstants.GameManager.CultureHandler.Values);
            base.SetUpUi();

            this.GUIManager.OpenGUI(GUINames.CHARACTER_CREATION_PART_1);

            /*
            this.CharacterCreationScreen = this.GUIManager
                .Get(GUINames.CHARACTER_CREATION_PART_1)
                .GetComponent<CharacterCreationScreen>();
            this.CharacterCreationScreen.Initialise();
            */
        }

        public override void HandleInput(InputEvent @event)
        {
        }

        public override void Update()
        {
        }

        public override GameState GetNextState()
        {
            /*
            IEntity player = this.CharacterCreationScreen.CreatePlayer();
            GlobalConstants.GameManager.Player = player;
            player.AddExperience(500);
            foreach (string jobName in player.Cultures.SelectMany(culture => culture.Jobs))
            {
                IJob job = GlobalConstants.GameManager.JobHandler.Get(jobName);
                job.AddExperience(300);
                player.AddJob(job);
            }
            
            return new WorldCreationState(player);
            */
            return null;
        }
    }
}