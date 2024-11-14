using dotNetRealTimeProcessingBasics.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace dotNetRealTimeProcessingBasics.Channels
{
    public class VehicleSpeedManagerChannel : BoundedChannel<decimal>
    {
        public VehicleSpeedManagerChannel(int channelCapacity, string channelName) : base(channelCapacity, channelName)
        {
        }

        public override Task Process(ChannelReader<decimal> reader)
        {
            throw new NotImplementedException();
        }
    }
}
