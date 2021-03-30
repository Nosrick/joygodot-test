using System.Linq;
using Godot;
using JoyLib.Code.Cultures;
using JoyLib.Code.Unity.GUI;

namespace JoyLib.Code.States
{
    class MainMenuState : GameState
    {
        protected GameState m_NextState;

        public MainMenuState() :
            base()
        {
        }

        public override void LoadContent()
        {
        }

        public override void SetUpUi()
        {
            GlobalConstants.GameManager.GUIManager.InstantiateUIScene(
                GD.Load<PackedScene>(
                    GlobalConstants.GODOT_ASSETS_FOLDER +
                    "Scenes/UI/MainMenu.tscn"));
            
            base.SetUpUi();
            this.GUIManager.OpenGUI(GUINames.MAIN_MENU);
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }

        public override void Update()
        {
        }

        public override void HandleInput(InputEvent @event)
        {
        }

        public void NewGame()
        {
            this.Done = true;
            this.m_NextState = new CharacterCreationState();
        }

        public void ContinueGame()
        {
        }

        public override GameState GetNextState()
        {
            return this.m_NextState;
        }
    }
}