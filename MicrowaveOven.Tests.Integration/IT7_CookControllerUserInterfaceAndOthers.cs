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
   [TestFixture()]
   class IT7_CookControllerUserInterfaceAndOthers
   {
      private IUserInterface _userInterface;
      private CookController _sut; //Må jeg lave denne til ikke at være et interface? Ellers virker _sut.UI ikke
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
         _sut = new CookController(_timer, _display, _powerTube);
         _userInterface = new UserInterface(_powerButton, _timeButton, _startButton, _door, _display, _light, _sut);
      }

      //[Test]
      //public OnTimerExpired_EventRaised_CookingIsDone()
      //{
      //   _sut.UI = _userInterface;
      //}
   }
}
