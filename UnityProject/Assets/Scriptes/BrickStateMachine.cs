using System.Collections.Generic;

public class BrickStateMachine
{
    public enum ProcessState
    {
        StateOne00 = 0,
        StateOne01,
        StateOne02,
        StateOne03,
        StateOne04,
        StateOne05,
        StateOne06,


        StateTwo0003,
        StateTwo0004,
        StateTwo0005,
        StateTwo0006,

        StateTwo0104,
        StateTwo0105,
        StateTwo0106,

        StateTwo0205,
        StateTwo0206,

        StateTwo0306,


        StateThree000306,


        InvalidTransition = 18,
    }

    public enum Command
    {
        CommandStay = 0,


        CommandOneOneDown = 1,
        CommandOneOneUp,
        CommandOneTwoDown,
        CommandOneTwoUp,


        CommandTwo0003,
        CommandTwo0004,
        CommandTwo0005,
        CommandTwo0006,

        CommandTwo0104,
        CommandTwo0105,
        CommandTwo0106,

        CommandTwo0205,
        CommandTwo0206,

        CommandTwo0306,


        CommandOneToTwo0003,
        CommandOneToTwo0004,
        CommandOneToTwo0005,
        CommandOneToTwo0006,

        CommandOneToTwo0104,
        CommandOneToTwo0105,
        CommandOneToTwo0106,

        CommandOneToTwo0205,
        CommandOneToTwo0206,

        CommandOneToTwo0306,
        

        CommandTwoToThree,


        CommandThreeToTwo0003,
        CommandThreeToTwo0004,
        CommandThreeToTwo0005,
        CommandThreeToTwo0006,

        CommandThreeToTwo0104,
        CommandThreeToTwo0105,
        CommandThreeToTwo0106,

        CommandThreeToTwo0205,
        CommandThreeToTwo0206,

        CommandThreeToTwo0306,


        CommandTwoToOne00,
        CommandTwoToOne01,
        CommandTwoToOne02,
        CommandTwoToOne03,
        CommandTwoToOne04,
        CommandTwoToOne05,
        CommandTwoToOne06,
    }

    public List<float> possibleYPos = new List<float>();

    float yPos1;
    float yPos2;
    float yPos3;

    const int ground00 = 0;
    const int ground01 = 1;
    const int ground02 = 2;
    const int ground03 = 3;
    const int ground04 = 4;
    const int ground05 = 5;
    const int ground06 = 6;
    const int ground07 = 7;
    const int ground08 = 8;

    Dictionary<StateTransition, ProcessState> transitions;
    public ProcessState CurrentState { get; private set; }

    /// <summary>
    /// Constructor
    /// When initialising from another class with the 
    /// new command, this constructor is accessed and the function
    /// the function InitStateMachine is triggered.
    /// </summary>
    public BrickStateMachine(int groundNumber, ProcessState lastState)
    {
        InitializeLists();
        InitStateMachine000102(lastState);
    }

    public void ChangeDictionary(int groundNumber, ProcessState lastState)
    {
        if (transitions.Count > 0)
            transitions.Clear();

        if (groundNumber >= ground00 && groundNumber <= ground02)
            InitStateMachine000102(lastState);
        if (groundNumber >= ground04 && groundNumber <= ground06)
            InitStateMachine040506(lastState);

        switch (groundNumber)
        {
            case ground03:
                InitStateMachine03(lastState);
                break;
            case ground07:
                InitStateMachine07(lastState);
                break;
            case ground08:
                InitStateMachine08(lastState);
                break;
        }
    }

    private void InitializeLists()
    {
        float yPos = 3.5f;
        possibleYPos.Clear();
        for (int i = 0; i < 7; i++)
        {
            yPos -= 1f;
            possibleYPos.Add(yPos);
        }
    }

