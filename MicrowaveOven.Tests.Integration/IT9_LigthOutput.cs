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
    class IT9_LigthOutput
    {
        private IOutput _output;
        private ILight _sut;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _sut = new Light(_output);
        }

        [Test]
        public void TurnOn_LogLineOK()
        {

        }

    
    }
}
