using System;
using Xunit;

namespace MyApp.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void CanCreateProgram()
        {
            var program = new Program();
        }

        [Fact]
        public void CanStartAndStop()
        {
            var program = new Program();
            program.Start();
            Assert.True(program.IsRunning);
            program.Stop();
            Assert.False(program.IsRunning);
        }
    }
}
