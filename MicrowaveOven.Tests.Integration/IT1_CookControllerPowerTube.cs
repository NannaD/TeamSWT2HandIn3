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
    public class IT1_CookControllerPowerTube
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
            _display = Substitute.For<IDisplay>();
            _timer = Substitute.For<ITimer>();
            _userInterface = Substitute.For<IUserInterface>();
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [TestCase(1, 10)]
        [TestCase(50, 10)]
        [TestCase(100, 10)]
        public void StartCooking_PowerOK_OutputWritesSpecifiedPower(int _power, int _time)
        {
            _sut.StartCooking(_power, _time);
            _output.Received(1).OutputLine($"PowerTube works with {_power} %");
        }
        // Vi har testet at der er forbindelse mellem CookController til power. Hvis power er 0, så får man fejlbesked om at power skal være mellem 0 og 100. 

    }
}
