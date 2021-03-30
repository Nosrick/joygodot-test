using System.Linq;
using Godot;
using JoyGodot.Assets.Scripts.GUI.Managed_Assets;
using JoyLib.Code.Cultures;
using JoyLib.Code.Helpers;
using JoyLib.Code.Unity.GUI;
using NUnit.Framework.Internal.Execution;

namespace JoyLib.Code.States
{
    public class CharacterCreationState : GameState
    {
        //protected CharacterCreationScreen CharacterCreationScreen { get; set; }

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
            
            GlobalConstants.GameManager.GUIManager.InstantiateUIScene(
                GD.Load<PackedScene>(
                GlobalConstants.GODOT_ASSETS_FOLDER +
                "Scenes/UI/Character Creation Part 1.tscn"));
            base.SetUpUi();

            this.GUIManager.OpenGUI(GUINames.CHARACTER_CREATION_PART_1);
        }

        public override void HandleInput(InputEvent @event)
        {
            if (@event.IsActionPressed("ui_accept"))
            {
                this.Done = true;
            }
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
            return new MainMenuState();
        }
    }
}