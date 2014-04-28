﻿namespace Kafka.Client.Api
{
    using System.IO;

    using Kafka.Client.Common;
    using Kafka.Client.Common.Imported;
    using Kafka.Client.Extensions;
    using Kafka.Client.Messages;

    public class FetchResponsePartitionData
    {
        public static FetchResponsePartitionData ReadFrom(ByteBuffer buffer)
        {
            var error = buffer.GetShort();
            var hw = buffer.GetLong();
            var messageSetSize = buffer.GetInt();
            var messageSetBuffer = buffer.Slice();
            messageSetBuffer.Limit(messageSetSize);
            buffer.Position = buffer.Position + messageSetSize;

            return new FetchResponsePartitionData(error, hw, new ByteBufferMessageSet(messageSetBuffer));
        }

        public const int HeaderSize = 2 + /* error code */ 8 + /* high watermark */ 4 /* messageSetSize */;


        public FetchResponsePartitionData(short error = ErrorMapping.NoError, long hw = -1, MessageSet messages = null)
        {
            this.Error = error;
            this.Hw = hw;
            this.Messages = messages;
        }


        public short Error { get; private set; }

        public long Hw { get; private set; }

        public MessageSet Messages { get; private set; }

        public int SizeInBytes
        {
            get
            {
                return HeaderSize + Messages.SizeInBytes;
            }
        }
    }
}