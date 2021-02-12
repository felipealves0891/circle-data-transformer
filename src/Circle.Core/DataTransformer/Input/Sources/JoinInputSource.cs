using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Input.Sources
{
    public class JoinInputSource : IInputSource
    {
        private Queue<IInputSource> _sources = new Queue<IInputSource>();

        private Queue<IInputSource> _consumed = new Queue<IInputSource>();

        private IInputSource _source = null;

        public JoinInputSource(List<IInputSource> sources)
        {
            foreach (var x in sources)
                _sources.Enqueue(x);
        }

        public void Open(string connectionString)
        {
            throw new NotImplementedException();
        }

        public bool Next()
        {
            if (_source == null)
                _source = _sources.Dequeue();

            if (_source.Next())
                return true;

            _consumed.Enqueue(_source);
            if (_sources.Count <= 0)
                return false;

            _source = _sources.Dequeue();
            if (_source == null)
                return false;

            return _source.Next();
        }

        public object[] GetData()
        {
            return _source.GetData();
        }

        public void Close()
        {
            foreach (var q in _sources)
                q.Close();

            foreach (var q in _consumed)
                q.Close();
        }

    }
}
