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
   [TestFixture]
   public class IT6_UserInterfaceCookControllerAndOthers
   {
      private ICookController _cookController;
      private IUserInterface _sut;
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
         _startButton = Substitute.For<IButton>();
         _powerButton = Substitute.For<IButton>();
         _timeButton = Substitute.For<IButton>();
         _door = Substitute.For<IDoor>();
         _light = new Light(_output);
         _timer = new Timer();
         _powerTube = new PowerTube(_output);
         _display = new Display(_output);
         _cookController = new CookController(_timer, _display, _powerTube);
         _sut = new UserInterface(_powerButton, _timeButton, _startButton, _door, _display, _light, _cookController);

      }

        //Denne tester ikke det rigtige
        [Test]
        public void OnDoorOpened_EventIsRaised_OutputIsCalled()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received(1).OutputLine("Light is turned on");
        }

        //Tror denne test er fin, men måske er der fundet en fejl som er grunden til at den fejler. 
        [Test]
        public void OnStartCancelPressed_EventRaised_OutputViaPowerTube()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received(6).OutputLine(Arg.Any<string>());
        }

        [Test]
      public void OnDoorOpen_EventRaised_CookingStops()
      {
         _door.Opened += Raise.EventWith(this, EventArgs.Empty);
         _door.Closed += Raise.EventWith(this, EventArgs.Empty);
         _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
         _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
         _startButton.Pressed += Raise.EventWith(this, EventArgs.Empty);   

         _door.Opened += Raise.EventWith(this, EventArgs.Empty);
         _output.Received(1).OutputLine($"PowerTube turned off"); 
      }

      [Test]
      public void OnStartCancelPressed_EventRaised_CookingStops()
      {
         _door.Opened += Raise.EventWith(this, EventArgs.Empty);
         _door.Closed += Raise.EventWith(this, EventArgs.Empty);
         _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
         _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
         _startButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

         _startButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
         _output.Received(1).OutputLine($"PowerTube turned off");
      }
   }
}
