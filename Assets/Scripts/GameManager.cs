using Code.Collections;
using Godot;
using Joy.Code.Managers;
using JoyGodot.Assets.Scripts.States;
using JoyLib.Code.Cultures;
using JoyLib.Code.Graphics;
using JoyLib.Code.Helpers;
using JoyLib.Code.Managers;
using JoyLib.Code.Rollers;
using JoyLib.Code.States;
using JoyLib.Code.Unity;
using JoyLib.Code.Unity.GUI;
using Thread = System.Threading.Thread;

namespace JoyLib.Code
{
    public class GameManager : Node, IGameManager
    {
        protected StateManager m_StateManager;

        protected IGameState NextState { get; set; }
        
        protected Thread LoadingThread { get; set; }

        // Use this for initialization
        public GameManager()
        {
            if (GlobalConstants.GameManager is null)
            {
                GlobalConstants.GameManager = this;
            }

            this.LoadingMessage = "Just waking up";
        }

        public override void _Ready()
        {
            base._Ready();
            this.BegunInitialisation = true;

            this.LoadingMessage = "Initialising object pools";
            Node2D worldHolder = this.GetNode<Node2D>("WorldHolder");
            Node2D floorHolder = (Node2D) worldHolder.FindNode("WorldFloors");
            Node2D wallHolder = (Node2D) worldHolder.FindNode("WorldWalls");
            Node2D objectHolder = (Node2D) worldHolder.FindNode("WorldObjects");
            Node2D entityHolder = (Node2D) worldHolder.FindNode("WorldEntities");
            Node2D fogHolder = (Node2D) worldHolder.FindNode("WorldFog");

            this.MyNode = this;

            this.LoadingThread = new Thread(this.Load);
            this.LoadingThread.Start();
        }

        // Update is called once per frame
        public override void _Process(float delta)
        {
            this.m_StateManager?.Update();
            this.ActionLog.Update();
        }

        protected void Load()
        {
            this.LoadingMessage = "Initialising action log";
            this.ActionLog = new ActionLog();
            
            this.LoadingMessage = "Revving up engines";

            this.GUIDManager = new GUIDManager();

            GlobalConstants.ActionLog = this.ActionLog;
            
            this.Roller = new RNG();

            this.ObjectIconHandler = new ObjectIconHandler(this.Roller);

            this.GUIManager = new GUIManager(this.FindNode("MainUI"));

            this.m_StateManager = new StateManager();
            this.m_StateManager.ChangeState(new LoadingState());

            this.LoadingMessage = "Initialising entity gubbinz";
            this.CultureHandler = new CultureHandler(this.ObjectIconHandler);

            this.LoadingMessage = "Done!";
            
            this.Initialised = true;
            this.LoadingThread.Abort();
        }

        public void SetNextState(IGameState nextState = null)
        {
            this.NextState = nextState;
            this.m_StateManager.ChangeState(this.NextState);
        }

        public void Reset()
        {
            this.m_StateManager.Stop();

            this.Dispose();

            this.Load();
        }
        
        public bool BegunInitialisation { get; protected set; }

        public bool Initialised { get; protected set; }

        public int LoadingPercentage { get; protected set; }

        public string LoadingMessage { get; protected set; }
        public ActionLog ActionLog { get; protected set; }
        public IObjectIconHandler ObjectIconHandler { get; protected set; }
        public ICultureHandler CultureHandler { get; protected set; }
        public IGUIManager GUIManager { get; protected set; }
        public RNG Roller { get; protected set; }
        public Node MyNode { get; protected set; }

        public GUIDManager GUIDManager { get; protected set; }
    }
}