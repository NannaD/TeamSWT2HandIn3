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
      private IButton _button;
      private IDoor _door;
      public void SetUp()
      {
         _output = Substitute.For<IOutput>();
         _button = Substitute.For<IButton>();
         _door = Substitute.For<IDoor>();
         _light = new Light(_output);
         _timer = new Timer();
         _powerTube = new PowerTube(_output);
         _display = new Display(_output);
         _sut=new UserInterface(_button,_button,_button,_door,_display,_light,_cookController);
         _cookController = new CookController(_timer, _display, _powerTube, _sut);
      }


   }
}
