using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListImplementation
{
    public class _List
    {
        public _List(int capacity)
        {
            _capacity = capacity;
            _items = new int[capacity];
        }

        public _List()
        {

        }

        public int this[int i]
        {
            get
            {
                if (i >= _size)
                    throw new IndexOutOfRangeException();
                return _items[i];
            }
            set
            {
                if (i >= _capacity)
                    throw new IndexOutOfRangeException();
                _items[i] = value;
            }
        }


        private int _defaultCapacity = 4;
        private int[] _items;
        private int _capacity = 0;
        private int _size;
        private int _version;
        private int[] _temp;
        private int[] _emptyArray = new int[0];
        public int Count { get => _size; }
        public void Add(int value)
        {
            EnsureCapacity(_size + 1);
            _items[_size++] = value;
        }

        public override string ToString()
        {
            return $"Count = {Count}";
        }

        private void EnsureCapacity(int min)
        {
            if (_capacity == 0)
            {
                _capacity = _defaultCapacity;
                _items = new int[_defaultCapacity];
            }

            if (_size >= _capacity)
            {
                _capacity *= 2;
                _temp = new int[_items.Length];
                for (int i = 0; i < _items.Length; i++)
                {
                    _temp[i] = _items[i];
                }
                _items = new int[_capacity];

                for (int i = 0; i < _temp.Length; i++)
                {
                    _items[i] = _temp[i];
                }
                _temp = null;
            }

        }

        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        int[] newItems = new int[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }
                }
            }
        }

        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                _size = 0;
            }
            _version++;
        }

        public int IndexOf(int item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public int IndexOf(int item, int index, int count)
        {
            if (index + count > _size)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = index; i < index + count; i++)
            {
                if (_items[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, int item)
        {
            if (index > _size)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }
            if (index < _size)
            {
                Array.Copy(_items, index, _items, index + 1, _size - index);
            }
            _items[index] = item;
            _size++;
            _version++;
        }

        public bool Remove(int item)
        {
            int index = IndexOf(item);
            if (index >= 0)
                RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if ((uint)index > (uint)_size)
            {
                throw new ArgumentOutOfRangeException();
            }
            _size--;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
            }
            _items[_size] = default(int);
            _version++;
        }

        public void RemoveRange(int index, int count)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (index + count > _size)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (count > 0)
            {
                int i = _size;
                _size -= count;

                if (index < _size)
                {
                    Array.Copy(_items, index + count, _items, index, _size - index);
                }

                Array.Clear(_items, _size, count);
                _version++;
            }
        }
        public Enumerator GetEnumerator()
        {
            return new Enumerator(_items, _size);
        }
        public class Enumerator
        {
            private int[] _items;
            private int _size;
            private int _count = 0;
            public Enumerator(int[] items, int size)
            {
                _items = items;
                _size = size;
            }
            public int Current { get => _items[_count++]; }
            public bool MoveNext()
            {
                return _count < _size;
            }
        }

    }
}

