using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MicrowaveOven.Tests.Integration
{
    [TestFixture]
    public class IT3_CookControllerPowerTubeDisplayTimer
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
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _timer = new Timer();
            _userInterface = Substitute.For<IUserInterface>();
            _sut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

       
        [Test]
        public void StartCooking_FiveSecondsInputAndWaitOneSecond_FourSecondsRemaining()
        {
            _sut.StartCooking(1, 5000);
            Thread.Sleep(1050);
            _sut.Stop();
            Assert.That(_timer.TimeRemaining, Is.EqualTo(4000));
        }
    }
}
