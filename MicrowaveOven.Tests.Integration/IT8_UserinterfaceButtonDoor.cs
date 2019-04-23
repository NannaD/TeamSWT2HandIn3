using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MicrowaveOven.Tests.Integration
{
    [TestFixture]
    class IT8_UserinterfaceButtonDoor
    {
        private IUserInterface _sut;
        private CookController _cookController;
        private ILight _light;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ITimer _timer;
        private IOutput _output;
        private IButton _startButton;
        private IButton _powerButton;
        private IButton _timeButton;
        private IDoor _door;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _startButton = new Button();
            _powerButton = new Button();
            _timeButton = new Button();
            _door = new Door();
            _light = new Light(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _sut = new UserInterface(_powerButton, _timeButton, _startButton, _door, _display, _light, _cookController);
            _cookController.UI = _sut;
        }

        [Test]
        public void DoorOpen_OutputLineOK()
        {
            _door.Open();
            _output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void DoorClose_OutputLineOK()
        {
            _door.Open();
            _door.Close();
            _output.Received(1).OutputLine("Light is turned off");
        }

        [Test]
        public void PressPowerButton_OutputLineOK()
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _output.Received(1).OutputLine($"Display shows: 50 W");
        }

        [Test]
        public void PressTimeButton_OutputLineOK()
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _output.Received(1).OutputLine($"Display shows: 01:00");
        }

        [Test]
        public void PressStartCancelButton_OutputLineOK()
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startButton.Press();
            _output.Received(1).OutputLine($"PowerTube works with 50 %");
        }

        [Test]
        public void OpenDoorTwice_PowerTubeOff_OutputLineOK()
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startButton.Press();

            _door.Open();

            _output.Received(1).OutputLine($"PowerTube turned off");

        }

        [Test]
        public void PressStartCancelButtonWhileCooking_PowerTubeOff_OutputLineOK()
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startButton.Press();

            _startButton.Press();

            _output.Received(1).OutputLine($"PowerTube turned off");

        }
    }
}
