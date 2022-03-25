using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Behaviours
{
	[PSerializable]
	public class LoggingAspect : OnMethodBoundaryAspect
	{
		public override void OnExit(MethodExecutionArgs args)
		{
			var result = (args.ReturnValue as HttpResponseMessage)?.Content.ReadAsStringAsync().Result;
			Console.WriteLine(result);
			base.OnExit(args);
		}
	}
}
