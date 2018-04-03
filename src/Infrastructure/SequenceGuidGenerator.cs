using ApplicationCore;
using System;
using System.Threading;

namespace Infrastructure.SharedKernel
{
    public class SequenceGuidGenerator : ISequenceGuidGenerator
    {
        class SequenceGuid : ISequenceGuid
        {
            readonly Guid _id;

            internal SequenceGuid(Guid guid)
            {
                _id = guid;
            }

            public Guid Id => _id;
        }

        object _locker = new object();
        long _counter = DateTime.UtcNow.Ticks;

        /// <summary>
        /// 生成SqlServer可排序的guid
        /// </summary>
        /// <returns></returns>
        public ISequenceGuid SqlServerKey()
        {
            byte[] guidBytes = Guid.NewGuid().ToByteArray();
            byte[] counterBytes;

            lock (_locker)
            {
                counterBytes = BitConverter.GetBytes(Interlocked.Increment(ref _counter));
            }

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(counterBytes);
            }

            guidBytes[08] = counterBytes[1];
            guidBytes[09] = counterBytes[0];
            guidBytes[10] = counterBytes[7];
            guidBytes[11] = counterBytes[6];
            guidBytes[12] = counterBytes[5];
            guidBytes[13] = counterBytes[4];
            guidBytes[14] = counterBytes[3];
            guidBytes[15] = counterBytes[2];

            var id = new Guid(guidBytes);
            return new SequenceGuid(id);
        }
    }
}
