using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Action.Model
{
    public class RandomFacade
    {
        private const int _size = 10000;
        private Random _random;

        private int[] _values;
        public int[] Values
        {
            get { return _values; }
        }

        private int _index = 0;
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public void Init()
        {
            _values = new int[_size];
            _random = new Random();
            for (int i = 0; i < _size; i++)
                _values[i] = _random.Next(10000);
        }

        public int Next()
        {
            //return 9995;
            lock (this)
            {
                if (_index >= _size)
                    _index = 0;
                return _values[_index++];
            }
        }

        public bool Percent(int data)
        {
            return Next() < data * 100;
        }

        public int Range(int min, int max)
        {
            return Next() % (max - min + 1) + min;
        }

        public int[] Range(int min, int max, int count, params int[] exceptions)
        {
            count = Math.Min(max - min + 1 - exceptions.Length, count);
            var results = new int[count];
            for (var i = 0; i < count; i++)
            {
                var result = Range(min, max);
                while(results.Contains(result) || exceptions.Contains(result))
                {
                    result++;
                    if (result > max)
                        result = min;
                }
                results[i] = result;
            }
            return results;
        }

        public int Select(int[] array)
        {
            return array[Range(0, array.Length - 1)];
        }
    }
}
