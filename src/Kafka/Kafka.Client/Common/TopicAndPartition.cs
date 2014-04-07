﻿namespace Kafka.Client.Common
{
    using System;
    using System.Collections.Concurrent;

    using Kafka.Client.Cluster;

    public class TopicAndPartition
    {
        public string Topic { get; private set; }

        public int Partiton { get; private set; }

        public TopicAndPartition(string topic, int partiton)
        {
            this.Topic = topic;
            this.Partiton = partiton;
        }

        public TopicAndPartition(Tuple<string, int> tuple)
            : this(tuple.Item1, tuple.Item2)
        {
        }

        public TopicAndPartition(Partition partition)
            : this(partition.Topic, partition.PartitionId)
        {
            
        }

        public TopicAndPartition(Replica replica) : this(replica.Topic, replica.PartitonId)
        {
            
        }

        public override string ToString()
        {
            return string.Format("[{0},{1}]", this.Topic, this.Partiton);
        }
    }
}