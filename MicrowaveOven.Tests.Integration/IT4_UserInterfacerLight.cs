﻿using System;
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
    [TestFixture]
    public class IT4_UserInterfacerLight
    {
        private IOutput _output;
        private ILight _light;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ITimer _timer;
        private ICookController _cookController;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private UserInterface _sut;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _light = new Light(_output);
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
            _sut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void TurnOn_RecieveOnDoorOpenedEvent_LightTurnsOn()
        {
            _sut.OnDoorOpened(this,EventArgs.Empty);
            _output.Received(1).OutputLine("Light is turned on");

        }

        //[Test]
        //public void TurnOff_RecieveEvent_LightTurnsOff()
        //{
        //    _sut.OnDoorClosed(this,EventArgs.Empty);
        //    _output.Received(1).OutputLine("Light is turned off");

        //}
    }

}
