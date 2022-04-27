using GM.VM.Functions;
using Sandbox;
using System.Collections.Generic;

namespace GM.VM;

public class VirtualMachine
{
	public static VirtualMachine The { get; internal set; }

	public Context GlobalContext { get; set; }

	private readonly Dictionary<string, Function> DefaultFunctionPool = new()
	{
		{ "Log", new LogFunction() }
	};

	public VirtualMachine()
	{
		The = this;

		Reset();
	}

	public void Reset()
	{
		GlobalContext = new();
		GlobalContext.Caller = "GlobalContext";
		GlobalContext.FunctionPool = DefaultFunctionPool;
	}
}
