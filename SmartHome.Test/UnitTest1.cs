using Moq;
using Moq.Protected;
using SmartHome.Lib;
using static System.Net.Mime.MediaTypeNames;

namespace SmartHome.Lib.Tests
{
    public class Tests
    {
        private IConnectable device;

        [SetUp]
        public void Setup()
        {
            device = new Thermostat("00");
        }

        [Test]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(-30)]
        public void SetTargetTest(double celsius)
        {
            var device = new Thermostat("01");
            device.Connect();
            device.SetTarget(celsius);
            Assert.That(device.TargetCelsius, Is.EqualTo(celsius));
        }
        
        [Test]
        public void SetTargetTest_Combine([Values(10, 20, 30)]double celsius)
        {
            var device = new Thermostat("02");
            device.Connect();
            device.SetTarget(celsius);
            Assert.That(device.TargetCelsius, Is.EqualTo(celsius));
        }

        [Test]
        public void SetTargetTest_Range([Range(10, 21)] double celsius)
        {
            var device = new Thermostat("03");
            device.Connect();
            device.SetTarget(celsius);
            Assert.That(device.TargetCelsius, Is.EqualTo(celsius));
        }

        [Test]
        public void SetTargetTest_OutOfRangeException()
        {
            
            var device = new Thermostat("03");
            device.Connect();

            var celsius = 1000000000;
            var exception = Assert.Throws<ArgumentException>(() =>
            { device.SetTarget(celsius); }, "Value is too high");
            Assert.That(exception.Message, Is.EqualTo("Values above 100 cannot be set"));
        }

        [Test]
        public void SetTargetByMoq()
        {
            var device = new Mock<Thermostat>();
            var celsius = 21.0;

            device.Object.SetTarget(celsius);
            Assert.That(device.Object.TargetCelsius, Is.EqualTo(celsius));
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}