using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MicrowaveOven.Tests.Integration
{
    [TestFixture]
    class IT7_CookControllerUserInterface
    {
        private IOutput _output;
        private ILight _light;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ITimer _timer;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IUserInterface _userInterface;
        private CookController _sut;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _timer = Substitute.For<ITimer>();
            _light = new Light(_output);
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            //_timer = new Timer();
            _sut = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _sut);
        }

        //[Test]
        //public void CookingIsDone_RecieveOnTimerExpired_CookingIsDoneIsCalled()
        //{

        //    _timer.Expired += Raise.EventWith(this, EventArgs.Empty);
            //_output.Received(1).OutputLine("Display cleared");
            //_userInterface.CookingIsDone();
            //_userInterface.Received(1).CookingIsDone();
        }
    }
}
