using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SF2022UserKonstantinovAndrey;

namespace UnitTestLibrary
{
    [TestClass]
    public class UnitTestLib
    {
        Calculations calculations = new Calculations();

        [TestMethod]
        // сравнение коллекций
        public void TestMethod1_OutCorrectStringArray()
        {
            // arange настройка
            TimeSpan beginWorkingTime = new TimeSpan(8, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(18, 00, 00);

            int consultationTime = 30;
            int[] durations = { 60, 30, 10, 10, 40 };

            TimeSpan test0 = new TimeSpan(10, 00, 00);
            TimeSpan test1 = new TimeSpan(11, 00, 00);
            TimeSpan test2 = new TimeSpan(15, 00, 00);
            TimeSpan test3 = new TimeSpan(15, 30, 00);
            TimeSpan test4 = new TimeSpan(16, 50, 00);
            TimeSpan[] startTimes = { test0, test1, test2, test3, test4 };

            // act выполнение
            string[] expectedArrayString = {"08:00-08:30","08:30-09:00","09:00-09:30","09:30-10:00","11:30-12:00", "12:00-12:30",
                "12:30-13:00", "13:00-13:30","13:30-14:00","14:00-14:30","14:30-15:00","15:10-15:30","15:40-16:10","16:10-16:40",
            "16:40-16:50","17:30-18:00"};

            string[] actualArrayString = calculations.AvailablePeriod(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            // assert проверка результата
            CollectionAssert.AreEqual(expectedArrayString, actualArrayString);
        }
        [TestMethod]
        //сравнение 0 и 1 элемента в получаемом массиве
        public void TestMethod2_expected0930and1000_actual()
        {
            // arange настройка
            TimeSpan beginWorkingTime = new TimeSpan(8, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(18, 00, 00);

            int consultationTime = 30;
            int[] durations = { 60, 30, 10, 10, 40 };

            TimeSpan test0 = new TimeSpan(08, 30, 00);
            TimeSpan test1 = new TimeSpan(11, 00, 00);
            TimeSpan test2 = new TimeSpan(15, 00, 00);
            TimeSpan test3 = new TimeSpan(15, 30, 00);
            TimeSpan test4 = new TimeSpan(16, 50, 00);
            TimeSpan[] startTimes = { test0, test1, test2, test3, test4 };

            // act выполнение
            string expectedArrayString0 = "08:00-08:30";
            string expectedArrayString1 = "09:30-10:00";

            string[] actualArrayString = calculations.AvailablePeriod(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            // assert проверка результата
            Assert.AreEqual(expectedArrayString0, actualArrayString[0]);
            Assert.AreEqual(expectedArrayString1, actualArrayString[1]);
        }
        [TestMethod]
        // сравнение 14 и 15 элемента в получаемом массиве, изменяя startTimes
        public void TestMethod3_1700and1740_durations10and20()
        {
            // arange настройка
            TimeSpan beginWorkingTime = new TimeSpan(8, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(18, 00, 00);

            int consultationTime = 30;
            int[] durations = { 60, 30, 10, 10, 20 };

            TimeSpan test0 = new TimeSpan(08, 30, 00);
            TimeSpan test1 = new TimeSpan(11, 00, 00);
            TimeSpan test2 = new TimeSpan(15, 00, 00);
            TimeSpan test3 = new TimeSpan(17, 00, 00);
            TimeSpan test4 = new TimeSpan(17, 40, 00);
            TimeSpan[] startTimes = { test0, test1, test2, test3, test4 };

            // act выполнение
            string expectedArrayString14 = "16:40-17:00";
            string expectedArrayString15 = "17:10-17:40";

            string[] actualArrayString = calculations.AvailablePeriod(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            // assert проверка результата
            Assert.AreEqual(expectedArrayString14, actualArrayString[14]);
            Assert.AreEqual(expectedArrayString15, actualArrayString[15]);
        }

        [TestMethod]
        // сравнение коллекции с одним промежутком в 80 минут
        public void TestMethod4_duration80()
        {
            // arange настройка
            TimeSpan beginWorkingTime = new TimeSpan(8, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(20, 00, 00);

            int consultationTime = 30;
            int[] durations = { 80 };

            TimeSpan test = new TimeSpan(17, 40, 00);

            TimeSpan[] startTimes = { test };

            // act выполнение
            string expectedArrayString19 = "17:30-17:40";
            string expectedArrayString20 = "19:00-19:30";

            string[] actualArrayString = calculations.AvailablePeriod(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            // assert проверка результата
            Assert.AreEqual(expectedArrayString19, actualArrayString[19]);
            Assert.AreEqual(expectedArrayString20, actualArrayString[20]);
        }

        [TestMethod]
        // сравнение пустой коллекции до 16:00 по количеству элементов
        public void TestMethod5_EmptyDurations_and_CheckCount_ToEndWorkingTime_16hours()
        {
            // arange настройка
            TimeSpan beginWorkingTime = new TimeSpan(8, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(16, 00, 00);

            int consultationTime = 30;
            int[] durations = { };

            TimeSpan[] startTimes = { };

            // act выполнение
            int expectedCount = (int)endWorkingTime.TotalHours;
            string[] actualArrayString = calculations.AvailablePeriod(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            // assert проверка результата
            Assert.AreEqual(expectedCount, actualArrayString.Length);

        }

        [TestMethod]
        // сравнение количества элементов той же коллекции только уже с промежутком в 10 минут
        public void TestMethod6_consultationTime10_ToEndWorkingTime_16hours_expectedCount()
        {
            // arange настройка
            TimeSpan beginWorkingTime = new TimeSpan(8, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(16, 00, 00);

            int consultationTime = 10;
            int[] durations = { };

            TimeSpan[] startTimes = { };

            // act выполнение
            int expectedCount = (int)endWorkingTime.TotalHours * 3;
            string[] actualArrayString = calculations.AvailablePeriod(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            // assert проверка результата
            Assert.AreEqual(expectedCount, actualArrayString.Length);

        }

        [TestMethod]
        // сравнение количества элементов предыдущей коллекции, но если добавить занятый промежуток в 100 минут
        public void TestMethod7_checkCount_expected48_substract10_actually38()
        {
            // arange настройка
            TimeSpan beginWorkingTime = new TimeSpan(8, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(16, 00, 00);

            int consultationTime = 10;
            int[] durations = { 100 };

            TimeSpan test = new TimeSpan(10, 00, 00);

            TimeSpan[] startTimes = { test };

            // act выполнение
            int expectedCount = (int)endWorkingTime.TotalHours * 3 - 10;
            string[] actualArrayString = calculations.AvailablePeriod(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            // assert проверка результата
            Assert.AreEqual(expectedCount, actualArrayString.Length);

        }

        [TestMethod]
        // сравнение количества элементов коллекции от изменения времени вывода свободных интервалов
        public void TestMethod8_checkCount_expected_120_consultationTime_actually_12_count()
        {
            // arange настройка
            TimeSpan beginWorkingTime = new TimeSpan(0, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(24, 00, 00);

            int consultationTime = 120;
            int[] durations = { };

            TimeSpan[] startTimes = { };

            // act выполнение
            int expectedCount = (int)endWorkingTime.TotalHours / (consultationTime / 60);
            string[] actualArrayString = calculations.AvailablePeriod(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            // assert проверка результата
            Assert.AreEqual(expectedCount, actualArrayString.Length);

        }

        [TestMethod]
        public void TestMethod9_expected_1_actually()
        {
            // arange настройка
            TimeSpan beginWorkingTime = new TimeSpan(08, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(20, 00, 00);

            int consultationTime = 30;
            int[] durations = {720};

            TimeSpan test = new TimeSpan(08, 30, 00);

            TimeSpan[] startTimes = { test };

            // act выполнение
            int expectedCount = ((int)endWorkingTime.TotalHours - (int)beginWorkingTime.TotalHours) / durations[0]+1;
            string[] actualArrayString = calculations.AvailablePeriod(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            // assert проверка результата
            Assert.AreEqual(expectedCount, actualArrayString.Length);
        }
        
        [TestMethod]
        public void TestMethod10_expected_1_actually()
        {
            // arange настройка
            TimeSpan beginWorkingTime = new TimeSpan(08, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(24, 00, 00);

            int consultationTime = 30;
            int[] durations = {960};

            TimeSpan test = new TimeSpan(08, 30, 00);

            TimeSpan[] startTimes = { test };

            // act выполнение
            int expectedCount = ((int)endWorkingTime.TotalHours - (int)beginWorkingTime.TotalHours) / durations[0]+1;
            string[] actualArrayString = calculations.AvailablePeriod(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            // assert проверка результата
            Assert.AreEqual(expectedCount, actualArrayString.Length);
        }
    }
}
