/*
 MIT License

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial SerialPortions of the Software.

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
using System.Threading;

namespace iRobot.Queues
{
    class CommandQueue
    {

        #region Variables

        /// <summary>
        /// Event control thread.
        /// </summary>
        private Thread queServiceThread;

        /// <summary>
        /// Requests queue.
        /// </summary>
        private Queue queue = new Queue();

        /// <summary>
        /// Lock mechanism.
        /// </summary>
        private object lockRequestInput = new object();

        /// <summary>
        /// 
        /// </summary>
        private bool requestToStopTheThread = false;

        #endregion

        #region Properties

        /// <summary>
        /// Delay between requests passing.
        /// </summary>
        public int QueueDelay { get; set; }

        /// <summary>
        /// Process request handler.
        /// </summary>
        public CommandQueueRequestDelegate QueueHandler { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the que.
        /// </summary>
        public void Start()
        {
            this.requestToStopTheThread = false;
            // Create the communication thread.
            this.queServiceThread = new Thread(new ThreadStart(this.PoolMethod));
            // Start the thread
            this.queServiceThread.Start();
        }

        /// <summary>
        /// Stop the que.
        /// </summary>
        public void Stop()
        {
            this.requestToStopTheThread = true;
            this.queServiceThread = null;
        }

        /// <summary>
        /// Put item to queue.
        /// </summary>
        /// <param name="request">Request item.</param>
        public void PutToQue(object request)
        {
            lock (this.lockRequestInput)
            {
                this.queue.Enqueue(request);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private object GetFromQue()
        {
            object request = null;

            lock (this.lockRequestInput)
            {
                try
                {
                    if (this.queue.Count > 0)
                    {
                        request = this.queue.Dequeue();
                    }
                }
                catch
                {

                }
            }

            return request;
        }

        /// <summary>
        /// Communication thread call back.
        /// </summary>
        private void PoolMethod()
        {
            while (!this.requestToStopTheThread)
            {
                try
                {
                    object request = this.GetFromQue();

                    if (request != null)
                    {
                        // Pass the request.
                        this.QueueHandler?.Invoke(request);

                        // Wait for a while.
                        Thread.Sleep(this.QueueDelay);
                    }
                }
                catch (Exception exception)
                {
                    // Create log.
                    //Log.CreateRecord("PrintingService.PrintingService.PoolMethod()[GENERAL_EXCEPTION]", exception.ToString() + Environment.NewLine, LogMessageTypes.Error);
                }
            }
        }

        #endregion

    }
}
