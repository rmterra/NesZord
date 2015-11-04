﻿using System.Linq;
using System.Reflection;
using NSpec;
using NSpec.Domain;
using NSpec.Domain.Formatters;

/*
 * Howdy,
 * 
 * This is NSpec's DebuggerShim.  It will allow you to use TestDriven.Net or Resharper's test runner to run
 * NSpec tests that are in the same Assembly as this class.  
 * 
 * It's DEFINITELY worth trying specwatchr (http://nspec.org/continuoustesting). Specwatchr automatically
 * runs tests for you.
 * 
 * If you ever want to debug a test when using Specwatchr, simply put the following line in your test:
 * 
 *     System.Diagnostics.Debugger.Launch()
 *     
 * Visual Studio will detect this and will give you a window which you can use to attach a debugger.
 */

//[TestFixture]
public class DebuggerShim
{
    //[Test]
    public void debug()
    {
		RunTests();
    }

	public static void Main(string[] args)
	{
		new DebuggerShim().RunTests();
	}

	private void RunTests(string className = null)
	{
		var types = GetType().Assembly.GetTypes();
		// OR
		// var types = new Type[]{typeof(Some_Type_Containg_some_Specs)};
		var finder = new SpecFinder(types, "");

		var builder = string.IsNullOrWhiteSpace(className)
			? new ContextBuilder(finder, new DefaultConventions())
			: new ContextBuilder(finder, new Tags().Parse(className), new DefaultConventions());

		var runner = new ContextRunner(builder, new ConsoleFormatter(), false);
		var results = runner.Run(builder.Contexts().Build());

		//assert that there aren't any failures
		results.Failures().Count().should_be(0);
	}
}
