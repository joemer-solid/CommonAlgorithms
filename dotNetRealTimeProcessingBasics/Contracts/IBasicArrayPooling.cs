using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetRealTimeProcessingBasics.Contracts
{
    public interface IBasicArrayPooling
    {
        Task TransformStringToByteArray(string input);
    }
}
