using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022UserKonstantinovAndrey
{
    public class Calculations
    {
        /* в метод входят 5 именованных аргумента представлющих: массив с началом занятого промежутка, массив их длительности, время начала консультаций,
         * их окончанияя и кол-во минут представляющий диапозон для вывода свободных промежутков.
         * Метод возращает на выходе string массив
        */
        public string[] AvailablePeriod(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            // список в который будет складыватся все временные промежутки
            List<TimeSpan> Times = new List<TimeSpan>();

            // переменная count для подсчета итераций цикла
            int count = 0;

            // цикл-счетчик TimeSpan считающий каждую минуту до конча рабочего времени
            for (TimeSpan i = beginWorkingTime; i <= endWorkingTime; i += new TimeSpan(00, 01, 00))
            {
                // цикл проверки исключения
                for (int t = 0; t < startTimes.Length; t++)
                {
                    /* если наше время совпадает с исключением, то переменная, подсчитывающая итерации, сбрасывается;
                     * в список/Склад записывается наше значение; а для цикла добавляется количество итераций,
                     т.к. durations минуты, а цикл по минутно, то получается наш счетчик сразу пропускает интервал(занятый)*/
                    if (i == startTimes[t])
                    {
                        Times.Add(i);
                        i += new TimeSpan(00, durations[t], 00);
                        count = 0;
                    }
                }
                // условия для сброса переменной count
                if (count == consultationTime) count = 0;

                // каждое 0 значкеие мы записываем наше время
                // мы их дублируем для дальнейшего удобства в составлении формата для вывода
                if (count == 0)
                {
                    Times.Add(i);
                    Times.Add(i);
                }

                // каждую итерацию цикла наша переменная инкрементируется
                count++;
            }

            // булевая переменная для проверки условия
            bool check = true;

            // два списка в которые, согласно формату, будут записывается значение старта свободного промежутка и его конец
            List<string> outputBegin = new List<string>();
            List<string> outputEnd = new List<string>();

            // в цикле мы проходимся по каждому элементу нашего Списка/Склада значений
            for (int i = 0; i < Times.Count; i++)
            {
                // цикл только для проверки на исключения, чтобы у нас только на эти времена добовлялись элементы ОДИН раз
                for (int t = 0; t < startTimes.Length; t++)
                {
                    if (Times[i] == startTimes[t])
                    {
                        outputEnd.Add(Times[i].ToString(@"hh\:mm"));
                        outputBegin.Add((Times[i] + new TimeSpan(00, durations[t], 00)).ToString(@"hh\:mm"));
                        check = false;
                    }
                }

                /* 
                 * в выъодном формате первые элементы идут с индексом 0 и кратные 2, вторые наоборот
                 * так же в условии проверка был ли этот элемент исключением, если да
                 * то bool'евая переменна принимает значение false, для того чтобы она не записалась еще раз 
                */
                if (i == 0 || i % 2 == 0 && check == true) outputBegin.Add(Times[i].ToString(@"hh\:mm"));
                else if (i != 1 && i % 2 != 0 && check == true) outputEnd.Add(Times[i].ToString(@"hh\:mm"));

                // возрашаем обратно в состояние true
                check = true;
            }

            // список для хранения результата, не массив потому что еще не известно сколько будет хранится значений
            List<string> Result = new List<string>();

            // заполняем список в правильной форме уже готовых значений
            for (int i = 0; i < outputEnd.Count; i++)
            {
                if (outputBegin[i] != outputEnd[i])
                    Result.Add($"{outputBegin[i]}-{outputEnd[i]}");
            }

            // перевод из списка в массив
            string[] stringOut = new string[Result.Count];
            for (int i = 0; i < Result.Count; i++) stringOut[i] = Result[i];

            // возращаем string[]
            return stringOut;
        }
    }
}
