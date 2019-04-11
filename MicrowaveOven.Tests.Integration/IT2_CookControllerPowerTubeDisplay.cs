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

namespace MicrowaveOven.Tests.Integration
{
    public class IT2_CookControllerPowerTubeDisplay
    {
        private ICookController _sut;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ITimer _timer;
        private IOutput _output;
        private IUserInterface _userInterface;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _timer = Substitute.For<ITimer>();
            _userInterface = Substitute.For<IUserInterface>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [Test]
        public void TimerTick_ThreeSecondsRemaining_OutputWritesThreeSeconds()
        {
            _sut.StartCooking(50, 115000);
            _timer.TimeRemaining.Returns(115000);
            _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            _output.Received(1).OutputLine($"Display shows: 01:55");
        }
    }
}
