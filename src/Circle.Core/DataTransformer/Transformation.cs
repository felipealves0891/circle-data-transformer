using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Circle.Core.DataTransformer.Input;
using Circle.Core.DataTransformer.Output;
using Circle.Core.DataTransformer.Transformer;

namespace Circle.Core.DataTransformer
{
    public class Transformation : TransformationDiagnostic
    {
        public IInputSource Input { get; private set; }

        public IOutputSource Output { get; private set; }

        public List<ITransformer> Transformers { get; private set; }

        public List<IRowTransformer> RowTransformers { get; private set; }

        public int Counter { get; private set; }

        public Transformation(IInputSource input, IOutputSource output)
            : base()
        {
            Input = input;
            Output = output;
            Transformers = new List<ITransformer>();
            RowTransformers = new List<IRowTransformer>();
        }

        public Transformation(IInputSource input, IOutputSource output, List<ITransformer> transformers)
            : base()
        {
            Input = input;
            Output = output;
            Transformers = transformers;
        }

        public Transformation(IInputSource input, IOutputSource output, List<IRowTransformer> rowTransformers)
            : base()
        {
            Input = input;
            Output = output;
            RowTransformers = rowTransformers;
        }

        public Transformation(IInputSource input, IOutputSource output, List<ITransformer> transformers, List<IRowTransformer> rowTransformers)
            : base()
        {
            Input = input;
            Output = output;
            Transformers = transformers;
            RowTransformers = rowTransformers;
        }

        public void AddTransformer(ITransformer transformer)
        {
            if (Transformers.Contains(transformer))
                return;

            Transformers.Add(transformer);
        }

        public void AddRowTransformer(IRowTransformer transformer)
        {
            if (RowTransformers.Contains(transformer))
                return;

            RowTransformers.Add(transformer);
        }

        public void Execute()
        {
            Counter = 0;

            while(Input.Next())
            {
                // read the next line
                object[] line = Input.GetData();

                // apply the transformation
                line = ToTransform(line, Counter);

                // if the line is not null it sends it to the output
                if (line != null)
                    Output.SetData(line);

                Counter++;
            }

            Input.Close();
            Output.Close();
        }

        public object[] ToTransform(object[] line, int lineNumber)
        {
            foreach (var t in RowTransformers)
                line = t.Transform(line, lineNumber);

            if (line != null)
                foreach (var t in Transformers)
                    line = line.Select((data, index) => t.Transform(data, index, lineNumber)).ToArray();

            return line;
        }
    }
}
