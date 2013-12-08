using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Core;
using Action.Utility;
using System.Collections;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class SelectFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//选择指定的字段形成新的集合\nobject[] select(prop1,prop2,...,propn);"; }
        }

        public Type Class
        {
            get { return typeof(IEnumerable); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "select(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length > 0 && args.Length < 6)
            {
                if (data.Value is IEnumerable<object>)
                {
                    var collection = (IEnumerable<object>)data.Value;
                    switch (args.Length)
                    {
                        case 1:
                            var field = MyConvert.ToString(args[0]);
                            data.Update(collection.Select(obj => TypeHelper.GetPropertyValue(obj, field)));
                            break;
                        case 2:
                            var field1 = MyConvert.ToString(args[0]);
                            var field2 = MyConvert.ToString(args[1]);
                            data.Update(collection.Select(obj => new Tuple<object, object>(
                                TypeHelper.GetPropertyValue(obj, field1),
                                TypeHelper.GetPropertyValue(obj, field2))));
                            break;
                        case 3:
                            field1 = MyConvert.ToString(args[0]);
                            field2 = MyConvert.ToString(args[1]);
                            var field3 = MyConvert.ToString(args[2]);
                            data.Update(collection.Select(obj => new Tuple<object, object, object>(
                                TypeHelper.GetPropertyValue(obj, field1),
                                TypeHelper.GetPropertyValue(obj, field2),
                                TypeHelper.GetPropertyValue(obj, field3))));
                            break;
                        case 4:
                            field1 = MyConvert.ToString(args[0]);
                            field2 = MyConvert.ToString(args[1]);
                            field3 = MyConvert.ToString(args[2]);
                            var field4 = MyConvert.ToString(args[3]);
                            data.Update(collection.Select(obj => new Tuple<object, object, object, object>(
                                TypeHelper.GetPropertyValue(obj, field1),
                                TypeHelper.GetPropertyValue(obj, field2),
                                TypeHelper.GetPropertyValue(obj, field3),
                                TypeHelper.GetPropertyValue(obj, field4))));
                            break;
                        case 5:
                            field1 = MyConvert.ToString(args[0]);
                            field2 = MyConvert.ToString(args[1]);
                            field3 = MyConvert.ToString(args[2]);
                            field4 = MyConvert.ToString(args[3]);
                            var field5 = MyConvert.ToString(args[4]);
                            data.Update(collection.Select(obj => new Tuple<object, object, object, object, object>(
                                TypeHelper.GetPropertyValue(obj, field1),
                                TypeHelper.GetPropertyValue(obj, field2),
                                TypeHelper.GetPropertyValue(obj, field3),
                                TypeHelper.GetPropertyValue(obj, field4),
                                TypeHelper.GetPropertyValue(obj, field5))));
                            break;
                    }
                }                
                else
                    data.Update(null, 1, "Object is not a instance of IEnumerable<object>");
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("select", 1, 5));
        }
    }
}
