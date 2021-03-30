using JoyLib.Code.States;

namespace JoyLib.Code.Unity.GUI.MainMenuState
{
    public class MainMenuHandler : GUIData
    {
        public void NewGame()
        {
            GlobalConstants.GameManager.GUIManager.CloseAllGUIs();
            GlobalConstants.GameManager.SetNextState(new CharacterCreationState());
        }

        public void Quit()
        {
            this.GetTree().Quit(0);
        }
    }
}