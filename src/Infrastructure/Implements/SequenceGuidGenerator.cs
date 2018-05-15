using ApplicationCore;
using ApplicationCore.SharedKernel;
using System;
using System.Security.Cryptography;
using System.Threading;

namespace Infrastructure.Implements
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
        static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

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

        public ISequenceGuid MySqlKey(bool oldGuids)
        {
            byte[] randomBytes = new byte[8];
            Rng.GetBytes(randomBytes);
            ulong ticks = (ulong)DateTime.UtcNow.Ticks;

            if (oldGuids)
            {
                byte[] guidBytes = new byte[16];
                byte[] tickBytes = BitConverter.GetBytes(ticks);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(tickBytes);
                Buffer.BlockCopy(tickBytes, 0, guidBytes, 0, 8);
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 8, 8);
                return new SequenceGuid(new Guid(guidBytes));
            }

            var guid = new Guid((uint)(ticks >> 32), (ushort)(ticks << 32 >> 48), (ushort)(ticks << 48 >> 48),
                randomBytes[0],
                randomBytes[1],
                randomBytes[2],
                randomBytes[3],
                randomBytes[4],
                randomBytes[5],
                randomBytes[6],
                randomBytes[7]);

            return new SequenceGuid(guid);
        }
    }
}
