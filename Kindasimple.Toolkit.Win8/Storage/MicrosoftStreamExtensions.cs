using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

//http://stackoverflow.com/questions/7669311/is-there-a-way-to-convert-a-system-io-stream-to-a-windows-storage-streams-irando
namespace Kindasimple.Toolkit.Storage
{
    public static class MicrosoftStreamExtensions
    {
        public static IRandomAccessStream AsRandomAccessStream(this Stream stream)
        {
            return new RandomStream(stream);
        }
    }

    class RandomStream : IRandomAccessStream
    {
        Stream internstream;
        public RandomStream(Stream underlyingstream)
        {
            internstream = underlyingstream;
        }

        public IInputStream GetInputStreamAt(ulong position)
        {
            //THANKS Microsoft! This is GREATLY appreciated!
            internstream.Position = (long)position;
            return internstream.AsInputStream();
        }

        public IOutputStream GetOutputStreamAt(ulong position)
        {
            internstream.Position = (long)position;
            return internstream.AsOutputStream();
        }

        public ulong Size
        {
            get
            { return (ulong)internstream.Length; }
            set
            { internstream.SetLength((long)value); }
        }

        public bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanWrite
        {
            get { throw new NotImplementedException(); }
        }

        public IRandomAccessStream CloneStream()
        {
            throw new NotImplementedException();
        }

        public ulong Position
        {
            get { throw new NotImplementedException(); }
        }

        public void Seek(ulong position)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Windows.Foundation.IAsyncOperationWithProgress<IBuffer, uint> ReadAsync(IBuffer buffer, uint count, InputStreamOptions options)
        {
            throw new NotImplementedException();
        }

        public Windows.Foundation.IAsyncOperation<bool> FlushAsync()
        {
            throw new NotImplementedException();
        }

        public Windows.Foundation.IAsyncOperationWithProgress<uint, uint> WriteAsync(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        bool IRandomAccessStream.CanRead
        {
            get { throw new NotImplementedException(); }
        }

        bool IRandomAccessStream.CanWrite
        {
            get { throw new NotImplementedException(); }
        }

        IRandomAccessStream IRandomAccessStream.CloneStream()
        {
            throw new NotImplementedException();
        }

        IInputStream IRandomAccessStream.GetInputStreamAt(ulong position)
        {
            throw new NotImplementedException();
        }

        IOutputStream IRandomAccessStream.GetOutputStreamAt(ulong position)
        {
            throw new NotImplementedException();
        }

        ulong IRandomAccessStream.Position
        {
            get { throw new NotImplementedException(); }
        }

        void IRandomAccessStream.Seek(ulong position)
        {
            throw new NotImplementedException();
        }

        ulong IRandomAccessStream.Size
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        Windows.Foundation.IAsyncOperationWithProgress<IBuffer, uint> IInputStream.ReadAsync(IBuffer buffer, uint count, InputStreamOptions options)
        {
            throw new NotImplementedException();
        }

        Windows.Foundation.IAsyncOperation<bool> IOutputStream.FlushAsync()
        {
            throw new NotImplementedException();
        }

        Windows.Foundation.IAsyncOperationWithProgress<uint, uint> IOutputStream.WriteAsync(IBuffer buffer)
        {
            throw new NotImplementedException();
        }
    }
}
