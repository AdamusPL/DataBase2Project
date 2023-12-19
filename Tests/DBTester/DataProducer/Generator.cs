using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer
{
    public class Generator
    {
        private List<IEntityGenerator> _entityGenerators;

        public Generator(List<IEntityGenerator> entityGenerators)
        {
            _entityGenerators = entityGenerators;
        }

        public void Generate()
        {
            foreach (var item in _entityGenerators)
            {
                item.Generate();
            }
        }

    }
}
