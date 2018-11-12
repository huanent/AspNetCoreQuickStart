using System;
using System.Threading;

namespace MyCompany.MyProject.Persistence.Internal
{
    public class SequentialGuidGenerator : ISequentialGuidGenerator
    {
        public Guid Next()
        {
            long counter = DateTime.UtcNow.Ticks;
            byte[] guidBytes = Guid.NewGuid().ToByteArray();
            byte[] counterBytes = BitConverter.GetBytes(Interlocked.Increment(ref counter));

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

            return new Guid(guidBytes);
        }
    }
}
