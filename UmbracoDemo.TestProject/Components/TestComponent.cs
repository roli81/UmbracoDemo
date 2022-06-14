using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;

namespace UmbracoDemo.TestProject.Components
{
    public class TestComponent : IComponent
    {
        public void Initialize()
        {
            //occures when umbraco starts up
        }

        public void Terminate()
        {
            // occures when umbraco shut down
        }
    }
}
