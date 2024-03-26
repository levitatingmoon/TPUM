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
        
        public static ModelAbstractApi CreateApi(LogicAbstractApi logicLayer = default(LogicAbstractApi))
        {
            return new ModelApi(logicLayer ?? LogicAbstractApi.Create());
        }
    }

    internal class ModelApi : ModelAbstractApi
    {
        private LogicAbstractApi logicLayer;

        public ModelApi(LogicAbstractApi logicLayer)
        {
            this.logicLayer = logicLayer;
        }

    }
}
