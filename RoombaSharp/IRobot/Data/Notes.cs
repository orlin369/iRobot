/*

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace iRobot.Data
{
    [Serializable]
    public class Notes : IList<Note>
    {

        #region Variables

        /// <summary>
        /// Notes container.
        /// </summary>
        private List<Note> container = new List<Note>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Notes()
        {

        }

        #endregion

        #region IList<Note> implementation.

        public Note this[int index]
        {
            get
            {
                return this.container[index];
            }

            set
            {
                this.container[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return this.container.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(Note item)
        {
            this.container.Add(item);
        }

        public void Clear()
        {
            this.container.Clear();
        }

        public bool Contains(Note item)
        {
            return this.container.Contains(item);
        }

        public void CopyTo(Note[] array, int arrayIndex)
        {
            this.container.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Note> GetEnumerator()
        {
            return this.container.GetEnumerator();
        }

        public int IndexOf(Note item)
        {
            return this.container.IndexOf(item);
        }

        public void Insert(int index, Note item)
        {
            this.container.Insert(index, item);
        }

        public bool Remove(Note item)
        {
            return this.container.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.container.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.container.GetEnumerator();
        }

        #endregion

    }
}
