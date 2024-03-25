using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        
        public static ModelAbstractApi CreateApi(ILogicLayer logicLayer = default(ILogicLayer))
        {
            return new ModelApi(logicLayer ?? ILogicLayer.Create());
        }
    }

    internal class ModelApi : ModelAbstractApi
    {
        private ILogicLayer logicLayer;

        public ModelApi(ILogicLayer logicLayer)
        {
            this.logicLayer = logicLayer;
        }

    }
}
