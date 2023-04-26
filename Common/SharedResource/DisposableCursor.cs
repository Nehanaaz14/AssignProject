using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;


namespace SharedResource
{
    public class DisposableCursor : IDisposable
    {
        private Cursor PreviousCursor { get; set; }
        private Dispatcher Dispatcher { get; }

        private static DisposableCursor disposableCursor;

        public DisposableCursor(Cursor cursor)
        {

            this.Dispatcher = Application.Current?.Dispatcher;

            this.Invoke(
                () =>
                {
                    this.PreviousCursor = Mouse.OverrideCursor;
                    Mouse.OverrideCursor = cursor;
                });
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DisposableCursor"/> class.
        /// </summary>
        ~DisposableCursor()
        {
            this.Dispose(false);
        }

        /// <summary>Gets a value indicating whether or not this instance has been disposed.</summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// The value <c>true</c> if this method is called explicitly.
        /// otherwise, <c>false</c>.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Do nothing if executing from the garbage collector or if this instance has already been disposed.
            if (!disposing || this.IsDisposed)
            {
                return;
            }

            Debug.Assert(disposing, "Instances of this class should always be disposed.");
            this.Invoke(() => { Mouse.OverrideCursor = this.PreviousCursor; });

            // Set the flag indicating this instance is disposed.
            this.IsDisposed = true;
        }

        /// <summary>Conditionally invokes an action on the dispatcher if it's defined.</summary>
        /// <param name="action">The action.</param>
        private void Invoke(Action action)
        {
            if (this.Dispatcher != null)
            {
                this.Dispatcher.Invoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        public static void Show(Cursor cursor)
        {
            if (disposableCursor == null)
            {
                disposableCursor = new DisposableCursor(cursor);
            }
        }

        public static void Hide()
        {
            if (disposableCursor != null)
            {
                disposableCursor.Dispose();
                disposableCursor = null;
            }
        }
    }
}
