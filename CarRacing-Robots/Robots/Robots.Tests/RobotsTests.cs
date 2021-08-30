namespace Robots.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class RobotsTests
    {
        private Robot robot;
        private const string RobotName = "R2D2";
        private const int MaxBattery = 1000;
        private RobotManager manager;
        private const int Capacity = 2;
        private const string Job = "Paint";
        [SetUp]
        public void SetUp()
        {
            robot = new Robot(RobotName, MaxBattery);
            manager = new RobotManager(Capacity);
        }
        [Test]
        public void Constructor_ShouldInitializePrivateListOfRobots()
        {
            var type = manager.GetType();
            var robots = type.GetField("robots", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(robots);
        }
        [Test]
        public void Capacity_SetNegativeValue_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new RobotManager(-1));
            StringAssert.Contains("Invalid capacity", ex.Message);
        }
        [Test]
        public void Capacity_Get_ShouldReturnTheCapacity()
        {
            Assert.AreEqual(Capacity, manager.Capacity);
        }
        [Test]
        public void Count_ShouldReturnCorrectCountOfManagerList()
        {
            manager.Add(robot);
            Assert.AreEqual(1, manager.Count);
        }
        [Test]
        public void Add_AlreadyAddedRobotName_ShouldThrowInvalidOperationException()
        {
            manager.Add(robot);
            var ex = Assert.Throws<InvalidOperationException>(() => manager.Add(robot));
            StringAssert.Contains($"There is already a robot with name {RobotName}", ex.Message);
        }
        [Test]
        public void Add_IfCapacityIsReached_ShouldThrowInvalidOperationException()
        {
            manager.Add(robot);
            manager.Add(new Robot("QWERTY", 2000));
            var ex = Assert.Throws<InvalidOperationException>(() => manager.Add(new Robot("ASD", 1400)));
            StringAssert.Contains("Not enough capacity", ex.Message);
        }
        [Test]
        public void Add_NewRobotAndEnoughCapacity_ShouldAddRobotToTheManager()
        {
            manager.Add(robot);

            Type type = manager.GetType();
            FieldInfo robots = type.GetField("robots", BindingFlags.NonPublic | BindingFlags.Instance);
            var expected = new List<Robot>();
            expected.Add(robot);
            var actual = robots.GetValue(manager);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Remove_NotExistingRobotName_ShouldThrowInvalidOperationException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => manager.Remove(RobotName));
            StringAssert.Contains($"Robot with the name {RobotName} doesn't exist", ex.Message);
        }
        [Test]
        public void Remove_ExistingRobotInTheManager_RemovesTheRobot()
        {
            manager.Add(robot);
            manager.Remove(RobotName);
            Assert.AreEqual(0, manager.Count);
        }
        [Test]
        public void Work_NotExistingRobot_ShouldThrowInvalidOperationException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => manager.Work(RobotName, Job, 100));
            StringAssert.Contains($"Robot with the name {RobotName} doesn't exist", ex.Message);
        }
        [Test]
        public void Work_RobotBatteryLessThanNeededForTheJob_ShouldThrowInvalidOperationException()
        {
            manager.Add(robot);
            var ex = Assert.Throws<InvalidOperationException>(() => manager.Work(RobotName, Job, 1001));
            StringAssert.Contains($"{RobotName} doesn't have enough battery", ex.Message);
        }
        [Test]
        public void Work_ExistingRobotWithEnoughBattery_ShouldReduceRobotBatteryByBatteryUsageForTheJob()
        {
            manager.Add(robot);
            manager.Work(RobotName, Job, 100);
            Assert.AreEqual(900, robot.Battery);
        }
        [Test]
        public void Charge_NotExistingRobot_ShouldThrowInvalidOperationException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => manager.Charge(RobotName));
            StringAssert.Contains($"Robot with the name {RobotName} doesn't exist", ex.Message);
        }
        [Test]
        public void Charge_ExistingRobot_ShouldSetRobotBatteryToMaximumBattery()
        {
            manager.Add(robot);
            manager.Work(RobotName, Job, 500);
            manager.Charge(RobotName);
            Assert.AreEqual(1000, robot.Battery);
        }
    }
}
