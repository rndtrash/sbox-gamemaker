using System.Collections.Generic;

namespace GM.VM;

public class Context
{
	public Context Previous { get; protected set; } = null;

	public string Caller { get; set; } = "";

	public Dictionary<string, Function> FunctionPool { get; set; } = new();
	public Dictionary<string, Variable> VariablePool { get; set; } = new();

	public Context()
	{
	}

	public Context( Context previous, string from )
	{
		Previous = previous;
		Caller = from;
	}

	public List<string> GetTrace()
	{
		if ( Previous is null )
			return null;

		List<string> trace = new() { Caller };

		if ( Previous.GetTrace() is List<string> previousTrace )
			trace.AddRange( previousTrace );

		return trace;
	}

	public Function GetFunction( string name )
	{
		if ( FunctionPool.ContainsKey( name ) )
			return FunctionPool[name];

		if ( Previous is not null )
			return Previous.GetFunction( name );

		return null;
	}

	public Variable GetVariable( string name )
	{
		if ( VariablePool.ContainsKey( name ) )
			return VariablePool[name];

		if ( Previous is not null )
			return Previous.GetVariable( name );

		return null;
	}
}
