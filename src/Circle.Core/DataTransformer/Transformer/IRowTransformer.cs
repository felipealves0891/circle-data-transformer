using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Core.DataTransformer.Transformer
{
    public interface IRowTransformer
    {
        object[] Transform(object[] inputData, int lineIndex);
    }
}