    private void InitStateMachine000102(ProcessState lastState)
    {
        CurrentState = lastState;

        transitions = new Dictionary<StateTransition, ProcessState>
        {
            #region OneToOneTransition
            { new StateTransition(ProcessState.StateOne00, Command.CommandStay), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandStay), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandStay), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandStay), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandStay), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne05, Command.CommandStay), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateOne06, Command.CommandStay), ProcessState.StateOne06 },

            { new StateTransition(ProcessState.StateOne00, Command.CommandOneOneDown), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneOneDown), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneOneDown), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneOneDown), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneOneDown), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateOne05, Command.CommandOneOneDown), ProcessState.StateOne06 },

            { new StateTransition(ProcessState.StateOne00, Command.CommandOneTwoDown), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneTwoDown), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneTwoDown), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneTwoDown), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneTwoDown), ProcessState.StateOne06 },

            { new StateTransition(ProcessState.StateOne01, Command.CommandOneOneUp), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneOneUp), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneOneUp), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneOneUp), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateOne05, Command.CommandOneOneUp), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne06, Command.CommandOneOneUp), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateOne02, Command.CommandOneTwoUp), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneTwoUp), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneTwoUp), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne05, Command.CommandOneTwoUp), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateOne06, Command.CommandOneTwoUp), ProcessState.StateOne04 },
            #endregion

            #region OneToTwoTransition
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateOne04, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneToTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneToTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateOne05, Command.CommandOneToTwo0306), ProcessState.StateTwo0306 },
            #endregion

            #region TwoToTwoTransition
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandStay), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandStay), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandStay), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandStay), ProcessState.StateTwo0006 },

            { new StateTransition(ProcessState.StateTwo0104, Command.CommandStay), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandStay), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandStay), ProcessState.StateTwo0106 },

            { new StateTransition(ProcessState.StateTwo0205, Command.CommandStay), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandStay), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateTwo0306, Command.CommandStay), ProcessState.StateTwo0306 },


            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0306), ProcessState.StateTwo0306 },
            #endregion

            #region TwoToThreeTransition
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToThree), ProcessState.StateThree000306 },

            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToThree), ProcessState.StateThree000306 },

            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToThree), ProcessState.StateThree000306 },

            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            #endregion

            #region ThreeToThreeTransition
            { new StateTransition(ProcessState.StateThree000306, Command.CommandStay), ProcessState.StateThree000306 },
            #endregion

            #region ThreeToTwoTransition
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0006), ProcessState.StateTwo0006 },

            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0106), ProcessState.StateTwo0106 },

            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0306), ProcessState.StateTwo0306 },
            #endregion

            #region TwoToOneRegion
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne05), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne06), ProcessState.StateOne06 },

            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne06), ProcessState.StateOne06 },

            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne06), ProcessState.StateOne06 },


            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne06), ProcessState.StateOne06 },

            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne06), ProcessState.StateOne06 },

            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne06), ProcessState.StateOne06 },


            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne06), ProcessState.StateOne06 },

            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne06), ProcessState.StateOne06 },


            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne06), ProcessState.StateOne06 },
            #endregion
        };
    }

    private void InitStateMachine03(ProcessState lastState)
    {
        CurrentState = lastState;

        transitions = new Dictionary<StateTransition, ProcessState>
        {
            #region OneToOneTransition
            { new StateTransition(ProcessState.StateOne04, Command.CommandStay), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne05, Command.CommandStay), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateOne06, Command.CommandStay), ProcessState.StateOne06 },
            
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneOneDown), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneOneDown), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateOne05, Command.CommandOneOneDown), ProcessState.StateOne06 },
            
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneTwoDown), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneTwoDown), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneTwoDown), ProcessState.StateOne06 },
            
            { new StateTransition(ProcessState.StateOne05, Command.CommandOneOneUp), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne06, Command.CommandOneOneUp), ProcessState.StateOne05 },
            
            { new StateTransition(ProcessState.StateOne06, Command.CommandOneTwoUp), ProcessState.StateOne04 },
            #endregion

            #region OneToTwoTransition
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0206), ProcessState.StateTwo0206 },
            
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0206), ProcessState.StateTwo0206 },
            
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateOne04, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneToTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneToTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateOne05, Command.CommandOneToTwo0306), ProcessState.StateTwo0306 },
            #endregion

            #region TwoToTwoTransition
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandStay), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandStay), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandStay), ProcessState.StateTwo0006 },

            { new StateTransition(ProcessState.StateTwo0104, Command.CommandStay), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandStay), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandStay), ProcessState.StateTwo0106 },

            { new StateTransition(ProcessState.StateTwo0205, Command.CommandStay), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandStay), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateTwo0306, Command.CommandStay), ProcessState.StateTwo0306 },


            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0306), ProcessState.StateTwo0306 },
            
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0006), ProcessState.StateTwo0006 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0306), ProcessState.StateTwo0306 },

            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0106), ProcessState.StateTwo0106 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0206), ProcessState.StateTwo0206 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0306), ProcessState.StateTwo0306 },
            #endregion

            #region TwoToThreeTransition
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToThree), ProcessState.StateThree000306 },

            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToThree), ProcessState.StateThree000306 },

            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToThree), ProcessState.StateThree000306 },

            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToThree), ProcessState.StateThree000306 },
            #endregion

            #region ThreeToThreeTransition
            { new StateTransition(ProcessState.StateThree000306, Command.CommandStay), ProcessState.StateThree000306 },
            #endregion

            #region ThreeToTwoTransition
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0006), ProcessState.StateTwo0006 },

            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0106), ProcessState.StateTwo0106 },

            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0205), ProcessState.StateTwo0205 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0206), ProcessState.StateTwo0206 },

            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0306), ProcessState.StateTwo0306 },
            #endregion

            #region TwoToOneRegion
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne06), ProcessState.StateOne06 },
            
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne06), ProcessState.StateOne06 },
            
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne06), ProcessState.StateOne06 },

            
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne06), ProcessState.StateOne06 },
            
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne06), ProcessState.StateOne06 },
            
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne06), ProcessState.StateOne06 },

            
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne06), ProcessState.StateOne06 },
            
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne06), ProcessState.StateOne06 },

            
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne06), ProcessState.StateOne06 },
            #endregion
        };
    }

    private void InitStateMachine040506(ProcessState lastState)
    {
        CurrentState = lastState;

        transitions = new Dictionary<StateTransition, ProcessState>
        {
            #region OneToOneTransition
            { new StateTransition(ProcessState.StateOne04, Command.CommandStay), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne05, Command.CommandStay), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateOne03, Command.CommandOneOneDown), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneOneDown), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateOne02, Command.CommandOneTwoDown), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneTwoDown), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateOne05, Command.CommandOneOneUp), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateOne06, Command.CommandOneOneUp), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateOne06, Command.CommandOneTwoUp), ProcessState.StateOne04 },
            #endregion

            #region OneToTwoTransition
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne00, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateOne04, Command.CommandOneToTwo0205), ProcessState.StateTwo0205 },
            #endregion

            #region TwoToTwoTransition
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandStay), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandStay), ProcessState.StateTwo0005 },

            { new StateTransition(ProcessState.StateTwo0104, Command.CommandStay), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandStay), ProcessState.StateTwo0105 },

            { new StateTransition(ProcessState.StateTwo0205, Command.CommandStay), ProcessState.StateTwo0205 },


            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0105), ProcessState.StateTwo0105 },

            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0005), ProcessState.StateTwo0005 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0205), ProcessState.StateTwo0205 },

            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0105), ProcessState.StateTwo0105 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0205), ProcessState.StateTwo0205 },
            #endregion

            #region ThreeToTwoTransition
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0005), ProcessState.StateTwo0005 },

            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0104), ProcessState.StateTwo0104 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0105), ProcessState.StateTwo0105 },

            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0205), ProcessState.StateTwo0205 },
            #endregion

            #region TwoToOneRegion
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne05), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne05), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne05), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne05), ProcessState.StateOne05 },


            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne05), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne05), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne05), ProcessState.StateOne05 },


            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne05), ProcessState.StateOne05 },

            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne04), ProcessState.StateOne04 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne05), ProcessState.StateOne05 },


            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne05), ProcessState.StateOne05 },
            #endregion
        };
    }

    private void InitStateMachine07(ProcessState lastState)
    {
        CurrentState = lastState;

        transitions = new Dictionary<StateTransition, ProcessState>
        {
            #region OneToOneTransition
            { new StateTransition(ProcessState.StateOne00, Command.CommandStay), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandStay), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandStay), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandStay), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandStay), ProcessState.StateOne04 },

            { new StateTransition(ProcessState.StateOne00, Command.CommandOneOneDown), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneOneDown), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneOneDown), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneOneDown), ProcessState.StateOne04 },

            { new StateTransition(ProcessState.StateOne00, Command.CommandOneTwoDown), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneTwoDown), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneTwoDown), ProcessState.StateOne04 },

            { new StateTransition(ProcessState.StateOne01, Command.CommandOneOneUp), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneOneUp), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneOneUp), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneOneUp), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateOne05, Command.CommandOneOneUp), ProcessState.StateOne04 },

            { new StateTransition(ProcessState.StateOne02, Command.CommandOneTwoUp), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneTwoUp), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneTwoUp), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne05, Command.CommandOneTwoUp), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateOne06, Command.CommandOneTwoUp), ProcessState.StateOne04 },
            #endregion

            #region OneToTwoTransition
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneToTwo0003), ProcessState.StateTwo0003 },

            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneToTwo0104), ProcessState.StateTwo0104 },

            { new StateTransition(ProcessState.StateOne03, Command.CommandOneToTwo0104), ProcessState.StateTwo0104 },
            #endregion

            #region TwoToTwoTransition
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandStay), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandStay), ProcessState.StateTwo0004 },

            { new StateTransition(ProcessState.StateTwo0104, Command.CommandStay), ProcessState.StateTwo0104 },


            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwo0104), ProcessState.StateTwo0104 },

            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwo0104), ProcessState.StateTwo0104 },

            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwo0104), ProcessState.StateTwo0104 },

            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwo0104), ProcessState.StateTwo0104 },

            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwo0004), ProcessState.StateTwo0004 },

            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwo0104), ProcessState.StateTwo0104 },

            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwo0104), ProcessState.StateTwo0104 },

            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0004), ProcessState.StateTwo0004 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwo0104), ProcessState.StateTwo0104 },

            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwo0104), ProcessState.StateTwo0104 },
            #endregion
            
            #region ThreeToTwoTransition
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0003), ProcessState.StateTwo0003 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0004), ProcessState.StateTwo0004 },

            { new StateTransition(ProcessState.StateThree000306, Command.CommandThreeToTwo0104), ProcessState.StateTwo0104 },
            #endregion

            #region TwoToOneRegion
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne04), ProcessState.StateOne04 },

            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne04), ProcessState.StateOne04 },

            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne04), ProcessState.StateOne04 },

            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne04), ProcessState.StateOne04 },


            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne04), ProcessState.StateOne04 },

            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne04), ProcessState.StateOne04 },

            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne04), ProcessState.StateOne04 },


            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne04), ProcessState.StateOne04 },

            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne04), ProcessState.StateOne04 },


            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne03), ProcessState.StateOne03 },
            #endregion
        };
    }

    private void InitStateMachine08(ProcessState lastState)
    {
        CurrentState = lastState;

        transitions = new Dictionary<StateTransition, ProcessState>
        {
            #region OneToOneTransition
            { new StateTransition(ProcessState.StateOne00, Command.CommandStay), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandStay), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandStay), ProcessState.StateOne02 },

            { new StateTransition(ProcessState.StateOne00, Command.CommandOneOneDown), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne01, Command.CommandOneOneDown), ProcessState.StateOne02 },

            { new StateTransition(ProcessState.StateOne00, Command.CommandOneTwoDown), ProcessState.StateOne02 },

            { new StateTransition(ProcessState.StateOne01, Command.CommandOneOneUp), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateOne02, Command.CommandOneOneUp), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneOneUp), ProcessState.StateOne02 },

            { new StateTransition(ProcessState.StateOne02, Command.CommandOneTwoUp), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateOne03, Command.CommandOneTwoUp), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateOne04, Command.CommandOneTwoUp), ProcessState.StateOne02 },


            { new StateTransition(ProcessState.StateOne05, Command.CommandOneTwoUp), ProcessState.StateOne02 },
            { new StateTransition(ProcessState.StateOne06, Command.CommandOneTwoUp), ProcessState.StateOne02 },
            #endregion

            #region ThreeToOneTransition
            { new StateTransition(ProcessState.StateThree000306, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateThree000306, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            #endregion

            #region TwoToOneRegion
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0003, Command.CommandTwoToOne02), ProcessState.StateOne02 },

            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0004, Command.CommandTwoToOne02), ProcessState.StateOne02 },

            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0005, Command.CommandTwoToOne02), ProcessState.StateOne02 },

            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0006, Command.CommandTwoToOne02), ProcessState.StateOne02 },


            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0104, Command.CommandTwoToOne02), ProcessState.StateOne02 },

            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0105, Command.CommandTwoToOne02), ProcessState.StateOne02 },

            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0106, Command.CommandTwoToOne02), ProcessState.StateOne02 },


            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0205, Command.CommandTwoToOne02), ProcessState.StateOne02 },

            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne00), ProcessState.StateOne00 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0206, Command.CommandTwoToOne02), ProcessState.StateOne02 },


            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne01), ProcessState.StateOne01 },
            { new StateTransition(ProcessState.StateTwo0306, Command.CommandTwoToOne02), ProcessState.StateOne02 },
            #endregion
        };
    }

    public string invalid;
    private ProcessState GetNext(Command command)
    {
        StateTransition transition = new StateTransition(CurrentState, command);
        ProcessState nextState = ProcessState.InvalidTransition;

        if (!transitions.TryGetValue(transition, out nextState))
            throw new System.Exception("Invalid transition: " + CurrentState + " -> " + command);

        return nextState;
    }

    public ProcessState MoveNext(Command randomCommand)
    {
        Command command = (Command)randomCommand;

        CurrentState = GetNext(command);
        //Debug.Log(CurrentState);
        return CurrentState;
    }

    const float NOYPOS = 8;
    public float[] GetYPosFromState(ProcessState actualState)
    {
        switch (actualState)
        {
            case ProcessState.StateOne00:
                yPos1 = possibleYPos[0];
                yPos2 = NOYPOS;
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateOne01:
                yPos1 = possibleYPos[1];
                yPos2 = NOYPOS;
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateOne02:
                yPos1 = possibleYPos[2];
                yPos2 = NOYPOS;
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateOne03:
                yPos1 = possibleYPos[3];
                yPos2 = NOYPOS;
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateOne04:
                yPos1 = possibleYPos[4];
                yPos2 = NOYPOS;
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateOne05:
                yPos1 = possibleYPos[5];
                yPos2 = NOYPOS;
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateOne06:
                yPos1 = possibleYPos[6];
                yPos2 = NOYPOS;
                yPos3 = NOYPOS;
                break;

            case ProcessState.StateTwo0003:
                yPos1 = possibleYPos[0];
                yPos2 = possibleYPos[3];
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateTwo0004:
                yPos1 = possibleYPos[0];
                yPos2 = possibleYPos[4];
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateTwo0005:
                yPos1 = possibleYPos[0];
                yPos2 = possibleYPos[5];
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateTwo0006:
                yPos1 = possibleYPos[0];
                yPos2 = possibleYPos[6];
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateTwo0104:
                yPos1 = possibleYPos[1];
                yPos2 = possibleYPos[4];
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateTwo0105:
                yPos1 = possibleYPos[1];
                yPos2 = possibleYPos[5];
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateTwo0106:
                yPos1 = possibleYPos[1];
                yPos2 = possibleYPos[6];
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateTwo0205:
                yPos1 = possibleYPos[2];
                yPos2 = possibleYPos[5];
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateTwo0206:
                yPos1 = possibleYPos[2];
                yPos2 = possibleYPos[6];
                yPos3 = NOYPOS;
                break;
            case ProcessState.StateTwo0306:
                yPos1 = possibleYPos[3];
                yPos2 = possibleYPos[6];
                yPos3 = NOYPOS;
                break;

            case ProcessState.StateThree000306:
                yPos1 = possibleYPos[0];
                yPos2 = possibleYPos[3];
                yPos3 = possibleYPos[6];
                break;


            case ProcessState.InvalidTransition:
                yPos1 = NOYPOS;
                yPos2 = NOYPOS;
                yPos3 = NOYPOS;
                break;
        }
        float[] allYPos = new float[3];
        allYPos[0] = yPos1;
        allYPos[1] = yPos2;
        allYPos[2] = yPos3;

        //Debug.Log(CurrentState + ": " + yPos1 + ", " + yPos2 + ", " + yPos3);

        return allYPos;
    }


    private class StateTransition
    {
        readonly ProcessState CurrentState;
        readonly Command Command;

        public StateTransition(ProcessState currentState, Command command)
        {
            CurrentState = currentState;
            Command = command;
        }

        public override int GetHashCode()       // Identification number
        {
            return 17 + 32 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
        }

        public override bool Equals(object obj)             // Comparison of objects
        {
            StateTransition other = obj as StateTransition;
            return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
        }
    }
}